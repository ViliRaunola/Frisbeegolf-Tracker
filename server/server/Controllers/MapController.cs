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
    public class MapController : ControllerBase
    {
        private readonly MapsService _mongoDBService;

        public MapController(MapsService mongoDBService) =>
            _mongoDBService = mongoDBService;


        [HttpGet]
        public async Task<List<Map>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<List<Map>> Get(string id)
        {
            return await _mongoDBService.GetMapAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Map map)
        {
            await _mongoDBService.CreateAsync(map);
            return CreatedAtAction(nameof(Get), new { id = map.Id }, map);
        }
    }
}
