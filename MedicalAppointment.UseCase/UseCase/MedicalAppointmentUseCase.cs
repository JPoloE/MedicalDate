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

        public async Task<Entity.Entities.MedicalAppointment> AgregateDoctor(Entity.Entities.MedicalAppointment Medical)
        {
            return await _medicalAppointmentRepository.InsertMedicalAsync(Medical);
        }

        public async Task<List<Entity.Entities.MedicalAppointment>> ListAllMedicals()
        {
            return await _medicalAppointmentRepository.GetAllMedicalsAsync();
        }

        public async Task<string> DeleteMedical(string IdMedical)
        {
            return await _medicalAppointmentRepository.DeleteMedicalByIdAsync(IdMedical);
        }

        public async Task<Entity.Entities.MedicalAppointment> UpdateMedical(Entity.Entities.MedicalAppointment Medical)
        {
            return await _medicalAppointmentRepository.UpdateMedicalAsync(Medical);
        }

    }
}
