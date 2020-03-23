using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SongLyrics.Communication.Models
{
    class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
