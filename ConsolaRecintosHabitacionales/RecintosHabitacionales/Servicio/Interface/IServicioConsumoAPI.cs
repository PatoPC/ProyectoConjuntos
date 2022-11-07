namespace RecintosHabitacionales.Servicio.Interface
{
    public interface IServicioConsumoAPI<T> where T : class
    {
        public Task<HttpResponseMessage> consumoAPI(string urlEndPoint, HttpMethod tipoMetodo, T obj = null);
    }
}