using SongLyrics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SongLyrics
{
    public static class SongNormalizer
    {
        public static Song Normalize(Song song)
        {
            song.Name = Normalize(SpotifyNormalize(song.Name));
            song.ArtistName = Normalize(song.ArtistName);

            return song;
        }

        public static string Normalize(string word)
        {
            return new Regex("\\W+").Replace(word.ToLower(), string.Empty);
        }

        public static string SpotifyNormalize(string word)
        {
            var phrases = new List<string> { "(with", "(feat", " -" };

            foreach (var phrase in phrases)
            {
                var i = word.IndexOf(phrase);

                if (i != -1)
                {
                    word = word.Substring(0, i);
                    break;
                }
            }

            return word;
        }
    }
}
