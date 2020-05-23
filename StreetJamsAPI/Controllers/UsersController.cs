using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreetJams.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreetJamsAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.Claims.First().Value;

            var user = await userManager.FindByIdAsync(userId);
            return Ok(user);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
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
