using Ardalis.GuardClauses;
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

            var result = (patientDelete != null)
                    ? (patientDelete.State = false, await UpdatePatientAsync(patientDelete))
                    : throw new ArgumentException($"There isn't a patient with this ID: {IdPatient}.");

            return $"Delete Successfull for ID: {IdPatient}";
        }

        public async Task<List<Entity.Entity.Patient>> GetAllPatientsAsync()
        {
            var filter = Builders<PatientEntity>.Filter.Eq(paciente => paciente.State, true);
            var patients = await _collection.FindAsync(filter);
            var listPatients = patients.ToEnumerable().Select(patient => _mapper.Map<Entity.Entity.Patient>(patient)).ToList();
            return listPatients;
        }

        public async Task<Entity.Entity.Patient> InsertPatientAsync(Entity.Entity.Patient newPatient)
        {
            Guard.Against.Null(newPatient, nameof(newPatient));
            Guard.Against.NullOrEmpty(newPatient.Name, nameof(newPatient.Name), "Name Required. ");
            Guard.Against.NullOrEmpty(newPatient.Fire_Id, nameof(newPatient.Fire_Id), "Id Required. ");
            Guard.Against.NullOrEmpty(newPatient.Last_Name, nameof(newPatient.Last_Name), "Last Name Required. ");
            Guard.Against.NullOrEmpty(newPatient.Card_Id.ToString(), nameof(newPatient.Card_Id), "Card Id required. ");
            Guard.Against.NullOrEmpty(newPatient.Email, nameof(newPatient.Email), "Email required. ");
            Guard.Against.NullOrEmpty(newPatient.Address, nameof(newPatient.Address), "Address required. ");
            Guard.Against.NullOrEmpty(newPatient.Phone, nameof(newPatient.Phone), "Phone required. ");
            Guard.Against.NullOrEmpty(newPatient.Role.ToString(), nameof(newPatient.Role), "Role required. ");

            var savePatient = _mapper.Map<PatientEntity>(newPatient);
            await _collection.InsertOneAsync(savePatient);
            return newPatient;
        }

        public async Task<Entity.Entity.Patient> UpdatePatientAsync(Entity.Entity.Patient patient)
        {
            Guard.Against.Null(patient, nameof(patient));
            Guard.Against.NullOrEmpty(patient.Name, nameof(patient.Name), "Name Required. ");
            Guard.Against.NullOrEmpty(patient.Last_Name, nameof(patient.Last_Name), "Last Name Required. ");
            Guard.Against.NullOrEmpty(patient.Card_Id.ToString(), nameof(patient.Card_Id), "Card Id required. ");
            Guard.Against.NullOrEmpty(patient.Email, nameof(patient.Email), "Email required. ");
            Guard.Against.NullOrEmpty(patient.Address, nameof(patient.Address), "Address required. ");
            Guard.Against.NullOrEmpty(patient.Phone, nameof(patient.Phone), "Phone required. ");
            Guard.Against.NullOrEmpty(patient.Role.ToString(), nameof(patient.Role), "Role required. ");

            var updatePatient = _mapper.Map<PatientEntity>(patient);
            var upPatient = await _collection.FindOneAndReplaceAsync(patientEntity => patientEntity.Patient_Id
            == patient.Patient_Id, updatePatient);
            return patient;
        }
    }
}
