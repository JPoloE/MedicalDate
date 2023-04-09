using MedicalAppointment.Infrastructure.Gateway;
using MedicalAppointment.Infrastructure;
using AutoMapper.Data;
using MedicalAppointment.AutoMapper;
using MedicalAppointment.UseCase.Gateway;
using MedicalAppointment.UseCase.UseCase;
using MedicalAppointment.UseCase.Gateway.Repository;
using MedicalAppointment.Infrastructure.Repository;
using Bank.AppService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(PerfilConfiguration));

builder.Services.AddScoped<IDoctorUseCase, DoctorUseCase>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

builder.Services.AddScoped<IMedicalAppointmentUseCase, MedicalAppointmentUseCase>();
builder.Services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();

builder.Services.AddTransient<IDbConnectionBuilder>(e =>
{
    return new DbConnectionBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
