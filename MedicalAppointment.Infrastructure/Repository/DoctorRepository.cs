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
                Name = Doctor.Name,
                Last_Name = Doctor.Last_Name,
                Specialty = Doctor.Specialty,
                Phone = Doctor.Phone,
                Email = Doctor.Email,
                State = Doctor.State
            };

            // Insertar la nueva cuenta y obtener su Id_Doctor.
            string insertDoctorQuery = $"INSERT INTO {nombreTabla} (Name, Last_Name, Specialty, Phone, Email, State) " +
                $"VALUES (@Name, @Last_Name, @Specialty, @Phone, @Email, @State); " +
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
                sql = "UPDATE Doctor SET Name = @Name, Last_Name = @Last_Name, Specialty = @Specialty, Phone = @Phone, Email = @Email, State = @State WHERE Id_Doctor = @Id_Doctor";
                await conn.ExecuteAsync(sql, new { Name = Doctor.Name, Last_Name = Doctor.Last_Name, Specialty = Doctor.Specialty, Phone = Doctor.Phone, Email = Doctor.Email, State = Doctor.State, Id_Doctor = Doctor.Id_Doctor });

                // Cargar los datos actualizados del doctor
                sql = "SELECT * FROM Doctor WHERE Id_Doctor = @Id_Doctor";
                var updatedDoctor = await conn.QueryFirstOrDefaultAsync<Doctor>(sql, new { Id_Doctor = Doctor.Id_Doctor });

                return updatedDoctor;
            }
        }
    }
}
