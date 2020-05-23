using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace StreetJams.Entities
{
    public class Song
    {
        public Guid Id { get; set; }

        public string SongUrl { get; set; }

        public string Artist { get; set; }

        public string SongTitle { get; set; }

        public Guid UserId { get; set; }

        public string Genre { get; set; }

        public string ReleaseDate { get; set; }

        public DateTime TimeStamp { get; set; }

        public SongStatus Status { get; set; }
    }
}
