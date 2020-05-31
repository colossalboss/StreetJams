using System;
using System.Collections.Generic;
using System.IO;
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
            var songs = RemoveNotExistingSongs();

            return songs;
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

        private bool CheckIfSongExistInDirectory(Guid id)
        {
            var song = GetSongById(id);

            var path = Directory.GetCurrentDirectory();

            var musicFile = new FileInfo(Path.Combine(path, song.SongUrl));

            if (musicFile.Exists)
            {
                return true;
            }
            return false;
        }

        private List<Song> RemoveNotExistingSongs()
        {
            var songs = _db.Songs.ToList();

            var existing = new List<Song>();
            var missing = new List<Song>();

            foreach (var song in songs)
            {
                if (CheckIfSongExistInDirectory(song.Id))
                {
                    existing.Add(song);
                }
                else
                {
                    missing.Add(song);
                }
            }

            DeleteMissingSongsFromDb(missing);

            return existing;
        }

        private void DeleteMissingSongsFromDb(List<Song> songs)
        {
            if (songs.Any())
            {
                _db.Songs.RemoveRange(songs);
                _db.SaveChanges();
            }
        }
    }
}
