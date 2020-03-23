using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Models
{
    public static class SongExtensions
    {
        public static Song ToEntity(this SongResponse songResponse)
        {
            return new Song
            {
                Name = songResponse.Item.Name,
                ArtistName = songResponse.Item.Artists?[0].Name,
            };
        }

        public static SongRequest ToRequest(this Song song)
        {
            return new SongRequest
            {
                Name = song.Name,
                ArtistName = song.ArtistName
            };
        }
    }
}
