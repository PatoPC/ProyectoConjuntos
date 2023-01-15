let contadorContratarUsuario = 0

function anadirConjuntosSeleccion() {
    //let elementoSelectConjunto = document.getElementById("IdConjunto");
    //let nombreConjunto = elementoSelectConjunto.options[elementoSelectConjunto.selectedIndex].text;

    //let idConjunto = elementoSelectConjunto.options[elementoSelectConjunto.selectedIndex].value;

    let idConjunto = recuperarValueSelect("IdConjunto");
    let nombreConjunto = recuperarTextSelect("IdConjunto");

    if (idConjunto != undefined) {
        let divConjuntosSeleccionadas = document.getElementById("divListaConjuntosAcceso")

        let idElementoConjunto = nombreConjunto + "" + idConjunto

        if (idElementoConjunto != "Seleccionar") {
            let nuevoDiv = document.getElementById(idElementoConjunto)

            if (nuevoDiv == undefined) {
                let nuevaConjuntoSeleccionada = document.createElement("DIV")
                nuevaConjuntoSeleccionada.className = "border border-gray-300 border-dashed rounded py-3 px-4 mb-3"
                nuevaConjuntoSeleccionada.innerHTML = nombreConjunto;
                nuevaConjuntoSeleccionada.setAttribute("id", idElementoConjunto);

                divConjuntosSeleccionadas.appendChild(nuevaConjuntoSeleccionada)

                let idOcultoConjuntoSeleccionada = document.createElement("INPUT")
                idOcultoConjuntoSeleccionada.value = idConjunto
                idOcultoConjuntoSeleccionada.setAttribute("type", "hidden")
                idOcultoConjuntoSeleccionada.setAttribute("name", "UsuarioConjuntos[" + contadorContratarUsuario + "].IdConjunto")

                divConjuntosSeleccionadas.appendChild(idOcultoConjuntoSeleccionada)

                let campoIDUsuario = document.getElementById('IdUsuario')

                if (campoIDUsuario != undefined) {
                    let idOcultoUsuario = document.createElement("INPUT")
                    idOcultoUsuario.value = campoIDUsuario.value
                    idOcultoUsuario.setAttribute("type", "hidden")
                    idOcultoUsuario.setAttribute("name", "UsuarioConjuntos[" + contadorContratarUsuario + "].IdUsuario")

                    divConjuntosSeleccionadas.appendChild(idOcultoUsuario)
                }

                let spanIcono = document.createElement("SPAN")
                spanIcono.className = "btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow"
                spanIcono.setAttribute("title", "Eliminar")
                spanIcono.setAttribute("onclick", "eliminarElementoConjuntoSeleccionada('" + idElementoConjunto + "','" + idConjunto + "')")

                nuevaConjuntoSeleccionada.appendChild(spanIcono)

                let iconoElimnar = document.createElement("I")
                iconoElimnar.className = "bi bi-x fs-2"

                spanIcono.appendChild(iconoElimnar)
                contadorContratarUsuario++

                let selectConjuntoFavorita = document.getElementById("IdConjuntoDefault")
                let idConjuntoFavorita = recuperarValueSelect("IdConjuntoDefault");

                if (idConjuntoFavorita == undefined) {
                    var options = document.querySelectorAll('#IdConjuntoDefault option');
                    options.forEach(o => o.remove());
                }

                let option = document.createElement('option');

                option.text = nombreConjunto;
                option.value = idConjunto;
                selectConjuntoFavorita.add(option);
            }
            else {
                Swal.fire({
                    title: 'Advertencia',
                    text: "Este conjunto ya fue añadida.",
                    icon: 'info',
                })
            }
        }

    }
    else {
        Swal.fire({
            title: 'Advertencia',
            text: "Por favor seleccione primero una conjunto favorita.",
            icon: 'info',
        })
    }
}

function eliminarElementoConjuntoSeleccionada(idElementoConjuntoSeleccion, idConjunto) {
    let elementoEliminar = document.getElementById(idElementoConjuntoSeleccion)
    if (elementoEliminar != undefined) {
        {
            elementoEliminar.remove()
            contadorContratarUsuario--
            var options = document.querySelectorAll('#IdConjuntoDefault option');
            options.forEach(o => o.value == idConjunto ? o.remove() : '');
        }
    }
}