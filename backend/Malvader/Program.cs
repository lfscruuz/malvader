using Malvader.DAO;
using Malvader.DAOs;
using Malvader.Models;
using Malvader.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new DbConnectionFactory(connectionString));
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AgenciaService>();

//DAOs
builder.Services.AddScoped<UsuarioDAO>();
builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<FuncionarioDAO>();
builder.Services.AddScoped<AgenciaDAO>();
builder.Services.AddScoped<EnderecoAgenciaDAO>();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

app.MapControllers();

app.Run();
