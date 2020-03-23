using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongLyrics.Communication
{
    public interface ISpotifyClient
    {
        Task<SongResponse> GetCurrentPlayingSongAsync();
    }
}
