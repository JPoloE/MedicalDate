using MedicalAppointment.Entity.Commands;
using MedicalAppointment.UseCase.Gateway;
using MedicalAppointment.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.UseCase
{
    public class MedicalAppointmentUseCase : IMedicalAppointmentUseCase
    {

        private readonly IMedicalAppointmentRepository _medicalAppointmentRepository;

        public MedicalAppointmentUseCase(IMedicalAppointmentRepository medicalAppointmentRepository)
        {
            _medicalAppointmentRepository = medicalAppointmentRepository;
        }

        public async Task<List<Entity.Entities.MedicalAppointment>> ListAllMedicals()
        {
            return await _medicalAppointmentRepository.GetAllMedicalsAsync();
        }

        public async Task<string> DeleteMedical(int IdMedical)
        {
            return await _medicalAppointmentRepository.DeleteMedicalByIdAsync(IdMedical);
        }

        public async Task<Entity.Entities.MedicalAppointment> UpdateMedical(Entity.Entities.MedicalAppointment Medical)
        {
            return await _medicalAppointmentRepository.UpdateMedicalAsync(Medical);
        }

        public async Task<InsertNewMedialAppointment> AgregateDoctor(InsertNewMedialAppointment Medical)
        {
            return await _medicalAppointmentRepository.InsertMedicalAsync(Medical);
        }

        public async Task<List<Entity.Entities.MedicalAppointment>> GetMedicalAppointmentsByDoctorAsync(int doctorId)
        {
            return await _medicalAppointmentRepository.GetMedicalAppointmentsByDoctorGroupedByDayAsync(doctorId);
        }

        public async Task<List<Entity.Entities.MedicalAppointment>> GetMedicalAppointmentsByPatientAsync(string patientId)
        {
            return await _medicalAppointmentRepository.GetMedicalAppointmentsByPatientGroupedByDayAsync(patientId);
        }
    }
}
