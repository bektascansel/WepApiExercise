using AutoMapper;
using NETCORE_7_WepApiProject.DTOs.Character;
using NETCORE_7_WepApiProject.Models;

namespace NETCORE_7_WepApiProject.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile() { 
        
                CreateMap<Character, GetCharacterDto>();
                CreateMap<AddCharacterDto, Character>();
                CreateMap<UpdateCharacterDto, Character>();
                CreateMap<UpdateCharacterDto, GetCharacterDto>();
        }
    }
}
