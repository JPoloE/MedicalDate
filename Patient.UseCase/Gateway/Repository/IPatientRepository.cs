using Patient.Entity.Commands;
using Patient.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.UseCase.Gateway.Repository
{
    public interface IPatientRepository
    {
        Task<Entity.Entity.Patient>InsertPatientAsync(Entity.Entity.Patient newPatient);
        Task<List<Entity.Entity.Patient>> GetAllPatientsAsync();
        Task<string> DeletePatientByIdAsync(string IdPatient);
        Task<Entity.Entity.Patient> UpdatePatientAsync(Entity.Entity.Patient patient);
    }
}
