using AutoMapper;
using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using MedicalAppointment.UseCase.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorUseCase _doctorUseCase;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorUseCase doctorUseCase, IMapper mapper)
        {
            _doctorUseCase = doctorUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Doctor>> getDoctors()
        {
            return await _doctorUseCase.ListAllDoctors();
        }

        [HttpPost]
        public async Task<InsertNewDoctor> RegisterDoctor(InsertNewDoctor doctor)
        {
            return await _doctorUseCase.AgregateDoctor(doctor);
        }
        
        [HttpDelete]
        public async Task<string> DeleteDoctor(int id)
        {
            return await _doctorUseCase.DeleteDoctor(id);
        }

        [HttpPut]
        public async Task<Doctor>UpdateDoctor(Doctor doctor)
        {
            return await _doctorUseCase.UpdateDoctor(doctor);
        }
       
    }
}
