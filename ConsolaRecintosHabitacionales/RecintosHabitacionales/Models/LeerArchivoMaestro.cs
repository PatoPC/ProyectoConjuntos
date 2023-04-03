using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.MaestroContable.Archivo;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using SpreadsheetLight;
using Utilitarios;

namespace RecintosHabitacionales.Models
{
    public class LeerArchivoMaestro
    {
        public static async Task<List<ModeloArchivoMaestro>> procesarArchivoExcel(IFormFile archivo, string usuarioCreacion)
        {
            DateTime fechaActual = DateTime.Now;
            List<ModeloArchivoMaestro> listaArchivoLeido = new List<ModeloArchivoMaestro>();

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

                listaArchivoLeido = await leerArchivoExcelMaestro(documentoLeido, usuarioCreacion);
            }
            catch (Exception ex)
            {

            }

            return listaArchivoLeido;
        }

        private async static Task<List<ModeloArchivoMaestro>> leerArchivoExcelMaestro(SLDocument documentoLeido, string usuarioCreacion)
        {
            List<ModeloArchivoMaestro> listaDatosDocumento = new();
            int contadorVacios = 0;
            for (int numFila = 2; ; numFila++)
            {
                try
                {
                    ModeloArchivoMaestro objDocumento = new ModeloArchivoMaestro();

                    objDocumento.ctacont = documentoLeido.GetCellValueAsString(numFila, 1).Trim();
                    objDocumento.nom_cuenta = documentoLeido.GetCellValueAsString(numFila, 2).Trim();
                    objDocumento.grupo = documentoLeido.GetCellValueAsString(numFila, 3).Trim();

                    if (string.IsNullOrEmpty(objDocumento.ctacont))
                        contadorVacios++;
                    else
                        listaDatosDocumento.Add(objDocumento);


                    if (contadorVacios > 4)
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
