using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Entity.Entity
{
    public class Patient
    {
        public string Patient_Id { get; set; }
        public string Fire_Id { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public decimal Card_Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool State { get; set; }
        public int Role { get; set; }

    }
}
