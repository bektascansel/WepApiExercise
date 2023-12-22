using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NETCORE_7_WepApiProject.Data;
using NETCORE_7_WepApiProject.DTOs.Character;
using NETCORE_7_WepApiProject.Models;

namespace NETCORE_7_WepApiProject.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
       

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext dataContext)
        {
            _mapper = mapper;
            _context = dataContext;
        }   


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        { 
            var serviceResponse= new ServiceResponse<List<GetCharacterDto>>();
            var characterr = _mapper.Map<Character>(character);
            await _context.Characters.AddAsync(characterr);
            serviceResponse.Data = _context.Characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            _context.SaveChanges();
            return serviceResponse;
        }

        public async  Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = _context.Characters.FirstOrDefault(x => x.Id == id);

                if (character is null)
                    throw new Exception($"Character with Id '{id}' is not found");

                _context.Characters.Remove(character);

                serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();   
                _context.SaveChanges();
              
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;



        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbcharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
                if (dbcharacter is null)
                    throw new Exception($"Character with Id '{id}' is not found");
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

            public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            
            try
            {
                var character = _context.Characters.FirstOrDefault(x => x.Id == updateCharacter.Id);

                if (character is null)
                    throw new Exception($"Character with Id '{updateCharacter.Id}' is not found");
                
  
                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligent = updateCharacter.Intelligent;
                character.Class = updateCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message=ex.Message;
            }
            return serviceResponse;




        }
    }
}
