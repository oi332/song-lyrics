using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Application.Models
{
    public static class BaseSongExtensions
    {
        public static BaseSong ToEntity(this SongResponse songResponse)
        {
            return new BaseSong
            {
                Name = songResponse.Item.Name,
                ArtistName = songResponse.Item.Artists?[0].Name,
            };
        }

        public static SongRequest ToRequest(this BaseSong song)
        {
            return new SongRequest
            {
                Name = song.Name,
                ArtistName = song.ArtistName
            };
        }

        public static Song ToSong(this BaseSong song, LyricsResponse lyricsResponse)
        {
            return new Song
            {
                Name = song.Name,
                ArtistName = song.ArtistName,
                Lyrics = lyricsResponse.Lyrics,
            };
        }

    }
}
