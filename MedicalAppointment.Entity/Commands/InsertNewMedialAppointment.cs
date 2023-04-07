using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.Commands
{
    public class InsertNewMedialAppointment
    {
        public string Id_Patient { get; set; }
        public string Id_Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public string Specialty { get; set; }
    }
}
