using AutoMapper;
using MongoDB.Driver;
using Patient.Entity.Commands;
using Patient.Entity.Entity;
using Patient.Infrastructure.Interface;
using Patient.Infrastructure.MongoEntity;
using Patient.UseCase.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.PatientRepository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMongoCollection<PatientEntity> _collection;
        private readonly IMapper _mapper;


        public PatientRepository(IContext context, IMapper mapper)
        {
            this._collection = context.Patients;
            this._mapper = mapper;
        }


        public async Task<string> DeletePatientByIdAsync(string IdPatient)
        {
            var patient = await GetAllPatientsAsync();
            var patientDelete = patient.FirstOrDefault(patientList => patientList.Patient_Id == IdPatient);
            if(patientDelete != null)
            {
                patientDelete.State = false;
                await UpdatePatientAsync(patientDelete);
            }
            else
            {
                throw new ArgumentException($"There isn't a patient with this ID: {IdPatient}.");
            }
            return $"Delete Successfull for ID: {IdPatient}";
        }

        public async Task<List<Entity.Entity.Patient>> GetAllPatientsAsync()
        {
            var patients = await _collection.FindAsync(Builders<PatientEntity>.Filter.Empty);
            var listPatients = patients.ToEnumerable().Select(patient => _mapper.Map<Entity.Entity.Patient>(patient)).ToList();
            return listPatients;
        }

        public async Task<InsetNewPatient> InsertPatientAsync(InsetNewPatient newPatient)
        {
            var savePatient = _mapper.Map<PatientEntity>(newPatient);
            await _collection.InsertOneAsync(savePatient);
            return newPatient;
        }

        public async Task<Entity.Entity.Patient> UpdatePatientAsync(Entity.Entity.Patient patient)
        {
            var updatePatient = _mapper.Map<PatientEntity>(patient);
            var upPatient = await _collection.FindOneAndReplaceAsync(patientEntity => patientEntity.Patient_Id
            == patient.Patient_Id, updatePatient);
            return patient;
        }
    }
}
