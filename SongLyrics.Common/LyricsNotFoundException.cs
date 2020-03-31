using System;
using System.Collections.Generic;
using System.Text;

namespace SongLyrics.Common
{
    public class LyricsNotFoundException : Exception
    {
        public LyricsNotFoundException(string message): base(message) { }
    }
}
