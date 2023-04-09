using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.DTO
{
    public class DoctorDTO
    {
        public int Id_Doctor { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public List<DoctorAppointmentDTO> Appointments { get; set; }
    }
}
