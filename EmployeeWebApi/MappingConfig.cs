using AutoMapper;
using EmployeeWebApi.Model;
using EmployeeWebApi.Model.DTO;

namespace EmployeeWebApi
{
    public class MappingConfig
    {
        public static  MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<EmployeeDTO,Employee>();
                config.CreateMap<Employee,EmployeeDTO>();
            });
            return mappingConfig;
        }
    }
}
