using System;
using System.Collections.Generic;
using System.Linq;
using StreetJams.Data;
using StreetJams.Entities;
using StreetJams.Services.Interfaces;

namespace StreetJams.Services.Repositories
{
    public class SongsRepository : ISongs
    {
        private readonly AppDbContext _db;

        public SongsRepository(AppDbContext db)
        {
            _db = db;
        }

        public Song GetSongById(Guid id)
        {
            var song = _db.Songs.FirstOrDefault(s => s.Id == id);
            return song;
        }

        public List<Song> GetSongs()
        {
            return _db.Songs.ToList();
        }

        public Song PostSong(Song song)
        {
            _db.Songs.Add(song);
            _db.SaveChanges();
            return song;
        }
    }
}
