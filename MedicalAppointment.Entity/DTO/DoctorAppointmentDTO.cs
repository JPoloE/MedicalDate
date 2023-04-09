using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.DTO
{
    public class DoctorAppointmentDTO
    {
        public int Id_MedicalAppointment { get; set; }
        public int Id_Patient { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public string AppointmentSpecialty { get; set; }
    }
}
