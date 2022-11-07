using Marvin.StreamExtensions;
using Newtonsoft.Json;
using RecintosHabitacionales.Conexiones;
using RecintosHabitacionales.Servicio.Interface;
using System.Net.Http.Headers;

namespace RecintosHabitacionales.Servicio.Implementar
{
    public class ServicioConsumoAPI<T> : IServicioConsumoAPI<T> where T : class
    {
        private ConexionApi _conexion;
        private T _obj;

        public ServicioConsumoAPI(T obj, ConexionApi conexion)
        {
            this._obj = obj;
            _conexion = conexion;
        }

        public async Task<HttpResponseMessage> consumoAPI(string urlEndPoint, HttpMethod tipoMetodo, T obj = null)
        {
            MemoryStream memoryContentStream = new MemoryStream();
            if (obj != null)
            {
                memoryContentStream.SerializeToJsonAndWrite(obj, new System.Text.UTF8Encoding(), 1024, true);
                //BORRAR
                var temp = JsonConvert.SerializeObject(obj);
            }

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(tipoMetodo, urlEndPoint))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    try
                    {
                        request.Content = streamContent;
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        return await _conexion.Client.SendAsync(request);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return null;
        }

    }
}
