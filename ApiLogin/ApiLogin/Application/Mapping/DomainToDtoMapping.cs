using ApiLogin.Domain.DTOs;
using ApiLogin.Domain.Models;
using AutoMapper;

namespace ApiLogin.Application.Mapping
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<EmployeeModel, EmployeeDTO>()
                .ForMember(dest => dest.role, x => x.MapFrom(orig => orig.Roles))
                .ReverseMap();

            
        }
    }
}
