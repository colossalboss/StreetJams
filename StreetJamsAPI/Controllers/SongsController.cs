using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreetJams.Entities;
using StreetJams.Services.Interfaces;
using StreetJamsAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreetJamsAPI.Controllers
{
    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        private readonly ISongs _songsRepo;
        private readonly IMapper _mapper;

        public SongsController(IMapper mapper, ISongs songs)
        {
            _songsRepo = songs;
            _mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var songs = _songsRepo.GetSongs();
            return Ok(songs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_songsRepo.GetSongById(id));
        }

        // POST api/values
        //[HttpPost]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post([FromForm] SongDto song)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Songs");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var upload = _mapper.Map<Song>(song);
                    upload.SongUrl = $"https://localhost:5001/{dbPath}";
                    _songsRepo.PostSong(upload);

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
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
