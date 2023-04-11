using Ardalis.GuardClauses;
using Dapper;
using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using MedicalAppointment.Infrastructure.Gateway;
using MedicalAppointment.UseCase.Gateway.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "Doctor";

        public DoctorRepository(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<string> ChangeDoctorStateAsync(int doctorId)
        {
            using (var conn = await _dbConnectionBuilder.CreateConnectionAsync())
            {
                var doctor = await conn.QuerySingleOrDefaultAsync<Doctor>("SELECT * FROM Doctor WHERE Id_Doctor = @doctorId", new { doctorId });

                if (doctor == null) // El doctor no existe en la base de datos
                {
                    return "El doctor no existe en la base de datos";
                }

                doctor.State = !doctor.State; // Cambiar el estado del doctor

                var rowsAffected = await conn.ExecuteAsync("UPDATE Doctor SET State = @State WHERE Id_Doctor = @doctorId", new { doctor.State, doctorId });

                if (rowsAffected == 0) // No se afectó ninguna fila
                {
                    return "No se pudo cambiar el estado del doctor";
                }

                return $"El doctor ha sido {(doctor.State ? "activado" : "desactivado")} exitosamente";
            }
        }

        public async Task<string> DeleteDoctorByIdAsync(int IdDoctor)
        {
            using (var conn = await _dbConnectionBuilder.CreateConnectionAsync())
            {
                // Verificar si el doctor tiene citas pendientes
                var sql = "SELECT COUNT(*) FROM MedicalAppointment WHERE Id_Doctor = @IdDoctor AND Date > GETDATE()";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { IdDoctor });

                if (count > 0)
                {
                    return "No se puede eliminar el doctor porque tiene citas pendientes.";
                }

                // Realizar el borrado lógico del doctor
                sql = $"UPDATE {nombreTabla} SET State = 0 WHERE Id_Doctor = @IdDoctor";
                await conn.ExecuteAsync(sql, new { IdDoctor });

                conn.Close();

                return "El doctor se ha eliminado con éxito.";
            }

        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Doctor>(query);

            connection.Close();
            return resultado.ToList();
        }

        public async Task<InsertNewDoctor> InsertDoctorAsync(InsertNewDoctor Doctor)
        {
            Guard.Against.Null(Doctor, nameof(Doctor));
            Guard.Against.NullOrEmpty(Doctor.Name, nameof(Doctor.Name), "Name Required. ");
            Guard.Against.NullOrEmpty(Doctor.Last_Name, nameof(Doctor.Last_Name), "Last Name Required. ");
            Guard.Against.NullOrEmpty(Doctor.Specialty, nameof(Doctor.Specialty), "Specialty Required. ");
            Guard.Against.NullOrEmpty(Doctor.Phone, nameof(Doctor.Phone), "Phone required. ");
            Guard.Against.NullOrEmpty(Doctor.Email, nameof(Doctor.Email), "Email required. ");
            Guard.Against.NullOrEmpty(Doctor.State.ToString(), nameof(Doctor.State), "State required. ");

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var insertNewDoctor = new
            {
                Id_Fire = Doctor.Id_Fire,
                Name = Doctor.Name,
                Last_Name = Doctor.Last_Name,
                Specialty = Doctor.Specialty,
                Phone = Doctor.Phone,
                Email = Doctor.Email,
                State = Doctor.State,
                Role = Doctor.Role
            };

            // Verificar si existe un doctor con el mismo correo electrónico
            string emailQuery = $"SELECT COUNT(*) FROM {nombreTabla} WHERE Email = @Email";
            int count = await connection.ExecuteScalarAsync<int>(emailQuery, new { Email = Doctor.Email });

            if (count > 0)
            {
                throw new Exception("Ya existe un doctor con el mismo correo electrónico.");
            }

            // Insertar la nueva cuenta y obtener su Id_Doctor.
            string insertDoctorQuery = $"INSERT INTO {nombreTabla} (Id_Fire, Name, Last_Name, Specialty, Phone, Email, State, Role) " +
                $"VALUES (@Id_Fire, @Name, @Last_Name, @Specialty, @Phone, @Email, @State, @Role); " +
                $"SELECT SCOPE_IDENTITY();";
            int doctor_Id = await connection.ExecuteScalarAsync<int>(insertDoctorQuery, insertNewDoctor);

            connection.Close();

            return Doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor Doctor)
        {
            using (var conn = await _dbConnectionBuilder.CreateConnectionAsync())
            {
                // Verificar si existe un doctor con el mismo correo electrónico
                var sql = "SELECT COUNT(*) FROM Doctor WHERE Email = @Email AND Id_Doctor <> @Id_Doctor";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { Email = Doctor.Email, Id_Doctor = Doctor.Id_Doctor });

                if (count > 0)
                {
                    throw new Exception("Ya existe un doctor con el mismo correo electrónico.");
                }

                // Actualizar los datos del doctor
                sql = "UPDATE Doctor SET Id_Fire = @Id_Fire, Name = @Name, Last_Name = @Last_Name, Specialty = @Specialty, Phone = @Phone, Email = @Email, State = @State, Role = @Role WHERE Id_Doctor = @Id_Doctor";
                await conn.ExecuteAsync(sql, new { Id_Fire = Doctor.Id_Fire, Name = Doctor.Name, Last_Name = Doctor.Last_Name, Specialty = Doctor.Specialty, Phone = Doctor.Phone, Email = Doctor.Email, State = Doctor.State, Role = Doctor.Role, Id_Doctor = Doctor.Id_Doctor });

                // Cargar los datos actualizados del doctor
                sql = "SELECT * FROM Doctor WHERE Id_Doctor = @Id_Doctor";
                var updatedDoctor = await conn.QueryFirstOrDefaultAsync<Doctor>(sql, new { Id_Doctor = Doctor.Id_Doctor });

                return updatedDoctor;
            }
        }
    }
}
