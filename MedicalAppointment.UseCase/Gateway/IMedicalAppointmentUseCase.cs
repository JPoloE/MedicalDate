using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway
{
    public interface IMedicalAppointmentUseCase
    {
        Task<Entity.Entities.MedicalAppointment> AgregateDoctor(Entity.Entities.MedicalAppointment Medical);
        Task<List<Entity.Entities.MedicalAppointment>> ListAllMedicals();
        Task<string> DeleteMedical(string IdMedical);
        Task<Entity.Entities.MedicalAppointment> UpdateMedical(Entity.Entities.MedicalAppointment Medical);
    }
}
