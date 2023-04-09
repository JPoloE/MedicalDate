using MedicalAppointment.Entity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway.Repository
{
    public interface IMedicalAppointmentRepository
    {
        Task<InsertNewMedialAppointment> InsertMedicalAsync(InsertNewMedialAppointment Medical);
        Task<List<Entity.Entities.MedicalAppointment>> GetAllMedicalsAsync();
        Task<string> DeleteMedicalByIdAsync(int IdMedical);
        Task<Entity.Entities.MedicalAppointment> UpdateMedicalAsync(Entity.Entities.MedicalAppointment Medical);
    }
}
