function mostrarModalEditarAreaComunal(idModal, idAreaComunal, tipoAccion, accion) {

    let nombreFormularioEditar = "formularioEditarGestionarAreaComunal"
    let nombreBTNFormularioEditar = "btnEditarGestionarAreaComunal"
    let nombreTablaTorres = "idTablaAreaComunal"
    let nombreDIVResultadosTorres = "resultadoListaAreaComunal"

    let spanTextoBotonAccion = document.getElementById("btnEditarGestionarAreaComunalSpan")
    let btnAccionFormulario = document.getElementById(nombreBTNFormularioEditar)
    let formulario = document.getElementById(nombreFormularioEditar)

    if (spanTextoBotonAccion != undefined) {
        if (tipoAccion == "Editar") {
            spanTextoBotonAccion.innerHTML = "Editar"
            btnAccionFormulario.classList.remove("btn-danger");
            btnAccionFormulario.classList.add("btn-primary")
            let accionBoton = "sendFormAjax(this, '" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres + "','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres + "','" + idModal + "')"

            btnAccionFormulario.setAttribute("onclick", accionBoton)
            formulario.action = pathConsola + "/C_AreaComunal/EditarAreaComunal";
        }
        else {
            spanTextoBotonAccion.innerHTML = "Eliminar"
            btnAccionFormulario.classList.add("btn-danger")
            btnAccionFormulario.classList.remove("btn-primary");

            formulario.action = pathConsola + "/C_AreaComunal/EliminarAreaComunal";

            let accionBotonEliminar = "sendFormAjaxEliminar(this,'" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres + "','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres + "','" + idModal + "')"

            btnAccionFormulario.setAttribute("onclick", accionBotonEliminar)
        }
    }

    $.ajax({
        url: pathConsola + "/C_AreaComunal/BusquedaPorAreaComunalID?IdAreaComunal=" + idAreaComunal,
        type: "get",
        //data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            let responseRead = JSON.stringify(result);
            let jsonObject = JSON.parse(responseRead);

            if (jsonObject != undefined) {
                document.getElementById("IdConjuntoAreaComunal").value = jsonObject.idConjunto

                document.getElementById("IdAreaComunalEditar").value = jsonObject.idAreaComunal
                document.getElementById("NombreAreaEditar").value = jsonObject.nombreArea
                document.getElementById("HoraInicioEditar").value = jsonObject.horaFin
                document.getElementById("HoraFinEditar").value = jsonObject.horaInicio
                asignarCampoActivadoDesactivado("EstadoTemporal", jsonObject.estado.toString(), "EstadoEditar")
                document.getElementById("ObservacionEditar").value = jsonObject.observacion
            }

        },
        error: function (xhr, textStatus, error) {
            alert(xhr.statusText);
        },
        failure: function (response) {
            alert("failure " + response.responseText);
        }
    });

    MostrarModal(idModal)
}

 function buscarAreaComunalPorConjuntoselectConjunto(selectConjunto, selectArearComunal) {
    let SelectConjunto = document.getElementById(selectConjunto)
    let SelectAreaComunal = document.getElementById(selectArearComunal)

    var options = document.querySelectorAll('#' + selectArearComunal + ' option');
    options.forEach(o => o.remove());
    option = document.createElement('option');
    option.text = "Seleccionar";
     SelectAreaComunal.add(option);

    return $.ajax({

        url: pathConsola + "/C_ReservaAreas/recuperarAreasComunalesPorIdConjunto?idConjunto= " + SelectConjunto.value,
        type: "get",
        /*                data: formData,*/
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            let responseRead = JSON.stringify(result);
            let Lista = JSON.parse(responseRead);
            if (Lista.length > 0) {
                let contadorOpciones = 0
                for (var item of Lista) {
                    option = document.createElement('option');

                    option.text = item.nombreArea;
                    option.value = item.idAreaComunal;

                    SelectAreaComunal.add(option);

                }
            } else {
                Swal.fire({
                    title: 'Advertencia',
                    text: "No existen áreas comunales para el conjunto seleccionado.",
                    icon: 'info',
                })
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            Swal.fire({
                title: 'Error Inesperado',
                text: "Ocurrió un error al recuperar los Proveedores, por favor intente más tarde.",
                icon: 'error',
            })
        },
        complete: function (data) {

        }
    });
}
