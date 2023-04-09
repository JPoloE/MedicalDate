using MedicalAppointment.Entity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway
{
    public interface IDoctorUseCase
    {
        Task<InsertNewDoctor> AgregateDoctor(InsertNewDoctor Doctor);
        Task<List<Entity.Entities.Doctor>> ListAllDoctors();
        Task<string> DeleteDoctor(int IdDoctor);
        Task<Entity.Entities.Doctor> UpdateDoctor(Entity.Entities.Doctor Doctor);
        Task<string> ChangeDoctorState(int doctorId);
    }
}
