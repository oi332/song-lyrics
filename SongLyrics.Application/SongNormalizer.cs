using SongLyrics.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SongLyrics.Application
{
    public static class SongNormalizer
    {
        public static BaseSong Normalize(BaseSong song)
        {
            var baseSong = new BaseSong();

            baseSong.Name = Normalize(SpotifyNormalize(song.Name));
            baseSong.ArtistName = Normalize(song.ArtistName);

            return baseSong;
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
