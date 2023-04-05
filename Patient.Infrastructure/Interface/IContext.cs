using MongoDB.Driver;
using Patient.Infrastructure.MongoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Interface
{
    public interface IContext
    {
        public IMongoCollection <PatientEntity> Patients { get; }
    }
}
