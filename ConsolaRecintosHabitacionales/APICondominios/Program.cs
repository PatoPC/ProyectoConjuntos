using ConjuntosEntidades.Entidades;
using EntidadesCatalogos.Entidades;
using EntidadesPapelera.Entidades;
using GestionLogs.Entidades;
using GestionLogs.Implementacion;
using GestionUsuarioDB.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RepositorioCatalogos.Implementacion;
using RepositorioCatalogos.Interface;
using RepositorioConjuntos.Implementacion;
using RepositorioConjuntos.Interface;
using RepositorioGestionUsuarios.Implementacion;
using RepositorioGestionUsuarios.Interface;
using RepositorioLogs.Interface;
using RepositorioPapelera.Implementacion;
using RepositorioPapelera.Interface;
using RepositorioProveedores.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextoDB_Condominios>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionConjuntos"]));
builder.Services.AddDbContext<ContextoDB_Permisos>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionPermisos"]));
builder.Services.AddDbContext<ContextoDB_Catalogos>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionCatalogos"]));
builder.Services.AddDbContext<ContextoDB_Papelera>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionPapelera"]));
builder.Services.AddDbContext<ContextoDB_Logs>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionLogs"]));


builder.Services.TryAddScoped(typeof(IManageConjuntosCRUD<>), typeof(ManageConjuntosCRUD<>));
builder.Services.TryAddScoped(typeof(IManageCRUDPermisos<>), typeof(ManageCRUDPermisos<>));
builder.Services.TryAddScoped(typeof(IManageCRUDCatalogo<>), typeof(ManageCRUDCatalogo<>));
builder.Services.TryAddScoped(typeof(IManageCRUDPapelera<>), typeof(ManageCRUDPapelera<>));

builder.Services.AddScoped<IManageConjuntos, ManageConjuntos>();
builder.Services.AddScoped<IManageTorre, ManageTorre>();
builder.Services.AddScoped<IManageDepartamento, ManageDepartamento>();
builder.Services.AddScoped<IManagePersona, ManagePersona>();
builder.Services.AddScoped<IManageConsultasUsuario, ManageConsultasUsuario>();
builder.Services.AddScoped<IManageLogError, ManageLogError>();
builder.Services.AddScoped<IManageConsultasCatalogos, ManageConsultasCatalogos>();
builder.Services.AddScoped<IManageConsultasPermisos, ManageConsultasPermisos>();
builder.Services.AddScoped<IManageProveedor, ManageProveedor>();
builder.Services.AddScoped<IManageConMST, ManageConMST>();
builder.Services.AddScoped<IManageAdeudo, ManageAdeudo>();
builder.Services.AddScoped<IManageComunicado, ManageComunicado>();
        

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
