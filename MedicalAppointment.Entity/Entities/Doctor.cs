using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.Entities
{
    public class Doctor
    {
        public int Id_Doctor { get; set; }
        public string Id_Fire { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool State { get; set; }
        public int Role { get; set; }
    }
}
