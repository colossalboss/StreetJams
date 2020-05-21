using System;
using System.Collections.Generic;
using StreetJams.Entities;

namespace StreetJams.Services.Interfaces
{
    public interface ISongs
    {

        List<Song> GetSongs();

        Song GetSongById(Guid id);

        Song PostSong(Song song);
    }
}
