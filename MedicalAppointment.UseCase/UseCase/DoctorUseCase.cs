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

        public Task<Doctor> AgregateDoctor(Doctor Doctor)
        {
            return _doctorRepository.InsertDoctorAsync(Doctor);
        }

        public Task<string> DeleteDoctor(string IdDoctor)
        {
            return _doctorRepository.DeleteDoctorByIdAsync(IdDoctor);
        }

        public Task<List<Doctor>> ListAllDoctors()
        {
            return _doctorRepository.GetAllDoctorsAsync();
        }

        public Task<Doctor> UpdateDoctor(Doctor Doctor)
        {
            return _doctorRepository.UpdateDoctorAsync(Doctor);
        }
    }
}
