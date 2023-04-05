using Patient.Entity.Commands;
using Patient.UseCase.Gateway;
using Patient.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.UseCase.UseCase
{
    public class PatientUseCase : IPatientUseCase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<InsetNewPatient> AgregatePatient(InsetNewPatient newPatient)
        {
            return await _patientRepository.InsertPatientAsync(newPatient);
        }

        public async Task<string> DeletePatient(string IdPatient)
        {
            return await _patientRepository.DeletePatientByIdAsync(IdPatient);
        }

        public async Task<List<Entity.Entity.Patient>> ListAllPatients()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }

        public async Task<Entity.Entity.Patient> UpdatePatient(Entity.Entity.Patient patient)
        {
            return await _patientRepository.UpdatePatientAsync(patient);
        }
    }
}
