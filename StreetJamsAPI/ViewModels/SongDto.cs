using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public IFormFile Title { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string SongTitle { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public string ReleaseDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }

        public SongStatus Status { get; set; }
    }
}
