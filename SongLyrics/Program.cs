using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SongLyrics.Application;
using SongLyrics.Application.Models;
using SongLyrics.Common;
using SongLyrics.Communication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SongLyrics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = RegisterServices();
            var songService = serviceProvider.GetService<ISongService>();

            try
            {
                Print(await songService.GetCurrentlyPlayingSongAsync());
                Console.Read();
            }
            catch (SpotifyAccessTokenExpiredException)
            {
                Console.WriteLine("Access token renewed. Restart the program.");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }

        public static void Print(Song song) 
        {
            Console.WriteLine($"{song.ArtistName} | {song.Name}");
            Console.WriteLine(song.Lyrics);
        }

        private static ServiceProvider RegisterServices()
        {
            var configuration = SetupConfiguration();
            var config = new SpotifySettings();

            configuration.Bind("SpotifySettings", config);

            var serviceCollection = new ServiceCollection();


            serviceCollection.AddSingleton(config);
            serviceCollection.AddHttpClient<ISpotifyClient, SpotifyClient>();
            serviceCollection.AddHttpClient<ILyricsClient, LyricsClient>();
            serviceCollection.AddScoped<ISongService, SongService>();

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
