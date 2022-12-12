using DTOs.Conjunto;
using DTOs.Departamento;
using DTOs.Persona;
using DTOs.Torre;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecintosHabitacionales.Conexiones;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.TryAddScoped(typeof(IServicioConsumoAPI<>), typeof(ServicioConsumoAPI<>));

builder.Services.AddScoped(typeof(ConjuntoDTOCrear));
builder.Services.AddScoped(typeof(ConjuntoDTOCompleto));
builder.Services.AddScoped(typeof(ResultadoBusquedaConjuntos));
builder.Services.AddScoped(typeof(BusquedaConjuntos));
builder.Services.AddScoped(typeof(ConjuntoDTOEditar));

builder.Services.AddScoped(typeof(TorreDTOCrear));
builder.Services.AddScoped(typeof(TorreDTOEditar));
builder.Services.AddScoped(typeof(BusquedaTorres));
builder.Services.AddScoped(typeof(DepartamentoDTOCrear));
builder.Services.AddScoped(typeof(DepartamentoDTOCompleto));
builder.Services.AddScoped(typeof(DepartamentoDTOEditar));
builder.Services.AddScoped(typeof(DepartamentoBusquedaDTO));

builder.Services.AddScoped(typeof(PersonaDTOCrear));
builder.Services.AddScoped(typeof(PersonaDTOEditar));
builder.Services.AddScoped(typeof(PersonaDTOCompleto));
builder.Services.AddScoped(typeof(ObjetoBusquedaPersona));
//builder.Services.AddScoped(typeof(ConexionApi));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddHttpClient<ConexionApi>(client =>
{
   //Desarrollo
    client.BaseAddress = new Uri("http://localhost:5186");

    //Producción
    //client.BaseAddress = new Uri("http://localhost");
    //client.BaseAddress = new Uri("http://181.39.23.39");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=C_Persona}/{action=AdministrarPersona}/{id?}");

app.Run();
