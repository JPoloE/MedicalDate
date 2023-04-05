using AutoMapper;
using Patient.Entity.Commands;
using Patient.Entity.Entity;
using Patient.Infrastructure.MongoEntity;

namespace Patient.Api.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile() 
        {
            CreateMap<InsertNewPatient, Entity.Entity.Patient>().ReverseMap();
            CreateMap<PatientEntity, Entity.Entity.Patient>().ReverseMap();
        }
    }
}
