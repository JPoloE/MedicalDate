﻿using Ardalis.GuardClauses;
using Dapper;
using MedicalAppointment.Entity.Commands;
using MedicalAppointment.Entity.Entities;
using MedicalAppointment.Infrastructure.Gateway;
using MedicalAppointment.UseCase.Gateway.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Infrastructure.Repository
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string nombreTabla = "MedicalAppointment";

        public MedicalAppointmentRepository(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<string> DeleteMedicalByIdAsync(int IdMedical)
        {
            using (var conn = await _dbConnectionBuilder.CreateConnectionAsync())
            {

                // Verificar si la cita existe
                var sql = "SELECT COUNT(*) FROM MedicalAppointment WHERE Id_MedicalAppointment = @IdMedical";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { IdMedical });

                if (count == 0)
                {
                    throw new Exception("La cita médica especificada no existe.");
                }

                // Eliminar la cita de la base de datos
                sql = "DELETE FROM MedicalAppointment WHERE Id_MedicalAppointment = @IdMedical";
                await conn.ExecuteAsync(sql, new { IdMedical });

                conn.Close();
            }

            return "La cita médica se ha eliminado correctamente.";
        }

        public async Task<List<Entity.Entities.MedicalAppointment>> GetAllMedicalsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Guard.Against.Null(connection, nameof(connection));

            string query = $"SELECT * FROM {nombreTabla}";
            var resultado = await connection.QueryAsync<Entity.Entities.MedicalAppointment>(query);

            connection.Close();

            return resultado.ToList();
        }

        public async Task<InsertNewMedialAppointment> InsertMedicalAsync(InsertNewMedialAppointment Medical)
        {
            using (var conn = await _dbConnectionBuilder.CreateConnectionAsync())
            {
                // Verificar si ya existe una cita para la misma fecha y hora con el mismo médico
                var sql = "SELECT COUNT(*) FROM MedicalAppointment WHERE Id_Doctor = @Id_Doctor AND Date = @Date";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { Id_Doctor = Medical.Id_Doctor, Date = Medical.Date });

                if (count > 0)
                {
                    throw new Exception("Ya existe una cita reservada para esta fecha y hora con el mismo médico.");
                }

                // Verificar si el médico ya tiene 8 citas en el mismo día
                sql = "SELECT COUNT(*) FROM MedicalAppointment WHERE Id_Doctor = @Id_Doctor AND CONVERT(date, Date) = CONVERT(date, @Date)";
                count = await conn.ExecuteScalarAsync<int>(sql, new { Id_Doctor = Medical.Id_Doctor, Date = Medical.Date });

                if (count >= 8)
                {
                    throw new Exception("Este médico ya tiene 8 citas reservadas para este día.");
                }

                // Verificar si ya hay una cita reservada para el mismo día y una hora anterior
                var horaAnterior = Medical.Date.AddHours(-1);
                sql = "SELECT COUNT(*) FROM MedicalAppointment WHERE Id_Doctor = @Id_Doctor AND CONVERT(date, Date) = CONVERT(date, @Date) AND Date BETWEEN @HoraAnterior AND @Date";
                count = await conn.ExecuteScalarAsync<int>(sql, new { Id_Doctor = Medical.Id_Doctor, Date = Medical.Date, HoraAnterior = horaAnterior });

                if (count > 0)
                {
                    throw new Exception("Debe haber al menos una hora entre cada cita del mismo médico.");
                }

                // Insertar la nueva cita en la tabla "Citas"
                sql = "INSERT INTO MedicalAppointment (Id_Patient, Id_Doctor, Date, Reason, Details, Specialty) VALUES ( @Id_Patient, @Id_Doctor, @Date, @Reason, @Details, @Specialty); SELECT SCOPE_IDENTITY()";
                var id = await conn.ExecuteScalarAsync<int>(sql, new { Id_Patient = Medical.Id_Patient, Id_Doctor = Medical.Id_Doctor, Date = Medical.Date, Reason = Medical.Reason, Details = Medical.Details, Specialty = Medical.Specialty});

                // Obtener la cita recién creada de la base de datos
                sql = "SELECT * FROM MedicalAppointment WHERE Id_MedicalAppointment = @Id_MedicalAppointment";
                var cita = await conn.QueryFirstOrDefaultAsync<Entity.Entities.MedicalAppointment>(sql, new { Id_MedicalAppointment = id });
                conn.Close();

                return Medical;
            }
        }

    

            public Task<Entity.Entities.MedicalAppointment> UpdateMedicalAsync(Entity.Entities.MedicalAppointment Medical)
        {
            throw new NotImplementedException();
        }
    }
}
