using ConjuntosEntidades.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RepositorioConjuntos.Implementacion;
using RepositorioConjuntos.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextoDB_Condominios>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionConjuntos"]));


builder.Services.TryAddScoped(typeof(IManageConjuntosCRUD<>), typeof(ManageConjuntosCRUD<>));
builder.Services.AddScoped<IManageConjuntos, ManageConjuntos>();
builder.Services.AddScoped<IManageTorre, ManageTorre>();
builder.Services.AddScoped<IManageDepartamento, ManageDepartamento>();
builder.Services.AddScoped<IManagePersona, ManagePersona>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//services.AddAutoMapper(typeof(Startup));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
