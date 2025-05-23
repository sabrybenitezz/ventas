using ConcesionariaVehiculos.Data;
using ConcesionariaVehiculos.Mapping;
using ConcesionariaVehiculos.Repositories;
using ConcesionariaVehiculos.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<ClienteDTOValidator>());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "API Vehiculos XL – Concesionaria",
    Version = "v1",
    Description = "Venta y Distribución de Autos",
    Contact = new OpenApiContact
    {
        Name = "Sabrina Benitez",
        Email = "tecnoabry@gmail.com"
    }
});


// Configuración de Entity Framework Core
builder.services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


// Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IServicioPosventaRepository, ServicioPosventaRepository>();

// Servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<ServicioPosventaService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Validaciones
builder.Services.AddValidatorsFromAssemblyContaining<ClienteDTOValidator>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configuración de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

