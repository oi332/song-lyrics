using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SongLyrics.Communication;
using SongLyrics.Models;
using System;
using System.Threading.Tasks;

namespace SongLyrics
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            var serviceProvider = RegisterServices();

            var spotifyClient = serviceProvider.GetService<ISpotifyClient>();
            var lyricsClient = serviceProvider.GetService<ILyricsClient>();

            var s = (await spotifyClient.GetCurrentPlayingSongAsync()).ToEntity();

            Console.WriteLine(s.ArtistName + " | " + s.Name);

            SongNormalizer.Normalize(s);

            var lyrics = await lyricsClient.GetLyricsAsync(s.ToRequest());

            Console.WriteLine(lyrics.Lyrics);
        }

        private static ServiceProvider RegisterServices()
        {
            var configuration = SetupConfiguration();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddHttpClient<ISpotifyClient, SpotifyClient>();
            serviceCollection.AddHttpClient<ILyricsClient, LyricsClient>();


            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration SetupConfiguration()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
        }
    }
}
