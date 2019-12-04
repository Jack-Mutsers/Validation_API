using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace ValidatorApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Validation, ValidationDto>();
            CreateMap<ValidationForCreationDto, Validation>();
            
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
