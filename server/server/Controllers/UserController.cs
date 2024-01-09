using System;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Models;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Cors;

namespace server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly UsersService _mongoDBService;

        public UserController(UsersService mongoDBService) =>
            _mongoDBService = mongoDBService;
        

        [HttpGet]
        public async Task<List<User>> Get() 
        {
            Console.WriteLine(HttpContext.Items["User"]);
            return await _mongoDBService.GetAsync();
        }

        [HttpGet("{sub}")]
        public async Task<List<User>> Get(string sub)
        {
            // TODO: Add a check that the user can only access their own data
            return await _mongoDBService.GetUserAsync(sub);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user) 
        {
            Console.WriteLine(user);
            await _mongoDBService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("game/{sub}")]
        public async Task<IActionResult> AddToGames(string sub, [FromBody] Game game)
        {
            await _mongoDBService.UpdateUserGame(sub, game);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
