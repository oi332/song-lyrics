using SongLyrics.Application.Models;
using SongLyrics.Common;
using SongLyrics.Communication;
using SongLyrics.Communication.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SongLyrics.Application
{
    public class SongService : ISongService
    {
        private readonly ILyricsClient _lyricsClient;
        private readonly ISpotifyClient _spotifyClient;

        public SongService(ILyricsClient lyricsClient, ISpotifyClient spotifyClient)
        {
            _lyricsClient = lyricsClient;
            _spotifyClient = spotifyClient;
        }

        public async Task<Song> GetCurrentlyPlayingSongAsync()
        {
            try
            {
                var songBase = (await _spotifyClient.GetCurrentPlayingSongAsync()).ToEntity();

                var songBaseCopy = SongNormalizer.Normalize(songBase);

                var lyricsResponse = await _lyricsClient.GetLyricsAsync(songBaseCopy.ToRequest());

                return songBase.ToSong(lyricsResponse);
            }
            catch (SpotifyAccessTokenExpiredException)
            {
                AccessTokenRenewer.Renew((await _spotifyClient.GetAccessTokenAsync()).Token);

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
