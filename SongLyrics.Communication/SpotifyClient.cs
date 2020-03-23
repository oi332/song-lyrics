using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public SpotifyClient(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<SongResponse> GetCurrentPlayingSongAsync()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player/currently-playing");

            var accessToken = await GetAccessTokenAsync();

            req.Headers.Add("Authorization", $"Bearer {accessToken.Token}");

            SongResponse song;

            try
            {
                song = await MakeRequestAsync<SongResponse>(req);
            }
            catch
            {
                throw new HttpRequestException("Failed to retrieve current playing song.");
            }
            

            return song;
        }

        private async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            var body = new Dictionary<string, string>();

            body.Add("grant_type", "refresh_token");
            body.Add("refresh_token", _config.GetSection("SpotifySettings").GetSection("RefreshToken").Value);

            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(body)
            };

            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            req.Headers.Add("Authorization", $"Basic {GenerateBasicToken()}");

            AccessTokenResponse accessTokenResponse;

            try
            {
                accessTokenResponse = await MakeRequestAsync<AccessTokenResponse>(req);
            }
            catch
            {
                throw new HttpRequestException("Failed to retrieve access token for Spotify.");
            }
            
            return accessTokenResponse;
        }

        private async Task<T> MakeRequestAsync<T>(HttpRequestMessage req)
        {
            var res = await _client.SendAsync(req);

            if (!res.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            var content = await res.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(content);

            return result;
        }

        private string GenerateBasicToken()
        {
            var clientId = _config.GetSection("SpotifySettings").GetSection("ClientId").Value;
            var clientSecret = _config.GetSection("SpotifySettings").GetSection("ClientSecret").Value;

            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
