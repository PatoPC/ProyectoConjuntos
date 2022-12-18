using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Utilitarios
{
    public static class FuncionesContrasena
    {
        public static string encriptarContrasena(string contrasena)
        {
            SHA256Managed encriptado = new SHA256Managed();
            string encriptado1 = String.Empty;
            //Se cencripta la primera parte del codigo 
            byte[] crypto = encriptado.ComputeHash(Encoding.ASCII.GetBytes(contrasena), 0, Encoding.ASCII.GetByteCount(contrasena));
            foreach (byte theByte in crypto)
            {
                encriptado1 += theByte.ToString("x2");
            }

            //Se ve necesario encriptar dos veces con diferente multiplicador
            string encriptado2 = "";
            foreach (byte theByte in crypto)
            {
                encriptado2 += theByte.ToString("x4");
            }

            string contrasenaEncriptada = encriptado1.ToString() + encriptado2.ToString();

            //Se regresa las dos cadenas y se corda 10 caracteres para aumentar la complejidad.
            return contrasenaEncriptada.Substring(0, contrasenaEncriptada.Length - 10);
        }//encriptarContrasena


        public static string GenerarNuevaContrasena()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            string nuevaContrasena = new String(Charsarr);

            return nuevaContrasena;
        }

    }
}
