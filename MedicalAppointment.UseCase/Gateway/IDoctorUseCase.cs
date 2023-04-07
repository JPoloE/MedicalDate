using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway
{
    public interface IDoctorUseCase
    {
        Task<Entity.Entities.Doctor> AgregateDoctor(Entity.Entities.Doctor Doctor);
        Task<List<Entity.Entities.Doctor>> ListAllDoctors();
        Task<string> DeleteDoctor(string IdDoctor);
        Task<Entity.Entities.Doctor> UpdateDoctor(Entity.Entities.Doctor Doctor);
    }
}
