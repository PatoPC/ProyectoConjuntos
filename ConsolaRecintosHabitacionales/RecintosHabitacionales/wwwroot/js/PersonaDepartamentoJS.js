

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