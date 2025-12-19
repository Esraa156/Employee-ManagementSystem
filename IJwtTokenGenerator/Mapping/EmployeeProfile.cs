using AutoMapper;
using DomainLayer.Entities;
using ApplicationLayer.DTOs;

namespace ApplicationLayer.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // DTO -> Entity
            CreateMap<EmployeeDto, Employee>();

            // Entity -> DTO
            CreateMap<Employee, EmployeeDto>();

            // Entity -> DTO
            CreateMap<Employee, GetAllEmployeeDto>()
     .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
     .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle.Name));

        }
    }
}
