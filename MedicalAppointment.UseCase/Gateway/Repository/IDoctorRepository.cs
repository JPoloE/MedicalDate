using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway.Repository
{
    public interface IDoctorRepository
    {
        Task<Entity.Entities.Doctor> InsertDoctorAsync(Entity.Entities.Doctor Doctor);
        Task<List<Entity.Entities.Doctor>> GetAllDoctorsAsync();
        Task<string> DeleteDoctorByIdAsync(string IdDoctor);
        Task<Entity.Entities.Doctor> UpdateDoctorAsync(Entity.Entities.Doctor Doctor);
    }
}
