namespace RecintosHabitacionales.Conexiones
{
    public class ConexionApi
    {
        public HttpClient Client { get; set; }
        public ConexionApi(HttpClient client)
        {
            Client = client;
        }
    }
}
