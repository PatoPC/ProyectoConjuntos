using AutoMapper;
using DTOs.AreaComunal;
using DTOs.CatalogoGeneral;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ReservaAreas : Controller
    {
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IMapper _mapper;
        private readonly IServicioConsumoAPI<AreaComunalDTOEditar> _servicioConsumoAPIEditar;
        public C_ReservaAreas(IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IMapper mapper, IServicioConsumoAPI<AreaComunalDTOEditar> servicioConsumoAPIEditar)
        {
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _mapper = mapper;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
        }

        // GET: C_ReservaAreas
        public async Task<ActionResult> ReservaAreasComunales()
        {

            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarAreasComunalesPorIdConjunto + objUsuarioSesion.IdConjuntoDefault, HttpMethod.Get);
            List<AreaComunalDTOCompleto> listAreaComunal = new();
            if (respuesta.IsSuccessStatusCode)
                listAreaComunal = await LeerRespuestas<List<AreaComunalDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if(listAreaComunal.Count() > 0) 
            {
                ViewData["listaAreasComunales"] = new SelectList(listAreaComunal, "IdAreaComunal", "NombreArea");
            }
            else 
            {
                
                ViewData["listaAreasComunales"] = "";
            }
            ViewData["ListaConjustosAcceso"] = objUsuarioSesion.ConjutosAccesoSelect;


            return View();
        }
          

        
    

      
    }
}
