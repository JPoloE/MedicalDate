using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patient.Entity.Commands;
using Patient.UseCase.Gateway;

namespace Patient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly IPatientUseCase _patientUseCase;
        private readonly IMapper _mapper;

        public PatientController(IPatientUseCase patientUseCase, IMapper mapper)
        {
            _patientUseCase = patientUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Entity.Entity.Patient>> GetListPatient()
        {
            return await _patientUseCase.ListAllPatients();
        }

        [HttpPost]
        public async Task<Entity.Entity.Patient> SavePatient(InsertNewPatient patient)
        {
            return await _patientUseCase.AgregatePatient(_mapper.Map<Entity.Entity.Patient>(patient));
        }

        [HttpPut]
        public async Task<Entity.Entity.Patient>UpdatePatient(Entity.Entity.Patient patient)
        {
            return await _patientUseCase.UpdatePatient(_mapper.Map<Entity.Entity.Patient>(patient));
        }

        [HttpDelete]
        public async Task<string> DeletePatient(string idPatient)
        {
            return await _patientUseCase.DeletePatient(idPatient);
        }
    }
}
