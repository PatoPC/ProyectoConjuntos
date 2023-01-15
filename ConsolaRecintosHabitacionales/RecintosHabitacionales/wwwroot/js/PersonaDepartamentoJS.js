

function validarDepartamentoPersonaDuplicado(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario, campoDepartamento, campoTipoPersonaDepartamento) {

    let IdDepartamento = recuperarValueSelect(campoDepartamento)
    let IdTipoPersonaDepartamento = recuperarValueSelect(campoTipoPersonaDepartamento)

    let rutaConsumo = pathConsola + "/C_Persona/BusquedTipoPersonaDepartamento?IdDepartamento=" + IdDepartamento + "&IdTipoPersonaDepartamento=" + IdTipoPersonaDepartamento;

    return new Promise((resolve, reject) => {
        $.ajax({
            url: rutaConsumo,
            type: 'get',
            //data: formData,
            cache: false,
            contentType: false,
            processData: false,
            //dataType: 'JSON',
            //contentType: "application/json",
            success: function (result) {
                let responseRead = JSON.stringify(result);
                let jsonObject = JSON.parse(responseRead);

                if (jsonObject != undefined) {
                    if (jsonObject.nombrePersona != undefined) {
                        Swal.fire({
                            title: 'Ya existe una persona asignada a este departamento <br/> ¿Esta seguro de reemplazarla?<br/><b>Persona:</b>' + jsonObject.nombrePersona + '<br/><b>Departamento: </b>' + jsonObject.codigoDepartamento,
                            text: "¡No podrás revertir esto!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Si, actualizar información!',
                            cancelButtonText: 'No, cancelar!',
                            reverseButtons: true
                        }).then((result) => {
                            if (result.isConfirmed) {
                                sendFormAjax(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario)
                            }
                        })
                    }
                    else {
                       sendFormAjax(btnSubmit, formAjax, idDIVCargar, rutaCargarSubVista, id_BTN_Formulario)
                    }
                    
                }

            }
        });
    })
    
}

//tipoOpcion = debe venir "Crear" o "Editar"
function anadirTipoAreaDepartamento(divAreasDepartamento, idCantidadAreasDepartamentos, selectTipoAreaDepartamento, tipoOpcion, textoEditarArea, idEditarArea, valorEditarMetrosCuadrados) {
    let cantidadActual = document.getElementById(idCantidadAreasDepartamentos)
    let divNuevosCamposAreas = document.getElementById(divAreasDepartamento)
    let campoTipoAreas = document.getElementById(selectTipoAreaDepartamento)

    if (cantidadActual != undefined && divNuevosCamposAreas != undefined && campoTipoAreas != undefined) {
        let valorCantidadActual = parseInt(cantidadActual.value)
        let nombreTemporal = "TipoAreaDepartamento" + valorCantidadActual + tipoOpcion
        let idTemporalAreas = "listaTipoAreaDepartamento" + tipoOpcion
        let divTemporalBorrar = "temporal" + nombreTemporal + tipoOpcion
        let inputOcultoIDTipoArea = "IdTipoArea" + tipoOpcion
        var divInternoCerrar = document.createElement('div');
        divInternoCerrar.setAttribute("onclick", "eliminarDivTemporal('" + divTemporalBorrar + "')");
        divInternoCerrar.className ="col-md-1"

        var iconoInterno = document.createElement('i');
        iconoInterno.className = "fas fa-window-close"
        iconoInterno.setAttribute("style", "color:red; cursor:pointer");
        //<i class="fas fa-window-close" style="color:red; cursor:pointer"></i>
        divInternoCerrar.appendChild(iconoInterno)

        var divInterno1 = document.createElement('div');
        divInterno1.className = "col-md-6";
        divInterno1.setAttribute("id", divTemporalBorrar);

        divNuevosCamposAreas.appendChild(divInterno1)

        var divInterno2 = document.createElement('div');
        divInterno2.className = "row mb-12";

        divInterno1.appendChild(divInterno2)
        divInterno2.appendChild(divInternoCerrar)
        

        var divInterno3 = document.createElement('div');
        divInterno3.className = "col-xl-4";

        divInterno2.appendChild(divInterno3)

        var divInterno4 = document.createElement('div');
        divInterno4.className = "fs-6 fw-bold mt-2 mb-3";

        divInterno3.appendChild(divInterno4)

        //Se carga los valores de los campos para editar
        var idSeleccionTipoArea = recuperarValueSelect(selectTipoAreaDepartamento)
        let valorMetrosCuadradosEditar = undefined
        var textoSeleccion = recuperarTextSelect(selectTipoAreaDepartamento)

        if (valorEditarMetrosCuadrados != undefined) {
            idSeleccionTipoArea = idEditarArea
            valorMetrosCuadradosEditar = valorEditarMetrosCuadrados
            textoSeleccion = textoEditarArea
        }

        var labelInterno5 = document.createElement('label');        

        labelInterno5.className = "control-label"
        labelInterno5.innerHTML = textoSeleccion
        labelInterno5.setAttribute("for", nombreTemporal);

        divInterno4.appendChild(labelInterno5)

        var divInterno6 = document.createElement('div');
        divInterno6.className = "col-xl-8 fv-row";

        divInterno2.appendChild(divInterno6)

        //Valor metros cuadrados
        var inputValor = document.createElement('input');
        inputValor.setAttribute("id", "id" + nombreTemporal);
        inputValor.setAttribute("name", idTemporalAreas);
        inputValor.className = "form-control form-control-solid";
        inputValor.setAttribute("type", "number");
        inputValor.setAttribute("required", "required");

        if (valorMetrosCuadradosEditar != undefined)
            inputValor.setAttribute("value", valorMetrosCuadradosEditar);

        divInterno6.appendChild(inputValor)        

        var inputIDOcultoTipoArea = document.createElement('input')
        inputIDOcultoTipoArea.setAttribute("name", inputOcultoIDTipoArea )
        inputIDOcultoTipoArea.className = "form-control form-control-solid"
        inputIDOcultoTipoArea.setAttribute("type", "hidden")
        inputIDOcultoTipoArea.setAttribute("required", "required")
        inputIDOcultoTipoArea.setAttribute("value", idSeleccionTipoArea)

        divInterno6.appendChild(inputIDOcultoTipoArea)

        cantidadActual.value = valorCantidadActual +1
    }
    else {
        console.log("cantidadActual, no encontrada")
    }
}

function eliminarDivTemporal(divTemporalBorrar) {

    const campoDIV = document.getElementById(divTemporalBorrar);

    if (campoDIV != undefined) {
        campoDIV.remove(); 
    }

}
