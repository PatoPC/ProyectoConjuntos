using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public static class Sesion<T> where T : class    
    {
        public static T recuperarSesion(ISession session, string nombreSesion)
        {
            try
            {
                //string json = session.GetString(nombreSesion);
                string json = "";
                var objSesion = JsonConvert.DeserializeObject<T>(json);

                return objSesion;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
