using AutoMapper;
using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using MedicalAppointment.UseCase.Gateway;
using MedicalAppointment.UseCase.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalAppointmentController : ControllerBase
    {
        private readonly IMedicalAppointmentUseCase _medicalUseCase;
        private readonly IMapper _mapper;

        public MedicalAppointmentController(IMedicalAppointmentUseCase medicalUseCase, IMapper mapper)
        {
            _medicalUseCase = medicalUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Entity.Entities.MedicalAppointment>> getMedicals()
        {
            return await _medicalUseCase.ListAllMedicals();
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<List<Entity.Entities.MedicalAppointment>> GetMedicalAppointmentsByDoctorGroupedByDay(int doctorId)
        {
            return await _medicalUseCase.GetMedicalAppointmentsByDoctorAsync(doctorId);
        }

        [HttpGet("paciente/{patientId}")]
        public async Task<List<Entity.Entities.MedicalAppointment>> GetMedicalAppointmentsByPatientGroupedByDay(string patientId)
        {
            return await _medicalUseCase.GetMedicalAppointmentsByPatientAsync(patientId);
        }

        [HttpPost]
        public async Task<InsertNewMedialAppointment> RegisterMEdical(InsertNewMedialAppointment medical)
        {
            return await _medicalUseCase.AgregateDoctor(medical);
        }

        [HttpDelete]
        public async Task<string>DeleteMedical(int idMedical)
        {
            return await _medicalUseCase.DeleteMedical(idMedical);
        }

        [HttpPut]
        public async Task<Entity.Entities.MedicalAppointment> updateMedical(Entity.Entities.MedicalAppointment medical)
        {
            return await _medicalUseCase.UpdateMedical(medical);
        }
    }
}
