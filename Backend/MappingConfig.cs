using AutoMapper;
using EmployeesBE.Models;
using EmployeesBE.Models.DTO;

namespace EmployeesBE
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
