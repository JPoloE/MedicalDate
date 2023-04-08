using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway.Repository
{
    public interface IMedicalAppointmentRepository
    {
        Task<Entity.Entities.MedicalAppointment> InsertMedicalAsync(Entity.Entities.MedicalAppointment Medical);
        Task<List<Entity.Entities.MedicalAppointment>> GetAllMedicalsAsync();
        Task<string> DeleteMedicalByIdAsync(string IdMedical);
        Task<Entity.Entities.MedicalAppointment> UpdateMedicalAsync(Entity.Entities.MedicalAppointment Medical);
    }
}
