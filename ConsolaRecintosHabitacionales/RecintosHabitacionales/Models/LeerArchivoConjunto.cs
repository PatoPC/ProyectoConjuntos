using DTOs.Conjunto;
using SpreadsheetLight;
using Utilitarios;

namespace RecintosHabitacionales.Models
{
    public class LeerArchivoConjunto
    {
        public static async Task<List<ModeloArchivoConjunto>> procesarArchivoExcel(IFormFile archivo)
        {
            DateTime fechaActual = DateTime.Now;
            List<ModeloArchivoConjunto> listaArchivoLeido = new List<ModeloArchivoConjunto>();

            try
            {
                string rutaCompletaIncluidaMeses = ConstantesAplicacion.rutaArchivosLectura;
                FuncionesUtiles.validarRuta(rutaCompletaIncluidaMeses);

                rutaCompletaIncluidaMeses += fechaActual.ToString("yyyy") + @"\";
                Console.WriteLine("Validando " + rutaCompletaIncluidaMeses);
                FuncionesUtiles.validarRuta(rutaCompletaIncluidaMeses);

                rutaCompletaIncluidaMeses += fechaActual.ToString("MM") + @"\";
                Console.WriteLine("Validando " + rutaCompletaIncluidaMeses);
                FuncionesUtiles.validarRuta(rutaCompletaIncluidaMeses);

                string nuevoNombreArchivo = fechaActual.ToString("dd-MM-yyyy") + archivo.FileName;

                rutaCompletaIncluidaMeses += nuevoNombreArchivo;

                await FuncionesUtiles.recuperarBytes(rutaCompletaIncluidaMeses, archivo);

                SLDocument documentoLeido = new SLDocument(rutaCompletaIncluidaMeses);

                listaArchivoLeido = leerArchivoExcel(documentoLeido);
            }
            catch (Exception ex)
            {

            }

            return listaArchivoLeido;
        }

        private static List<ModeloArchivoConjunto> leerArchivoExcel(SLDocument documentoLeido)
        {
            List<ModeloArchivoConjunto> listaDatosDocumento = new();
            int contadorVacios = 0;
            for (int numFila = 2; ; numFila++)
            {
                try
                {
                    ModeloArchivoConjunto objDocumento = new ModeloArchivoConjunto();

                    objDocumento.Nombre_Conjunto = documentoLeido.GetCellValueAsString(numFila, 1).Trim();
                    objDocumento.RUC = documentoLeido.GetCellValueAsString(numFila, 2).Trim();
                    objDocumento.Correo_Conjunto = documentoLeido.GetCellValueAsString(numFila, 3).Trim();
                    objDocumento.Telefono = documentoLeido.GetCellValueAsString(numFila, 4).Trim();
                    objDocumento.Dirección = documentoLeido.GetCellValueAsString(numFila, 5).Trim();
                    objDocumento.Torre = documentoLeido.GetCellValueAsString(numFila, 6).Trim();
                    objDocumento.Departamento = documentoLeido.GetCellValueAsString(numFila, 7).Trim();
                    objDocumento.Metros_Cuadrados = FuncionesUtiles.convertirADecimal(documentoLeido.GetCellValueAsString(numFila, 8).Replace(".", ","));
                    objDocumento.Valor_Alicuota = FuncionesUtiles.convertirADecimal(documentoLeido.GetCellValueAsString(numFila, 9).Replace(".", ","));
                    objDocumento.Saldo_Inicial = FuncionesUtiles.convertirADecimal(documentoLeido.GetCellValueAsString(numFila, 10).Replace(".", ","));
                    objDocumento.Tipo_Identificación_Condomino = documentoLeido.GetCellValueAsString(numFila, 11).Trim();
                    objDocumento.Numero_Identificación_Condomino = documentoLeido.GetCellValueAsString(numFila, 12).Trim();
                    objDocumento.Nombre_Condomino = documentoLeido.GetCellValueAsString(numFila, 13).Trim();
                    objDocumento.Apellido_Condomino = documentoLeido.GetCellValueAsString(numFila, 14).Trim();
                    objDocumento.Telefono_Condomino = documentoLeido.GetCellValueAsString(numFila, 15).Trim();
                    objDocumento.Celular_Condomino = documentoLeido.GetCellValueAsString(numFila, 16).Trim();
                    objDocumento.Correo_Condomino = documentoLeido.GetCellValueAsString(numFila, 17).Trim();
                    objDocumento.Observación_Condomino = documentoLeido.GetCellValueAsString(numFila, 18).Trim();
                    objDocumento.Tipo_Identificación_Propietario = documentoLeido.GetCellValueAsString(numFila, 19).Trim();
                    objDocumento.Numero_Identificación_Propietario = documentoLeido.GetCellValueAsString(numFila, 20).Trim();
                    objDocumento.Nombre_Propietario = documentoLeido.GetCellValueAsString(numFila, 21).Trim();
                    objDocumento.Apellido_Propietario = documentoLeido.GetCellValueAsString(numFila, 22).Trim();
                    objDocumento.Telefono_Propietario = documentoLeido.GetCellValueAsString(numFila, 23).Trim();
                    objDocumento.Celular_Propietario = documentoLeido.GetCellValueAsString(numFila, 24).Trim();
                    objDocumento.Correo_Propietario = documentoLeido.GetCellValueAsString(numFila, 25).Trim();
                    objDocumento.Observacion_Propietario = documentoLeido.GetCellValueAsString(numFila, 26);

                    if (string.IsNullOrEmpty(objDocumento.Nombre_Conjunto))
                    {
                        contadorVacios++;
                    }
                    else
                    {
                        listaDatosDocumento.Add(objDocumento);
                    }
                    
                    if(contadorVacios > 4)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {

                }

            }

            return listaDatosDocumento;
        }


    }
}
