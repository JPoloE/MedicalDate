using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.DTO
{
    public class MedicalAppointmentDTO
    {
        public string Id_MedicalAppointment { get; set; }
        public int Id_Doctor { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public string Id_Patient { get; set; }
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public string Specialty { get; set; }
    }
}
