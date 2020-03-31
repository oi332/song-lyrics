using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Common
{
    public class SpotifyAccessTokenExpiredException : Exception
    {
        public SpotifyAccessTokenExpiredException(string message) : base(message) { }
    }
}
