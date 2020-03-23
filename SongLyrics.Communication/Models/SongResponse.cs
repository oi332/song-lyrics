using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SongLyrics.Communication.Models
{
    public class SongResponse
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("artists")]
        public IList<Artist> Artists { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Artist
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
