using SongLyrics.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongLyrics.Application
{
    public interface ISongService
    { 
        Task<Song> GetCurrentlyPlayingSongAsync();
    }
}
