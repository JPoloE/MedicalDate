using AutoMapper;
using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using System.Runtime.CompilerServices;

namespace MedicalAppointment.AutoMapper
{
    public class PerfilConfiguration : Profile
    {
        public PerfilConfiguration() 
        {
            CreateMap<InsertNewDoctor, Doctor>().ReverseMap();
            CreateMap<InsertNewMedialAppointment, Doctor>().ReverseMap();
        }
        
        
    }
}
