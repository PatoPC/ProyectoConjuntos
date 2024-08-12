
async function mostrarDetallesContabilidad(idModal, idEquipoDespacho) {

    let result = await ConsumoAPI_HTTP("GET", pathConsola + "/C_Contabilidad/DetalleComprobantesCabecera?IdEncCont=" + idEquipoDespacho);

    var objDetallContabilidad = JSON.parse(result)
    let textoSecuencial

    let campofechaCabecera = document.getElementById("fechaCabecera")
    let campoNumeroComprobante = document.getElementById("numeroComprobanteCabecera")
    let campoconceptoCabecera = document.getElementById("conceptoCabecera")


    if (objDetallContabilidad != undefined) {
        if (objDetallContabilidad.secuencialCabeceraConts != undefined) {
            var secuencial = objDetallContabilidad.secuencialCabeceraConts[0];

            if (secuencial != undefined) {
                textoSecuencial = objDetallContabilidad.mes + "-" + secuencial.secuencial
            }
            else {
                textoSecuencial ="-"
            }

            campoNumeroComprobante.value = textoSecuencial
        }

        campofechaCabecera.value = objDetallContabilidad.fechaEncContTexto
        campoconceptoCabecera.value = objDetallContabilidad.conceptoEncCont

        let detalleContabilidad = objDetallContabilidad.detalleContabilidads

        var contador = 1;

        let tbody = document.getElementById("tblDetalleContabilidad")
        if (tbody.hasChildNodes()) {
            // Eliminar todos los hijos de tbody
            while (tbody.firstChild) {
                tbody.removeChild(tbody.firstChild);
            }
        }

        let totalDebito = 0
        let totalCredito= 0
        detalleContabilidad.forEach(function (dato) {
            var fila = tbody.insertRow();
            var celdaNumeroCuenta = fila.insertCell(0);
            var celdaConcepto = fila.insertCell(1);
            var celdaDetalle = fila.insertCell(2);
            var celdaDebito = fila.insertCell(3);
            var celdaCredito = fila.insertCell(4);

            celdaNumeroCuenta.innerText = dato.cuentaContable;
            celdaConcepto.innerText = dato.nroDepartmentoCont;
            celdaDetalle.innerText = dato.detalleDetCont;
            celdaDebito.innerText = dato.debitoDetCont;
            celdaCredito.innerText = dato.creditoDetCont;

            if (dato.debitoDetCont != undefined) {
                totalDebito = totalDebito + dato.debitoDetCont
            }
            if (dato.creditoDetCont != undefined) {
                totalCredito = totalCredito + dato.creditoDetCont
            }

            contador++; // Incrementar el contador
        });

        var filaTotal = tbody.insertRow();

        var celdaBlanco1 = filaTotal.insertCell(0);
        var celdaBlanco2 = filaTotal.insertCell(1);
        var celdaTotal = filaTotal.insertCell(2);
        var celdaTotalDebito = filaTotal.insertCell(3);
        var celdaTotalCredito = filaTotal.insertCell(4);

        celdaTotal.innerText = "TOTAL";
        celdaTotalDebito.innerText = totalDebito.toFixed(2)
        celdaTotalCredito.innerText = totalCredito.toFixed(2)
    }

    MostrarModal(idModal)

}