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

        public Song DeleteSong(Guid id)
        {
            var song = _db.Songs.FirstOrDefault(s => s.Id == id);
            _db.Remove(song);
            _db.SaveChanges();
            return song;
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

        public List<Song> GetUserSongs(Guid id)
        {
            return _db.Songs.Where(s => s.UserId == id).ToList();
        }

        public Song PostSong(Song song)
        {
            _db.Songs.Add(song);
            _db.SaveChanges();
            return song;
        }
    }
}
