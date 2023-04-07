﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Entity.Entities
{
    public class DoctorWithMedicalAppointment
    {
        public string Id_Doctor { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<DoctorWithMedicalAppointment> Medical { get; set; }
    }
}
