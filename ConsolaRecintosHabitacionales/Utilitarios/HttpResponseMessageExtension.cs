using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<MensajesRespuesta> ExceptionResponse(this HttpResponseMessage httpResponseMessage)
        {
            string mensaje = "Error:";
            string responseContent = "";
            try
            {
                
                responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

                string mensajeInterior = "";

                var mensajeError = JsonConvert.DeserializeObject<MensajesRespuesta>(responseContent);

                if (mensajeError != null)
                {
                    if (!string.IsNullOrEmpty(mensajeError.message))
                        return mensajeError;
                }

                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if (result.errors != null)
                {
                    foreach (var value in result.errors)
                    {
                        try
                        {
                            if (value != null)
                            {
                                foreach (Newtonsoft.Json.Linq.JArray item in value)
                                {
                                    foreach (Newtonsoft.Json.Linq.JValue property in item)
                                    {
                                        mensajeInterior += property.ToString() + " <br/>";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                MensajesRespuesta objRespuesta = new MensajesRespuesta(mensaje, false, mensajeInterior, "error");

                return objRespuesta;
            }
            catch (Exception ex)
            {
                MensajesRespuesta objRespuesta = new MensajesRespuesta(mensaje, false, responseContent, "error");

                return objRespuesta;

            }

            return MensajesRespuesta.errorInesperado();
        }
    }



}