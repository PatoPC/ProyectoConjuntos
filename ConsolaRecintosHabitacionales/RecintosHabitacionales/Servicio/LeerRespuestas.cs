using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Servicio
{
    public static class LeerRespuestas<T> where T : class
    {
        public static async Task<T> procesarRespuestasConsultas(HttpResponseMessage respuestaCatalogo)
        {
            string respuestaString = "";

            if (respuestaCatalogo.IsSuccessStatusCode)
            {
                MemoryStream memoryContentStream = new();
                memoryContentStream.Seek(0, SeekOrigin.Begin);

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    try
                    {
                        respuestaString = await respuestaCatalogo.Content.ReadAsStringAsync();
                        var listaRespuesta = JsonConvert.DeserializeObject<T>(respuestaString);

                        return listaRespuesta;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return default;
        }

        public static async Task<MensajesRespuesta> procesarRespuestaCRUD(HttpResponseMessage respuesta, string controlador = "", string accion = "", bool eliminar = false)
        {
            MensajesRespuesta objMensajeRespuesta = new MensajesRespuesta();
            try
            {
                if (respuesta == null)
                {
                    objMensajeRespuesta = MensajesRespuesta.errorConexion();
                    return objMensajeRespuesta;
                }


                if (respuesta.IsSuccessStatusCode && !eliminar)
                {

                    if (!string.IsNullOrEmpty(controlador) && !string.IsNullOrEmpty(accion))
                    {
                        objMensajeRespuesta = MensajesRespuesta.guardarOK(controlador, accion);
                    }
                    else
                    {
                        objMensajeRespuesta = MensajesRespuesta.guardarOK();
                    }
                    return objMensajeRespuesta;
                }
                else if (respuesta.IsSuccessStatusCode && eliminar)
                {

                    if (!string.IsNullOrEmpty(controlador) && !string.IsNullOrEmpty(accion))
                    {
                        objMensajeRespuesta = MensajesRespuesta.eliminadoOK(controlador, accion);
                    }
                    else
                    {
                        objMensajeRespuesta = MensajesRespuesta.eliminadoOK();
                    }

                    return objMensajeRespuesta;
                }
                else
                {
                    try
                    {
                        var createdContent = await respuesta.Content.ReadAsStringAsync();
                        objMensajeRespuesta = JsonConvert.DeserializeObject<MensajesRespuesta>(createdContent);

                        if (objMensajeRespuesta.title == "Not Acceptable")
                        {
                            objMensajeRespuesta = MensajesRespuesta.errorInesperado();
                        }
                    }
                    catch (Exception)
                    {
                        objMensajeRespuesta = await respuesta.ExceptionResponse();
                    }
                    return objMensajeRespuesta;
                }
            }
            catch (Exception ex)
            {
            }
            return MensajesRespuesta.errorInesperado();
        }

        public static async Task<SelectList> cargarListaDropDownGenerico(IServicioConsumoAPI<T> servicioConsumoAPI, string urlAPI, string id, string value, Guid? valorSeleccionado = null)
        {
            HttpResponseMessage restapuestaCatalogo = await servicioConsumoAPI.consumoAPI(urlAPI, HttpMethod.Get);

            T listaCatalogos = await procesarRespuestasConsultas(restapuestaCatalogo);
            //var listaCatalogos = JsonConvert.DeserializeObject<T>(responseJSON);

            SelectList objSelectList = new SelectList((IEnumerable<T>)listaCatalogos, id, value, valorSeleccionado);

            return objSelectList;
        }

       

    }
}
