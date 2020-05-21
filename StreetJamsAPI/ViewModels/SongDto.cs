using System;
using Microsoft.AspNetCore.Http;
using StreetJams.Entities;

namespace StreetJamsAPI.ViewModels
{
    public class SongDto
    {
        public SongDto()
        {
        }

        public Guid Id { get; set; }

        public IFormFile Title { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }

        public DateTime TimeStamp { get; set; }

        public SongStatus Status { get; set; }
    }
}
