using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE_7_WepApiProject.DTOs.Character;
using NETCORE_7_WepApiProject.Models;
using NETCORE_7_WepApiProject.Services.CharacterService;

namespace NETCORE_7_WepApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {


        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService service)
        {

            _characterService = service;

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
        {

            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto character)
        {

            var reponse = await _characterService.UpdateCharacter(character);

            if (reponse.Data is null)
                return NotFound(reponse);

            return Ok(reponse);


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var reponse = await _characterService.DeleteCharacter(id);

            if (reponse.Data is null)
                return NotFound(reponse);

            return Ok(reponse);



        }
    }
}
