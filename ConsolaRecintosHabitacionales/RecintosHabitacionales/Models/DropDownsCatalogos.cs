using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;


namespace RecintosHabitacionales.Models
{
    public static class DropDownsCatalogos<T> where T : class
    {
        public static async Task<SelectList> cargarListaDropDownGenerico(IServicioConsumoAPI<T> servicioConsumoAPI, string urlAPI, string id, string value, Guid? valorSeleccionado = null)
        {
            HttpResponseMessage restapuestaCatalogo = await servicioConsumoAPI.consumoAPI(urlAPI, HttpMethod.Get);

            List<T> listaCatalogos = await LeerRespuestas<List<T>>.procesarRespuestasConsultas(restapuestaCatalogo);
            //var listaCatalogos = JsonConvert.DeserializeObject<T>(responseJSON);

            SelectList objSelectList = new SelectList(listaCatalogos, id, value, valorSeleccionado);

            return objSelectList;
        }

        public static async Task<List<T>> procesarRespuestasConsultaCatlogoObjeto(IServicioConsumoAPI<T> servicioConsumoAPI, string urlAPI)
        {
            HttpResponseMessage restapuestaCatalogo = await servicioConsumoAPI.consumoAPI(urlAPI, HttpMethod.Get);

            List<T> objCatalogo = await LeerRespuestas<List<T>>.procesarRespuestasConsultas(restapuestaCatalogo);

            return objCatalogo;
        }


    }
}
