using Microsoft.Extensions.Configuration;
using SongLyrics.Common;
using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SongLyrics.Communication
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly SpotifySettings _config;
        private readonly HttpClient _client;

        public SpotifyClient(SpotifySettings config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<SongResponse> GetCurrentPlayingSongAsync()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player/currently-playing");

            req.Headers.Add("Authorization", $"Bearer {_config.AccessToken}");

            var res = await _client.SendAsync(req);

            if (res.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                throw new SpotifyNoSongPlayingException("No song is currently playing.");
            }

            if (!res.IsSuccessStatusCode)
            {
                throw new SpotifyAccessTokenExpiredException("Access token expired.");
            }
                
            var content = await res.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<SongResponse>(content); ;
        }

        public async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            var body = new Dictionary<string, string>();

            body.Add("grant_type", "refresh_token");
            body.Add("refresh_token", _config.RefreshToken);

            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(body)
            };

            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            req.Headers.Add("Authorization", $"Basic {GenerateBasicToken()}");

            var res = await _client.SendAsync(req);

            if (!res.IsSuccessStatusCode)
                throw new SpotifyAccessTokenAcquireException("Failed to retrieve access token.");

            var content = await res.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<AccessTokenResponse>(content);
        }

        private string GenerateBasicToken()
        {
            var clientId = _config.ClientId;
            var clientSecret = _config.ClientSecret;

            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
