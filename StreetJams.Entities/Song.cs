using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace StreetJams.Entities
{
    public class Song
    {
        public Guid Id { get; set; }

        [Display(Name = "Song Name")]
        [NotMappedAttribute]
        public IFormFile SongName { get; set; }
    }
}
