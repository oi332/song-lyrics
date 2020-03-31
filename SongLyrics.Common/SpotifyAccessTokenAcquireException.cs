using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Common
{
    public class SpotifyAccessTokenAcquireException : Exception
    {
        public SpotifyAccessTokenAcquireException(string message) : base(message) { }
    }
}
