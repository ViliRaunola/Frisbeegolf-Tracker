using System;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Models;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Authorize]
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
            return await _mongoDBService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<List<User>> Get(string id)
        {
            return await _mongoDBService.GetUserAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user) 
        {
            await _mongoDBService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("game/{id}")]
        public async Task<IActionResult> AddToGames(string id, [FromBody] Game game)
        {
            await _mongoDBService.UpdateUserGame(id, game);
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
