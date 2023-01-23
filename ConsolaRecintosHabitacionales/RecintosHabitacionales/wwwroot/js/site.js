function sendFormAjaxEliminar(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario, dataTable, idModal) {
    Swal.fire({
        title: '¿Esta seguro de eliminar?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, borrar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            sendFormAjax(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario, dataTable, idModal)
        }
    })
}

async function sendFormAjax(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario, dataTable, idModal) {
    let continueSummit = true;
    let elementLoading = null
    let myForm = document.getElementById(formAjax);

    if (idModal == undefined && idModal == null)
        elementLoading = document.getElementById("imagenCargando");

    if (myForm != undefined) {
        if (elementLoading != null) {
            elementLoading.classList.remove("invisible");
            elementLoading.style.display = "block";
        }

        if (id_BTN_Formulario != undefined) {
            let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
            if (idBotonFormularioFormulario != undefined) {
                idBotonFormularioFormulario.setAttribute("data-kt-indicator", "on");
            }
        }


        //console.log("Formulario guardar:", formAjax);
        //console.log("elementLoading:", elementLoading);

        $(btnSubmit).attr('disabled', true);

        try {
            event.preventDefault();
            event.stopImmediatePropagation();
        }
        catch { }

        let formData = new FormData(myForm);

        continueSummit = validarCamposObligatoriosFormulario(formAjax)

        console.log("*********************************")
        if (continueSummit) {

            if (idDIVCargar != undefined) {
                let idDIVCarga = document.getElementById(idDIVCargar);
                if (idDIVCarga != undefined) {
                    idDIVCarga.innerHTML = ""
                }
            }
            return new Promise((resolve, reject) => {
            $.ajax({
                url: myForm.action,
                type: myForm.method,
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                //dataType: 'JSON',
                //contentType: "application/json",
                success: function (result) {

                    let responseRead = JSON.stringify(result);
                    let jsonObject = JSON.parse(responseRead);

                    if (jsonObject != undefined) {
                        if (jsonObject.result != undefined) {
                            console.log("Error mensaje linea 79 - Site.js")
                            Swal.fire(
                                jsonObject.result.message,
                                jsonObject.result.state,
                                jsonObject.result.icon,
                            )
                                .then((resulThen) => {
                                    if (jsonObject.result.urlRetorno != undefined) {
                                        window.location.href = jsonObject.result.urlRetorno
                                    }
                                    
                                    
                                    if (idDIVCargar != undefined && rutaCargarSubVista != undefined) {
                                        let rutaCargarSubVistaFinal = pathConsola + rutaCargarSubVista
                                        console.log("site.js sendAjax -> rutaCargarSubVistaFinal: " + rutaCargarSubVistaFinal)

                                        $('#' + idDIVCargar).load(rutaCargarSubVistaFinal, function () {
                                            if (dataTable != undefined)
                                                ordenarTabla(dataTable)

                                            if (elementLoading != null) {
                                                //elementLoading.classList.add("invisible");
                                                elementLoading.style.display = "none";
                                            }
                                            if (id_BTN_Formulario != undefined) {
                                                let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
                                                if (idBotonFormularioFormulario != undefined) {
                                                    idBotonFormularioFormulario.removeAttribute("data-kt-indicator");
                                                }
                                            }
                                        });
                                    }
                                    
                                    limpiarCamposFormulario(formAjax)
                                })
                        }

                        else {
                            console.log("Else linea 143 - Site.js")
                            if (id_BTN_Formulario != undefined) {
                                let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
                                if (idBotonFormularioFormulario != undefined) {
                                    idBotonFormularioFormulario.removeAttribute("data-kt-indicator");
                                }
                            }
                            if (jsonObject.state != undefined) {
                                if (jsonObject.state == "Exitoso!") {
                                    limpiarCamposFormulario(formAjax)
                                }
                            }
                            Swal.fire(
                                jsonObject.message,
                                jsonObject.state,
                                jsonObject.icon,
                            ).then((resulThen) => {
                                if (jsonObject.url != undefined) {
                                    window.location.href = jsonObject.url;
                                }
                                if (jsonObject.urlRetorno != undefined) {
                                    window.location.href = jsonObject.urlRetorno;
                                }

                                if (idDIVCargar != undefined && rutaCargarSubVista != undefined)
                                    $('#' + idDIVCargar).load(pathConsola + rutaCargarSubVista, function () {
                                        if (dataTable != undefined)
                                            ordenarTabla(dataTable)
                                    });
                            })
                        }
                    }
                    else {
                        console.log("Else linea 24 - Site.js")
                        if (id_BTN_Formulario != undefined) {
                            let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
                            if (idBotonFormularioFormulario != undefined) {
                                idBotonFormularioFormulario.removeAttribute("data-kt-indicator");
                            }
                        }

                        Swal.fire(
                            "Ocurró un error inesperado",
                            "Error",
                            "error"
                        )
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (id_BTN_Formulario != undefined) {
                        let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
                        if (idBotonFormularioFormulario != undefined) {
                            idBotonFormularioFormulario.removeAttribute("data-kt-indicator");
                        }
                    }

                    Swal.fire(
                        "Ocurró un error inesperado",
                        "Error",
                        "error"
                    )
                },
                complete: function (data) {
                    if (elementLoading != null) {
                        //elementLoading.classList.add("invisible");
                        elementLoading.style.display = "none";
                    }

                    myForm.classList.remove("formOpacityDisabled");
                    $(btnSubmit).attr('disabled', false);

                    if (idModal != undefined) {
                        cerrarModal(idModal)
                    }
                }
            });
            })
            }


        myForm.classList.remove("formOpacityDisabled");
    }
    else {
        Swal.fire(
            "!No se encontró el formulario¡ Contáctese con soporte técnico",
            "error",
            "error",
        )
    }

    if (elementLoading != null) {
        //elementLoading.classList.add("invisible");
        elementLoading.style.display = "none";
    }

    if (id_BTN_Formulario != undefined) {
        let idBotonFormularioFormulario = document.getElementById(id_BTN_Formulario);
        if (idBotonFormularioFormulario != undefined) {
            idBotonFormularioFormulario.removeAttribute("data-kt-indicator");
        }
    }


    $(btnSubmit).attr('disabled', false);

    if (idModal != undefined) {
        cerrarModal(idModal)
    }
}



function validarCamposObligatoriosFormulario(nombreFormulario) {
    let inputs

    if (nombreFormulario.id != undefined) {
        inputs = nombreFormulario;
    }
    else {
        let formuarioTemporal = document.getElementById(nombreFormulario);
        if (formuarioTemporal != undefined) {
            inputs = formuarioTemporal.elements;
        }
        else {
            Swal.fire(
                "Error",
                "No se puede continuar porque el formulario no existe en la página actual.",
                "error"
            )
        }
    }

    // Iterate over the form controls
    if (inputs) {
        for (i = 0; i < inputs.length; i++) {

            if (inputs[i].nodeName === "INPUT" && inputs[i].type === "text" || inputs[i].type === "email" || inputs[i].type === "number" || inputs[i].nodeName === "TEXTAREA") {
                try {
                    if (inputs[i].getAttribute("required")) {
                        if (!inputs[i].value) {
                            let campo = obtenerTextoNombreCampoObligatorio(inputs[i])
                            if (campo == undefined)
                                campo = "Campo obligatorio sin label for, posición " + i
                            Swal.fire(
                                campo,
                                "Por favor Ingresa la información solicitada.",
                                "error"
                            )
                            return false;
                        }
                    }
                } catch (e) {
                    console.log("Error línea 228 en validarCamposObligatoriosFormulario campo #: " + i + " Error: " + e)
                    return false;
                }
            }
            else if (inputs[i].nodeName === "SELECT") {
                console.log("es select")
                try {
                    if (inputs[i].getAttribute("required")) {
                        let idSelect = recuperarValueSelect(inputs[i].getAttribute("id"))

                        if (idSelect == null || idSelect == undefined || idSelect == "" || idSelect == "Seleccionar" || idSelect == "-1") {
                            let campo = inputs[i].getAttribute('id')

                            console.log("Campo linea 163: " + campo)

                            let textoLabelSelect = obtenerTextoNombreCampoObligatorio(campo)
                            console.log("campo Select id: " + campo)

                            if (textoLabelSelect == "" || textoLabelSelect == undefined) {
                                textoLabelSelect = campo.replace("Id", "")
                            }

                            Swal.fire(
                                textoLabelSelect,
                                "Por favor Ingresa la información solicitada.",
                                "error"
                            )

                            return false;
                        }
                    }
                }
                catch (e) {
                    console.log("Error en validarCamposObligatoriosFormulario campo select #: " + i + " Error: " + e)
                    return false;
                }
            }
            else if (inputs[i].type === "file") {
                try {
                    if (inputs[i].required) {
                        if (inputs[i].value === "") {
                            Swal.fire(
                                "Cargue el archivo solicitado",
                                "Por favor Ingresa la información solicitada.",
                                "error"
                            )
                            return false;
                        }

                    }
                }
                catch (e) {
                    console.log("Error en validarCamposObligatoriosFormulario campo select #: " + i + " Error: " + e)
                    return false;
                }
            }
        }

        return true;
    }

    return false;
}

function obtenerTextoNombreCampoObligatorio(input) {
    let campo
    try {
        campo = $("label[for='" + input.getAttribute('id') + "']")[0].innerText
    }
    catch {
        try {
            campo = input.getAttribute('placeholder');
        }
        catch {
            try {
                campo = $("label[for='" + input + "']")[0].innerText
            }
            catch (e) {
                console.log("Error en obtenerTextoNombreCampoObligatorio" + " Error: " + e)
            }

        }
    }

    return campo
}

function limpiarCamposFormulario(nombreFormulario) {
    var formulario = document.getElementById(nombreFormulario);

    if (formulario != undefined) {
        var inputs = document.getElementById(nombreFormulario).elements;

        // Iterate over the form controls
        for (i = 0; i < inputs.length; i++) {
            if (inputs[i].nodeName === "INPUT" && inputs[i].type === "text" || inputs[i].nodeName === "TEXTAREA") {
                inputs[i].value = ""
            }
            else if (inputs[i].nodeName === "SELECT") {
                $("#" + inputs[i].getAttribute('id')).val('').change();
            }
            else if (inputs[i].type === "file") {
                let inputIdDIVInformacion = inputs[i]
                let idDIVInformacion = inputIdDIVInformacion.getAttribute("data-divinformacion");

                if (idDIVInformacion != undefined) {
                    let divDIVInformacion = document.getElementById(idDIVInformacion)
                    if (divDIVInformacion != undefined) {
                        divDIVInformacion.innerHTML = ""
                        divDIVInformacion.style.display = "none"
                    }
                }
            }
        }
    }
    else {
        console.log("Se intentó limpiar un formuario en limpiarCamposFormulario() -> site.js que no existe: " + nombreFormulario)
    }
    return true;
}

function ordenarTabla(idTabla) {
    try {
        console.log("ingresa: " + idTabla)
        $('#' + idTabla).dataTable({
            "order": [[2, "desc"]], //or asc //desc
        });

        console.log("Tabla " + idTabla + " ordenada")
    }
    catch (error) {
        console.log("Error ordenarTabla: " + idTabla + " -> Error: " + error)
    }
}


function MostrarModal(nombreModal) {
    let modalModulo = document.getElementById(nombreModal);

    if (modalModulo != undefined) {
        modalModulo.classList.add("show");
        modalModulo.setAttribute('aria-labelledby', 'modalRolCategoria');
        modalModulo.setAttribute('style', 'display: block; padding-left: 0px; z-index:1022');
        //modalModulo.setAttribute('style', 'display: block; padding-left: 0px; z-index:1');
        modalModulo.setAttribute('aria-modal', 'true;');
        modalModulo.setAttribute('role', 'dialog;');
        modalModulo.removeAttribute('aria-hidden', 'true');
    }
   
}

function cerrarModal(nombreModal) {

    let modalModulo = document.getElementById(nombreModal);

    if (modalModulo != undefined) {
        modalModulo.classList.remove("show");
        modalModulo.removeAttribute('aria-labelledby', 'modalRolCategoria');
        modalModulo.removeAttribute('style', 'display: block; padding-left: 0px; z-index:1022');
        modalModulo.removeAttribute('aria-modal', 'true;');
        modalModulo.removeAttribute('role', 'dialog;');
        modalModulo.removeAttribute('aria-hidden', 'true');
    }
    else {
        console.log("Se intentó cerrar un modal que ya no existe en el contexto actual en cerrarModal() -> site.js => " + nombreModal)
    }
}

function borrarResultadosAnteriores(divEmpty) {
    $("#" + divEmpty).html("");
}


function consultarDesdeAPIControlador(idSelectPadre, idSelectHijo, controlador, accion, parametroBusqueda) {
    //Este control se muestra cuando los tipos documentos tienen tres niveles (sub documentos como en las licencias, Drive Smarth )

    let elemento = document.getElementById(idSelectPadre);
    try {
        let idSeleccion = elemento.options[elemento.selectedIndex].value;        //Elemento en donde se colocará el resultado

        let selectHijo = document.getElementById(idSelectHijo);

        var options = document.querySelectorAll('#' + idSelectHijo + ' option');
        options.forEach(o => o.remove());
        option = document.createElement('option');
        option.text = "Seleccionar";
        selectHijo.add(option);
        //alert(value)

        let rutaConsumo
        var xhttp = new XMLHttpRequest();
        if (idSeleccion != "") {
            rutaConsumo = pathConsola + "/" + controlador + "/" + accion + "?" + parametroBusqueda + "=" + idSeleccion;
        }
        else {
            rutaConsumo = pathConsola + "/" + controlador + "/" + accion + "?" + parametroBusqueda;
        }
        xhttp.open("GET", rutaConsumo, true);
        xhttp.send();

        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var directo = xhttp.response

                var listaRegistros = JSON.parse(directo)
                if (listaRegistros.length > 0) {
                    for (var dato of listaRegistros) {
                        option = document.createElement('option');

                        if (dato.nombrecatalogo != undefined && dato.nombrecatalogo != null) {
                            option.text = dato.nombrecatalogo;
                            option.value = dato.idCatalogo;
                        }
                        else if (dato.id != undefined) {
                            option.text = dato.texto;
                            option.value = dato.id;
                        }
                        selectHijo.add(option);
                    }


                }
                else {
                   
                    console.log("No existen registros: JSBusquedaDocumentos.js -> linea 10 a 88.")
                    console.log("Controlador: " + controlador + ". Accion: " + accion + ". ParametroBusqueda: " + parametroBusqueda + " = " + idSeleccion)
                }
            }
            else if (this.readyState != 4 && this.status != 200) {
                console.log("Ocurrió un error al recuperar los datos, por favor intente más tarde.");
            }
        }
    }
    catch {
        console.log("Error al recuperar información. (consultarDesdeAPIControlador.js linea 503)")
    }
};


/////////////////////////////////////////////////////ARVHICOS//////////////////////////////////
function recuperNombreArchivoInputFile(inputFile, textoNombreArchivo, idLinkArchivo) {
    let inputFileElement = document.getElementById(inputFile)
    let divTextoNombreArchivo = document.getElementById(textoNombreArchivo)
    let tipoArchivo = inputFileElement.files[0].type;

    let divImagen

    console.log("tipoArchivo: " + tipoArchivo)

    switch (tipoArchivo) {

        case 'application/pdf':
            divImagen = divConIconoPDF('/media/svg/files/pdf.svg')
            break;
        case 'image/png':
            divImagen = divConIconoPDF('/media/svg/files/png.svg')
            break;
        case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
            divImagen = divConIconoPDF('/media/svg/files/icons8-ms-excel.svg')
            break;
        case 'image/jpeg':
        case 'image/jpg':
            divImagen = divConIconoPDF('/media/svg/files/jpg.svg')
            break;

        default:
    }

    if (divImagen != undefined) {

        let linkVerDocumento = document.getElementById(idLinkArchivo)

        if (linkVerDocumento != undefined)
            linkVerDocumento.remove()


        let spanTextoInput = "spanTextoInput"
        divTextoNombreArchivo.innerHTML = "<b>Nombre:</b> " + inputFileElement.files[0].name;

        divTextoNombreArchivo.style.backgroundColor = "#f5f8fa";
        let tamanoArchivo = parseFloat(inputFileElement.files[0].size) / parseFloat(1000);

        let tamanoArchivoMB = tamanoArchivo / parseFloat(1000);

        divTextoNombreArchivo.style.display = "block";

        var spanCerrar = document.createElement("SPAN");
        spanCerrar.setAttribute("class", "dropzone-delete");
        spanCerrar.setAttribute("onclick", "eliminarArchivoInput('" + inputFile + "','" + textoNombreArchivo + "','" + inputFile + "')");
        spanCerrar.setAttribute("id", "spanTextoInput");

        var iconoCerrar = document.createElement("i");
        iconoCerrar.setAttribute("class", "bi bi-x fs-1");

        spanCerrar.appendChild(iconoCerrar)

        var spanTamanio = document.createElement("div");
        spanTamanio.innerHTML = "<b>Tamaño:</b> " + tamanoArchivoMB + " <b>MB</b>"

        console.log(tamanoArchivo)

        divTextoNombreArchivo.appendChild(spanTamanio)
        divTextoNombreArchivo.appendChild(divImagen)

        divTextoNombreArchivo.appendChild(spanCerrar)
    }
    else {
        Swal.fire({
            title: 'Advertencia',
            text: "Formato de archivo no permitido.",
            icon: 'info',
        })
    }
}

//Icono pdf
// /assets/media/svg/files/pdf.svg
// /assets/media/svg/files/jpg.svg
// /assets/media/svg/files/png.svg

function divConIconoPDF(urlArchivo) {
    var divImagenIcono = document.createElement("DIV");
    divImagenIcono.setAttribute("class", "symbol symbol-60px mb-5");

    var imagenSVG = document.createElement("IMG");
    imagenSVG.setAttribute("src", urlArchivo);

    divImagenIcono.appendChild(imagenSVG);

    return divImagenIcono
}

