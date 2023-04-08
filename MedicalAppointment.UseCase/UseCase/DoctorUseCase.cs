using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using MedicalAppointment.UseCase.Gateway;
using MedicalAppointment.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.UseCase
{
    public class DoctorUseCase : IDoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<InsertNewDoctor> AgregateDoctor(InsertNewDoctor Doctor)
        {
            return await _doctorRepository.InsertDoctorAsync(Doctor);
        }

        public async Task<string> DeleteDoctor(int IdDoctor)
        {
            return await _doctorRepository.DeleteDoctorByIdAsync(IdDoctor);
        }

        public async Task<List<Doctor>> ListAllDoctors()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<Doctor> UpdateDoctor(Doctor Doctor)
        {
            return await _doctorRepository.UpdateDoctorAsync(Doctor);
        }
    }
}
