using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        public IActionResult Get()
        {
            var songs = _songsRepo.GetSongs();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = HttpContext.User.Claims.First().Value;


            return Ok(songs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_songsRepo.GetSongById(id));
        }

        // GET api/values/5
        [HttpGet("user/{id}")]
        public IActionResult GetUserSongs(Guid id)
        {
            return Ok(_songsRepo.GetUserSongs(id));
        }

        //[Authorize]
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

                    var userId = HttpContext.User.Claims.First().Value;

                    var upload = _mapper.Map<Song>(song);
                    upload.UserId = Guid.Parse(userId);
                    upload.TimeStamp = DateTime.Now;
                    upload.Status = SongStatus.Pending;
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

        //[Authorize]
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var song = _songsRepo.GetSongById(id);
                var fileName = song.SongUrl.Split('/')[song.SongUrl.Split('/').Length - 1];

                if (song != null)
                {
                     var deletedSong = _songsRepo.DeleteSong(song.Id);
                    if (deletedSong != null)
                    {
                        var folderName = Path.Combine("Resources", "Songs");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        var file = new FileInfo(fullPath);

                        file.Delete();
                    }
                }

            }
        }

    }
}
