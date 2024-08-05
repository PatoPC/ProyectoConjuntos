using DTOs.Adeudo;
using DTOs.AreaComunal;
using DTOs.CatalogoGeneral;
using DTOs.Comunicado;
using DTOs.ConfiguracionCuenta;
using DTOs.Conjunto;
using DTOs.Contabilidad;
using DTOs.Departamento;
using DTOs.Logs;
using DTOs.MaestroContable;
using DTOs.Parametro;
using DTOs.Persona;
using DTOs.Proveedor;
using DTOs.ReservaArea;
using DTOs.Roles;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecintosHabitacionales.Conexiones;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Rotativa.AspNetCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.TryAddScoped(typeof(IServicioConsumoAPI<>), typeof(ServicioConsumoAPI<>));


builder.Services.AddMvc()
        .AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.AddScoped(typeof(ConjuntoDTOCrear));
builder.Services.AddScoped(typeof(ConjuntoDTOCrearArchivo));
builder.Services.AddScoped(typeof(List<ConjuntoDTOCrear>));
builder.Services.AddScoped(typeof(List<ConjuntoDTOCrearArchivo>));
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
builder.Services.AddScoped(typeof(TipoPersonaDTO));
builder.Services.AddScoped(typeof(ObjTipoPersonaDepartamento));

builder.Services.AddScoped(typeof(CatalogoDTODropDown));
builder.Services.AddScoped(typeof(CatalogoDTOCrear));
builder.Services.AddScoped(typeof(CatalogoDTOCrear));
builder.Services.AddScoped(typeof(CatalogoDTOActualizar));
builder.Services.AddScoped(typeof(CatalogoDTOPaginaDropDown));

builder.Services.AddScoped(typeof(ObjetoBusquedaUsuarios));
builder.Services.AddScoped(typeof(UsuarioDTOCompleto));
builder.Services.AddScoped(typeof(UsuarioDTOCrear));
builder.Services.AddScoped(typeof(UsuarioDTOEditar));
builder.Services.AddScoped(typeof(UsuarioConjuntoDTO));
builder.Services.AddScoped(typeof(List<UsuarioConjuntoDTO>));
builder.Services.AddScoped(typeof(UsuarioResultadoBusquedaDTO));
builder.Services.AddScoped(typeof(UsuarioCambioContrasena));
builder.Services.AddScoped(typeof(OlvideMicontrasenaDTO));

builder.Services.AddScoped(typeof(ResultadoBusquedaConjuntos));
builder.Services.AddScoped(typeof(RolDTOCrear));
builder.Services.AddScoped(typeof(RolDTOBusqueda));
builder.Services.AddScoped(typeof(RolDTOEditar));


builder.Services.AddScoped(typeof(ProveedorDTOCrear));
builder.Services.AddScoped(typeof(ProveedorDTOEditar));
builder.Services.AddScoped(typeof(ProveedorDTOCompleto));
builder.Services.AddScoped(typeof(BusquedaProveedor));

builder.Services.AddScoped(typeof(LogBusqueda));

builder.Services.AddScoped(typeof(MaestroContableDTOCrear));
builder.Services.AddScoped(typeof(MaestroContableDTOCompleto));
builder.Services.AddScoped(typeof(MaestroContableBusqueda));
builder.Services.AddScoped(typeof(List<MaestroContableDTOCrear>));

builder.Services.AddScoped(typeof(ParametroCrearDTO));
builder.Services.AddScoped(typeof(GenerarAdeudo));
builder.Services.AddScoped(typeof(List<AdeudoDTOCrear>));

builder.Services.AddScoped(typeof(ComunicadoDTOCrear));
builder.Services.AddScoped(typeof(ComunicadoDTOEditar));
builder.Services.AddScoped(typeof(BusquedaComunicadoDTO));

builder.Services.AddScoped(typeof(AreaComunalDTOCrear));
builder.Services.AddScoped(typeof(BusquedaAreaComunal));
builder.Services.AddScoped(typeof(AreaComunalDTOEditar));

builder.Services.AddScoped(typeof(BusquedaParametro));
builder.Services.AddScoped(typeof(ParametroEditarDTO));

builder.Services.AddScoped(typeof(ConfiguraCuentasDTOCrear));
builder.Services.AddScoped(typeof(ConfiguraCuentasDTOEditar));

builder.Services.AddScoped(typeof(ReservaAreaDTOCrear));
builder.Services.AddScoped(typeof(ReservaAreaDTOEditar));
builder.Services.AddScoped(typeof(ReservaAreaDTOCompleto));
builder.Services.AddScoped(typeof(CargarMaestroContable));

builder.Services.AddScoped(typeof(EncabezContDTOCrear));
builder.Services.AddScoped(typeof(BusquedaContabilidad));
builder.Services.AddScoped(typeof(AdeudoDTOEditar));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ConexionApi>(client =>
{
    //Desarrollo
    client.BaseAddress = new Uri("http://localhost:5181");

    //Producción    
    //client.BaseAddress = new Uri("http://DESKTOP-26QEGBC\\SQLEXPRESS/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.UseRotativa();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=C_Ingreso}/{action=Ingresar}/{id?}");

app.Run();

//Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, "../Rotativa");

