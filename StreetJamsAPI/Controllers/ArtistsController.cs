using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreetJams.Data;
using StreetJams.Services.Interfaces;
using StreetJamsAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreetJamsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper _mapper;
        private readonly ISongs _songsRepo;

        public ArtistsController(UserManager<AppUser> userManager, IMapper mapper, ISongs songs)
        {
            this.userManager = userManager;
            _mapper = mapper;
            _songsRepo = songs;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var artist = userManager.Users.ToList();

            var model = _mapper.Map<List<ArtistViewModel>>(artist);

            return Ok(AddSongCounts(model));
        }

        private int CountSongs(Guid id)
        {
            var songs = _songsRepo.GetSongs();
            var count = 0;
            foreach(var song in songs)
            {
                if (song.UserId == id)
                {
                    count += 1;
                }
            }
            return count;
        }

        private List<ArtistViewModel> AddSongCounts(List<ArtistViewModel> model)
        {
            foreach(var artist in model)
            {
                artist.NumberOfSongs = CountSongs(artist.Id);
            }
            return model;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var song = _songsRepo.GetSongById(Guid.Parse("3f89f23e-36c7-4bb6-8367-3b4db9c85867"));

            var path = Directory.GetCurrentDirectory();

            var musicFile = new FileInfo(Path.Combine(path, song.SongUrl));

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
