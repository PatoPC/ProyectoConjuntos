using DTOs.AreasDepartamento;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using SpreadsheetLight;
using Utilitarios;

namespace RecintosHabitacionales.Models
{
    public class LeerArchivoConjunto
    {
        public static async Task<List<ModeloArchivoConjunto>> procesarArchivoExcel(IFormFile archivo, IServicioConsumoAPI<CatalogoDTOCrear> _consumo, string usuarioCreacion)
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

                listaArchivoLeido = await leerArchivoExcel(documentoLeido, _consumo, usuarioCreacion);
            }
            catch (Exception ex)
            {

            }

            return listaArchivoLeido;
        }

        private async static Task<List<ModeloArchivoConjunto>> leerArchivoExcel(SLDocument documentoLeido, IServicioConsumoAPI<CatalogoDTOCrear> _servicioConsumoAPI, string usuarioCreacion)
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
                    objDocumento.Tipo_Identificacion_Condomino = documentoLeido.GetCellValueAsString(numFila, 11).Trim();
                    objDocumento.Numero_Identificacion_Condomino = documentoLeido.GetCellValueAsString(numFila, 12).Trim();
                    objDocumento.Nombre_Condomino = documentoLeido.GetCellValueAsString(numFila, 13).Trim();
                    objDocumento.Apellido_Condomino = documentoLeido.GetCellValueAsString(numFila, 14).Trim();
                    objDocumento.Telefono_Condomino = documentoLeido.GetCellValueAsString(numFila, 15).Trim();
                    objDocumento.Celular_Condomino = documentoLeido.GetCellValueAsString(numFila, 16).Trim();
                    objDocumento.Correo_Condomino = documentoLeido.GetCellValueAsString(numFila, 17).Trim();
                    objDocumento.Observación_Condomino = documentoLeido.GetCellValueAsString(numFila, 18).Trim();
                    objDocumento.Tipo_Identificacion_Propietario = documentoLeido.GetCellValueAsString(numFila, 19).Trim();
                    objDocumento.Numero_Identificacion_Propietario = documentoLeido.GetCellValueAsString(numFila, 20).Trim();
                    objDocumento.Nombre_Propietario = documentoLeido.GetCellValueAsString(numFila, 21).Trim();
                    objDocumento.Apellido_Propietario = documentoLeido.GetCellValueAsString(numFila, 22).Trim();
                    objDocumento.Telefono_Propietario = documentoLeido.GetCellValueAsString(numFila, 23).Trim();
                    objDocumento.Celular_Propietario = documentoLeido.GetCellValueAsString(numFila, 24).Trim();
                    objDocumento.Correo_Propietario = documentoLeido.GetCellValueAsString(numFila, 25).Trim();
                    objDocumento.Observacion_Propietario = documentoLeido.GetCellValueAsString(numFila, 26);                    

                    try
                    {
                        //Se valida si existen extras del departamento, (terraza,bodega, estacionamiento), etc
                        string existeExtra = documentoLeido.GetCellValueAsString(numFila, 27);

                        int cantidadExtras = Convert.ToInt32(existeExtra);
                        int posicionActual = 27;

                        if (cantidadExtras > 0)
                        {
                            //List<AreasDepartamentoDTO> AreasDepartamentos = new();

                            for (int i = 1; i < cantidadExtras + 1; i += 2)
                            {
                                posicionActual += 1;
                                string nombreCatalogo = documentoLeido.GetCellValueAsString(numFila, posicionActual);

                                HttpResponseMessage respuestaTipoExtra = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getNombreExactoCatalogo + nombreCatalogo, HttpMethod.Get);
                                var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaTipoExtra);
                                posicionActual += 1;
                                decimal valorExtra = FuncionesUtiles.convertirADecimal(documentoLeido.GetCellValueAsString(numFila, posicionActual).Replace(".", ","));

                                if (objCatalogo == null)
                                {
                                    CatalogoDTOCrear objCatalogoCrear = new CatalogoDTOCrear();

                                    objCatalogoCrear.Nombrecatalogo = nombreCatalogo;
                                    objCatalogoCrear.TieneVigencia = false;
                                    objCatalogoCrear.UsuarioCreacion = usuarioCreacion;

                                    HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosCreate, HttpMethod.Post, objCatalogoCrear);

                                    objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);
                                }

                                if (i > 2)
                                {
                                    objDocumento.listaAreasDepartamentos +=";"+ objCatalogo.IdCatalogo + "," + valorExtra;
                                }
                                else
                                {
                                    objDocumento.listaAreasDepartamentos += objCatalogo.IdCatalogo + "," + valorExtra;
                                }
                                     
                                //AreasDepartamentoDTO objArea = new AreasDepartamentoDTO(objCatalogo.IdCatalogo, valorExtra);
                                //AreasDepartamentos.Add(objArea);

                            }
                            //objDocumento.AreasDepartamentos = AreasDepartamentos;
                        }
                    }
                    catch { }

                    if (string.IsNullOrEmpty(objDocumento.Nombre_Conjunto))
                    {
                        contadorVacios++;
                    }
                    else
                    {
                        listaDatosDocumento.Add(objDocumento);
                    }

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
