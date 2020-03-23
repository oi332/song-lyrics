using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongLyrics.Communication
{
    public interface ILyricsClient
    {
        Task<LyricsResponse> GetLyricsAsync(SongRequest songRequest);
    }
}
