using Newtonsoft.Json;

namespace RecintosHabitacionales.Models
{
    public static class SesionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string datoObjeto = JsonConvert.SerializeObject(value, Formatting.Indented);
            session.SetString(key, datoObjeto);
            //session.SetObject(key, value);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            //var jsonString = session.GetObject<T>(key);

            //return jsonString;

            var jsonString = session.GetString(key);

            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            else
                return JsonConvert.DeserializeObject<T>(jsonString);

        }

    }//class

}
