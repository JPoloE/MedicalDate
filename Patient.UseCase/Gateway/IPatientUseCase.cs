using Patient.Entity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.UseCase.Gateway
{
    public interface IPatientUseCase
    {
        Task<InsetNewPatient> AgregatePatient(InsetNewPatient newPatient);
        Task<List<Entity.Entity.Patient>> ListAllPatients();
        Task<string> DeletePatient(string IdPatient);
        Task<Entity.Entity.Patient> UpdatePatient(Entity.Entity.Patient patient);
    }
}
