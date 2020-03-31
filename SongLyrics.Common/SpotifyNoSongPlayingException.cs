using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Common
{
    public class SpotifyNoSongPlayingException : Exception
    {
        public SpotifyNoSongPlayingException(string message) : base(message) { }
    }
}
