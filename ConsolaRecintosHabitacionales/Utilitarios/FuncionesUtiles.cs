using DTOs.Select;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class FuncionesUtiles
    {
        public static string GenerarCadena()
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var cadena = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                int indiceCaracter = random.Next(caracteresPermitidos.Length);
                char caracterAleatorio = caracteresPermitidos[indiceCaracter];
                cadena.Append(caracterAleatorio);
            }

            return cadena.ToString();
        }

        public static string construirUsuarioAuditoria(UsuarioSesionDTO objUsuarioSesion)
        {
            try
            {
                string nombreUsuario = objUsuarioSesion.IdUsuario + ";" + objUsuarioSesion.Nombre + " " + objUsuarioSesion.Apellido;

                return nombreUsuario;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string primeraLetraMayuscula(string original)
        {
            string nuevoTexto = "";
            if (original == null)
                return null;

            string[] arregloTemporal = original.Split(" ");

            foreach (string palabra in arregloTemporal)
            {
                if (original.Length > 1)
                    nuevoTexto += char.ToUpper(original[0]) + original.Substring(1) + " ";
            }

            return nuevoTexto.Trim();
        }

        public static string consturirNombreUsuario(UsuarioSesionDTO objUsuarioSesion)
        {
            try
            {
                string[] nombreTemporal = objUsuarioSesion.Nombre.Split(' ');
                string[] apellidoTemporal = objUsuarioSesion.Apellido.Split(' ');

                string nombreUsuarioTemporal = nombreTemporal[0] + " " + apellidoTemporal[0];

                return nombreUsuarioTemporal;
            }
            catch (Exception ex)
            {
            }

            return objUsuarioSesion.Nombre + " " + objUsuarioSesion.Apellido;
        }

        public static List<ObjetoSelectDropDown> crearListaOpcionesEnBlanco = new()
        {
            new(
                id: "No se recuperar con el ID",
                texto: "Error. No recupero información."
                )
        };

        public static int CalcularEdadPersona(DateTime fechaNacimientoPersona)
        {
            int anioNacimiento = fechaNacimientoPersona.Year;
            int anioActual = DateTime.Today.Year;
            int edadPersona = 0;
            int aniosDiferencia = anioActual - anioNacimiento;

            DateTime fechaNacimientoCalculo = new DateTime(DateTime.Today.Year, fechaNacimientoPersona.Month, fechaNacimientoPersona.Day);

            TimeSpan diasFechaNacimiento = new DateTime(DateTime.Today.Year, 1, 1).Subtract(fechaNacimientoCalculo);
            TimeSpan diasFechaActual = new DateTime(DateTime.Today.Year, 1, 1).Subtract(DateTime.Today);

            if (Math.Abs(diasFechaNacimiento.Days) <= Math.Abs(diasFechaActual.Days))
            {
                edadPersona = aniosDiferencia;
            }
            else
            {
                edadPersona = (aniosDiferencia - 1);
            }

            return edadPersona;
        }//CarlcularEdadPersona

        public static bool validacionNumeroCedula(string cedula, string tipoIdentificacion)
        {
            bool bandera = false;
            double isNumeric;
            var total = 0;
            const int tamanoLongitudCedula = 10;
            int[] coeficientesCedula = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            const int numeroProvincias = 24;
            const int tercerDigito = 9;

            try
            {

                if (tipoIdentificacion.ToUpper() == ConstantesAplicacion.IdentificacionCedula.ToUpper())
                {
                    if (double.TryParse(cedula, out isNumeric) && cedula.Length == tamanoLongitudCedula)
                    {
                        var provincia = Convert.ToInt32(string.Concat(cedula[0], cedula[1], string.Empty));
                        var digitoTres = Convert.ToInt32(cedula[2] + string.Empty);
                        //30 El digito 30 se utiliza para ecuatorianos nacidos en el extranjero
                        if (((provincia > 0 && provincia <= numeroProvincias) || provincia == 30) && digitoTres < tercerDigito)
                        {
                            var digitoVerificadorRecibido = Convert.ToInt32(cedula[9] + string.Empty);

                            for (var i = 0; i < coeficientesCedula.Length; i++)
                            {
                                var valor = Convert.ToInt32(coeficientesCedula[i] + string.Empty) * Convert.ToInt32(cedula[i] + string.Empty);
                                total = valor >= 10 ? total + (valor - 9) : total + valor;
                            }//for

                            //Luego a la suma de todos los resultdos le restamos de la decena superior
                            var digitoVerificadorObtenido = (((total / 10) + 1) * 10) - total;

                            if (digitoVerificadorObtenido >= 10)
                            {
                                digitoVerificadorObtenido = 0;
                            }

                            if (digitoVerificadorObtenido == digitoVerificadorRecibido)
                                bandera = true;

                            return bandera;
                        }//if
                    }//if
                }//cedula
                else
                {
                    bandera = true;
                }

            }
            catch (Exception ex)
            {

            }

            return bandera;
        }//IsValid

        public static decimal convertirADecimal(string dato)
        {
            try
            {
                decimal valor = Convert.ToDecimal(dato.Replace(".", ","));

                return valor;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static short convertirAShort(string dato)
        {
            try
            {
                short numero = Convert.ToInt16(dato);

                return numero;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static async Task<string> recuperarBytes(string rutaCompleta, IFormFile imagenFile)
        {
            using (Stream inputStream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await imagenFile.CopyToAsync(inputStream);

                byte[] array = new byte[inputStream.Length];
                inputStream.Seek(0, SeekOrigin.Begin);
                inputStream.Read(array, 0, array.Length);
            }

            return rutaCompleta;
        }

        public static void validarRuta(string ruta)
        {
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
                Console.WriteLine("Se creo " + ruta);
            }
            else
            {
                Console.WriteLine("Ya existe");
            }

        }

        public static string quitarTildes(string texto)
        {
            try
            {
                string sinTildes = texto.Normalize(NormalizationForm.FormD).Replace("\u0301", string.Empty);

                return sinTildes;
            }
            catch (Exception ex)
            {

            }

            return texto;
        }

        public static DateTime ObtenerUltimoDiaDelMes(int mes, int año)
        {
            // Crear una instancia de DateTime con el primer día del mes siguiente
            DateTime primerDiaDelMesSiguiente = new DateTime(año, mes, 1).AddMonths(1);

            // Restar un día a la fecha obtenida para obtener el último día del mes actual
            DateTime ultimoDiaDelMesActual = primerDiaDelMesSiguiente.AddDays(-1);

            return ultimoDiaDelMesActual;
        }
    }
}
