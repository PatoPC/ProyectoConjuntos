using DTOs.CatalogoGeneral;
using DTOs.ConfiguracionCuenta;
using DTOs.MaestroContable;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Servicio
{
    public class CargarMaestroContable
    {
        private readonly IServicioConsumoAPI<MaestroContableBusqueda> _servicioConsumoAPIBusqueda;

        public CargarMaestroContable(IServicioConsumoAPI<MaestroContableBusqueda> servicioConsumoAPIBusqueda)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
        }

        public async Task<List<MaestroContableDTOCompleto>> recuperarMaestroContable(MaestroContableBusqueda objBusqueda)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarMaestroContableAvanzado, HttpMethod.Get, objBusqueda);

            var listaMaestro = await LeerRespuestas<List<MaestroContableDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            ConfiguraCuentasDTOCompleto objConfigurar = await recuperarParametrizacionMaCont(objBusqueda.IdConjunto);

            if (!string.IsNullOrEmpty(objConfigurar.Parametrizacion))
            {
                foreach (var cuenta in listaMaestro)
                {
                    cuenta.CuentaCon = FuncionesUtiles.FormatearCadenaCuenta(cuenta.CuentaCon, objConfigurar.Parametrizacion);

                    if (cuenta.InverseIdConMstPadreNavigation != null)
                    {
                        FormatearCuentasRecursivo(cuenta.InverseIdConMstPadreNavigation, objConfigurar.Parametrizacion);
                    }
                }
            }


            return listaMaestro;
        }

        public async Task<ConfiguraCuentasDTOCompleto> recuperarParametrizacionMaCont(Guid? idConjunto)
        {
            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

            try
            {
                HttpResponseMessage respuestaConfigurar = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConfiguracion + idConjunto, HttpMethod.Get);

                if (respuestaConfigurar.IsSuccessStatusCode)
                    objConfigurar = await LeerRespuestas<ConfiguraCuentasDTOCompleto>.procesarRespuestasConsultas(respuestaConfigurar);
            }
            catch (Exception ex)
            {
                objConfigurar = new ConfiguraCuentasDTOCompleto();
            }

            return objConfigurar;
        }

        private void FormatearCuentasRecursivo(IEnumerable<MaestroContableDTOCompleto> listaCuentas, string parametrizacion)
        {
            foreach (var subCuentas in listaCuentas)
            {
                subCuentas.CuentaCon = FuncionesUtiles.FormatearCadenaCuenta(subCuentas.CuentaCon, parametrizacion);

                if (subCuentas.InverseIdConMstPadreNavigation.Count > 0)
                {
                    FormatearCuentasRecursivo(subCuentas.InverseIdConMstPadreNavigation, parametrizacion);
                }
            }
        }
    }
}
