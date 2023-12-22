using EntityFramework7Relationships.Data;
using EntityFramework7Relationships.DTOs;
using EntityFramework7Relationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework7Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TlouController : ControllerBase
    {

        private readonly DataContext _context;

        public TlouController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _context.Characters
                .Include(x => x.Backpack)
                .Include(x => x.Weapons)
                .Include(x => x.Factions).FirstOrDefaultAsync(c=>c.Id==id);

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDto request)
        {

            var newCharacter = new Character
            {
                Name = request.Name,
            };

            var backpack = new Backpack
            {
                Description = request.Backpack.Description,
                Character = newCharacter
            };
            var weapons = request.Weapons.Select(x => new Weapon
            {
                Name = x.Name,
                Character = newCharacter
            }).ToList();

            var factions= request.Factions.Select(x => new Faction
            {
                Name = x.Name,
                Character = new List<Character> { newCharacter }
            }).ToList();


            newCharacter.Backpack= backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions= factions;
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return Ok(await _context.Characters.Include(c=>c.Backpack).Include(x=>x.Weapons).ToListAsync());
        }

    }
}
