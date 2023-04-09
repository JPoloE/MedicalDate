using MedicalAppointment.Entity.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.UseCase.Gateway
{
    public interface IMedicalAppointmentUseCase
    {
        Task<InsertNewMedialAppointment> AgregateDoctor(InsertNewMedialAppointment Medical);
        Task<List<Entity.Entities.MedicalAppointment>> ListAllMedicals();
        Task<string> DeleteMedical(int IdMedical);
        Task<Entity.Entities.MedicalAppointment> UpdateMedical(Entity.Entities.MedicalAppointment Medical);
    }
}
