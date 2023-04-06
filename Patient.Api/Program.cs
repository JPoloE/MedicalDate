using AutoMapper.Data;
using Patient.Api.AutoMapper;
using Patient.Infrastructure.Interface;
using Patient.Infrastructure;
using Patient.UseCase.Gateway;
using Patient.UseCase.UseCase;
using Patient.UseCase.Gateway.Repository;
using Patient.Infrastructure.PatientRepository;
using Bank.AppService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));
builder.Services.AddSingleton<IContext>(provider => new Context(builder.Configuration.GetConnectionString("DefaultConnection"), "Patient"));

builder.Services.AddScoped<IPatientUseCase, PatientUseCase>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

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
