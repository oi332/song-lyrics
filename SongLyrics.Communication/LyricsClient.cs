using SongLyrics.Communication.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SongLyrics.Communication
{
    public class LyricsClient : ILyricsClient
    {
        private readonly HttpClient _client;

        public LyricsClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<LyricsResponse> GetLyricsAsync(SongRequest songRequest)
        {
            var URL = GenerateURL(songRequest);
            var req = new HttpRequestMessage(HttpMethod.Get, URL);

            var res = await _client.SendAsync(req);

            if (!res.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to find lyrics");
            }

            var content = await res.Content.ReadAsStreamAsync();
            
            return PageParser.Parse(content);
        }

        private string GenerateURL(SongRequest songRequest)
        {
            var baseURL = "https://www.azlyrics.com/lyrics";

            return $"{baseURL}/{songRequest.ArtistName}/{songRequest.Name}.html";
        }


    }
}
