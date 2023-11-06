//Cambia algunos atributos incluyendo ID del modal para crear Cargos
function crearNuevaTorreConjunto(IdTorre_atributo, IdConjunto_Torre_atributo, IdTorreHtml,
    IdConjuntoHtml, nombreModal, Contador, btn_idCerrarModal_cabecera, btn_idCerrarModal_Foter, btn_FormularioModal) {
    let IdConjunto = document.getElementById(IdConjuntoHtml)
    let IdArea = document.getElementById(IdTorreHtml)
    let elementor_Btn_FormularioModal = document.getElementById(btn_FormularioModal)

    IdConjunto.value = IdConjunto_Torre_atributo
    IdArea.value = IdTorre_atributo
    let nuevoIdModal = 'ModalArea_' + Contador

    let campoDIVModal = document.getElementsByName(nombreModal)[0]
    campoDIVModal.setAttribute("id", nuevoIdModal)

    let Id_btn_cerrarModal_Cabecera = document.getElementById(btn_idCerrarModal_cabecera)
    Id_btn_cerrarModal_Cabecera.setAttribute("onclick", "cerrarModal('" + nuevoIdModal + "')")
    let Id_btn_cerrarModal_Foter = document.getElementById(btn_idCerrarModal_Foter)
    Id_btn_cerrarModal_Foter.setAttribute("onclick", "cerrarModal('" + nuevoIdModal + "')")

    elementor_Btn_FormularioModal.setAttribute("onclick", "sendFormAjax(this, 'formularioCrearNuevoCargo', 'resultadoAreasPordepartamento','/C_Departamento/obtenerAreasPorIdDepartamento?IdConjunto=" + IdDepartamentoCargo_atributo + "','btn_AgregarCargo','idTablaAreasDepartamento','" + nuevoIdModal + "')")

    MostrarModal(nuevoIdModal)
}

//////////////////////// Editar Areas ///////////
function mostrarModalEditarTorre(idModal, idTorreEditar, tipoAccion, accion) {

    let nombreFormularioEditar = "formularioGestionarTorreModal"
    let nombreBTNFormularioEditar = "btnGestionarTorreoModal"
    let nombreTablaTorres = "idTablaTorres"
    let nombreDIVResultadosTorres = "resultadoListaTorres"

    let spanTextoBotonAccion = document.getElementById("btnGestionarTorreoModalSpan")
    let btnAccionFormulario = document.getElementById(nombreBTNFormularioEditar)
    let formulario = document.getElementById(nombreFormularioEditar)

    if (spanTextoBotonAccion != undefined) {
        if (tipoAccion == "Editar") {
            spanTextoBotonAccion.innerHTML = "Editar"
            btnAccionFormulario.classList.remove("btn-danger");
            btnAccionFormulario.classList.add("btn-primary")
            let accionBoton = "sendFormAjax(this, '" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres +"','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres +"','" + idModal +"')"
            
            btnAccionFormulario.setAttribute("onclick", accionBoton)
            formulario.action = pathConsola + "/C_Torres/EditarTorres";
        }
        else {
            spanTextoBotonAccion.innerHTML = "Eliminar"
            btnAccionFormulario.classList.add("btn-danger")
            btnAccionFormulario.classList.remove("btn-primary");

            formulario.action = pathConsola + "/C_Torres/EliminarTorres";

            let accionBotonEliminar = "sendFormAjaxEliminar(this,'" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres +"','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres +"','" + idModal +"')"

            btnAccionFormulario.setAttribute("onclick", accionBotonEliminar)
        }
    }

    $.ajax({
        url: pathConsola + "/C_Torres/BusquedaPorTorresID?IdTorres=" + idTorreEditar,
        type: "get",
        //data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            let responseRead = JSON.stringify(result);
            let jsonObject = JSON.parse(responseRead);

            if (jsonObject != undefined) {

                console.log("jsonObject.idConjunto: " + jsonObject.idConjunto)
                console.log("jsonObject.idTorres: " + jsonObject.idTorres)                

                document.getElementById("IdConjuntoEditar").value = jsonObject.idConjunto
                let idTorreTemporal = document.getElementById("IdTorresEditar")
                idTorreTemporal.value = jsonObject.idTorres
                document.getElementById("NombreTorresEditar").value = jsonObject.nombreTorres             
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

function mostrarCrearDepartamentoTorre(idModal, idTorreEditar) {
    let idTorreDepartamento = document.getElementById("IdTorresCrearDepartamento")

    if (idTorreDepartamento != undefined) {
        idTorreDepartamento.value = idTorreEditar
    }
    else {
        console.log("No existe campo para colocar id Torre: (crear departatmento) " + idTorreEditar)
    }
    MostrarModal(idModal)
}

//////////////////////// Editar Areas ///////////
function mostrarModalDepartamentoEditar(idModal, idDepartamentoEditar, tipoAccion, accion) {

    let nombreFormularioEditar = "FormularioEditarDepartamentoModal"
    let nombreBTNFormularioEditar = "btnModalDepartamentoGestionar"
    let nombreTablaTorres = "idTablaTorres"
    let nombreDIVResultadosTorres = "resultadoListaTorres"

    let spanTextoBotonAccion = document.getElementById("btnModalDepartamentoGestionarSpan")
    let btnAccionFormulario = document.getElementById(nombreBTNFormularioEditar)
    let formulario = document.getElementById(nombreFormularioEditar)

    if (spanTextoBotonAccion != undefined) {
        if (tipoAccion == "Editar") {
            spanTextoBotonAccion.innerHTML = "Editar"
            btnAccionFormulario.classList.remove("btn-danger");
            btnAccionFormulario.classList.add("btn-primary")
            let accionBoton = "sendFormAjax(this, '" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres + "','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres + "','" + idModal + "')"

            btnAccionFormulario.setAttribute("onclick", accionBoton)
            formulario.action = pathConsola + "/C_Departamento/EditarDepartamento";
        }
        else {
            spanTextoBotonAccion.innerHTML = "Eliminar"
            btnAccionFormulario.classList.add("btn-danger")
            btnAccionFormulario.classList.remove("btn-primary");

            formulario.action = pathConsola + "/C_Departamento/EliminarDepartamento";

            let accionBotonEliminar = "sendFormAjaxEliminar(this,'" + nombreFormularioEditar + "','" + nombreDIVResultadosTorres + "','" + accion + "','" + nombreBTNFormularioEditar + "','" + nombreTablaTorres + "','" + idModal + "')"

            btnAccionFormulario.setAttribute("onclick", accionBotonEliminar)
        }
    }

    $.ajax({
        url: pathConsola + "/C_Departamento/BusquedaPorDepartamentoID?IdDepartamento=" + idDepartamentoEditar,
        type: "get",
        //data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            let responseRead = JSON.stringify(result);
            let jsonObject = JSON.parse(responseRead);

            if (jsonObject != undefined) {
                document.getElementById("CoigoDeptoEditar").value = jsonObject.codigoDepartamento
                document.getElementById("MetrosDeptoEditar").value = jsonObject.metrosDepartamento
                document.getElementById("AliqDeptoEditar").value = jsonObject.aliqDepartamento
                document.getElementById("SaldoInicialAnualEditar").value = jsonObject.saldoInicialAnual
                document.getElementById("IdDeptoEditar").value = jsonObject.idDepartamento
                document.getElementById("IdTorresEditarDepartamento").value = jsonObject.idTorres
                document.getElementById("DatosCuentaDepartamento").value = jsonObject.nombreCuentaContable

                console.log("jsonObject.tipoPersonas " + jsonObject.tipoPersonas)
                
                for (let i = 0; i < jsonObject.tipoPersonas.length; i++) {
                    let divRow = document.getElementById("columnaTipoPersona" + i)
                    divRow.style.display = '';  // toggle back to the previous value

                    let labelTipoPersona = document.getElementById("labelTipoPersonaDepartamento" + i)
                    if (labelTipoPersona != undefined)
                        labelTipoPersona.innerHTML = jsonObject.tipoPersonas[i].tipoPersona

                    let inputTipoPersona = document.getElementById("nombreTipoPersonaDepartamento" + i)
                    if (inputTipoPersona != undefined)
                        inputTipoPersona.value = jsonObject.tipoPersonas[i].nombrePersona
                }

                let divAreasDepartamentoEditar = document.getElementById('divAreasDepartamentoEditar')

                if (divAreasDepartamentoEditar != undefined)
                    divAreasDepartamentoEditar.innerHTML=""

                for (let j = 0; j < jsonObject.areasDepartamentos.length; j++) {                   

                    anadirTipoAreaDepartamento('divAreasDepartamentoEditar', 'idCantidadAreasDepartamentosEditar', 'IdTipoAreaDepartamentoEditar', 'Editar', jsonObject.areasDepartamentos[j].nombreTipoArea, jsonObject.areasDepartamentos[j].idTipoArea, jsonObject.areasDepartamentos[j].metrosCuadrados)
                }
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
