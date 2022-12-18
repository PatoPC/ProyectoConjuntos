
const idEmpresaOCulto = "idEmpresaOCulto";
const idModuloSeleccionOculto = "idModuloSeleccionOculto";
let listaRoles = new Object();
listaRoles.listaModulos = [];

$(document).ready(function () {
    crearObjetoJSAcordion()
});


function cerrarModelRol(modalCerrar, divBotonesConfirmarEliminar) {
    let modalModulo
    if (modalCerrar != undefined) {
        modalModulo = document.getElementById(modalCerrar);
    }
    else {
        modalModulo = document.getElementById("modalRolCategoria");
    }

    if (divBotonesConfirmarEliminar != null) {
        document.getElementById(divBotonesConfirmarEliminar).remove();
    }

    modalModulo.classList.remove("show");
    modalModulo.removeAttribute('aria-labelledby', 'modalRolCategoria');
    modalModulo.removeAttribute('style', 'display: block; padding-left: 0px;');
    modalModulo.removeAttribute('aria-modal', 'true;');
    modalModulo.removeAttribute('role', 'dialog;');
    modalModulo.removeAttribute('aria-hidden', 'true');
}

function mostrarModalRol() {
    let modalModulo = document.getElementById("modalRolCategoria");

    let elementoSelectModulo = document.getElementById("IdModuloSelect");
    let Nombre = elementoSelectModulo.options[elementoSelectModulo.selectedIndex].text;
    //Se valida que no se intente añadir un Módulo que ya se encuentre el en objeto y en el acordión
    let btnAniadirModal = document.getElementById("btnAniadirModal");
    btnAniadirModal.classList.remove("d-none")

    let btnEditarModal = document.getElementById("btnEditarModal");
    btnEditarModal.classList.add("d-none")

    for (moduloActual of listaRoles.listaModulos) {
        try {
            if (moduloActual.Nombre == Nombre) {
                Swal.fire({
                    title: 'Advertencia',
                    text: "Este módulo ya fue añadido.",
                    icon: 'info',
                })
                return false;
            }

        } catch {

        }
    }

    if (Nombre == "Seleccionar") {
        Swal.fire(
            'Error!',
            'Por favor seleccione un módulo.',
            'warning')
    }
    else {

        recuperarCatalogosMenus();

        modalModulo.classList.add("show");
        modalModulo.setAttribute('aria-labelledby', 'modalRolCategoria');
        modalModulo.setAttribute('style', 'display: block; padding-left: 0px;');
        modalModulo.setAttribute('aria-modal', 'true;');
        modalModulo.setAttribute('role', 'dialog;');
        modalModulo.removeAttribute('aria-hidden', 'true');
        document.getElementById("idTituloModal").innerHTML = "Añadir Módulo \"" + Nombre + "\"";
    }
}

async function recuperarCatalogosMenus() {
   
    let elementoSelectModulo = document.getElementById("IdModuloSelect");
    let idModuloSeleccion = elementoSelectModulo.options[elementoSelectModulo.selectedIndex].value;

    recuperarEstadoCatalogAPI( idModuloSeleccion, false).then(listaCatalogos => {
        crearContenidoModal(listaCatalogos, idModuloSeleccion)
    }
    );

}

function recuperarEstadoCatalogAPI(Nombre, editar) {
    let rutaRecuperarCatalogos = ""
    if (editar) {
        rutaRecuperarCatalogos = pathConsola + "/C_Catalogo/RecuperarCatalogoHijosPorNombre?nombreCatalogo=" + Nombre;
    }
    else {
        rutaRecuperarCatalogos = pathConsola + "/C_Catalogo/RecuperarHijosPorIDCatologPadreIDEmpresaRol?idCatalogoPadre=" + Nombre;
    }
    return new Promise((resolve, reject) => {
        var xhttp = new XMLHttpRequest();
        xhttp.open("GET", rutaRecuperarCatalogos, true);
        xhttp.send();

        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var cuerpoModal = document.getElementById("cuerpoModal");
                cuerpoModal.innerHTML = "";
                var directo = xhttp.response

                var listaCatalogos = JSON.parse(directo)

                if (listaCatalogos.length <= 0) {

                    Swal.fire({
                        title: 'Advertencia',
                        text: "No existen registros el Módulo seleccionado.",
                        icon: 'info',
                    })
                }

                resolve(listaCatalogos)
            }
            else if (this.readyState != 4 && this.status != 200) {
                reject(Swal.fire({
                    title: 'Error Inesperado',
                    text: "Ocurrió un error al recuperar las ciudades, por favor intente más tarde.",
                    icon: 'error',
                }))
            }
        }

    })
}

///Creación Cuerpo Modal JS
function crearContenidoModal(listaCatalogos, Nombre, posicionListaObjeto, idDivItemAcordion) {
    let objetoRolEditar = listaRoles.listaModulos[posicionListaObjeto] == undefined ? false : listaRoles.listaModulos[posicionListaObjeto];
    let idNombreModuloElemento = Nombre

    //Se oculta el boton de añadir d-none
    if (posicionListaObjeto != undefined) {
        let btnAniadirModal = document.getElementById("btnAniadirModal");
        btnAniadirModal.classList.add("d-none")

        let btnEditarModal = document.getElementById("btnEditarModal");
        btnEditarModal.classList.remove("d-none")
        btnEditarModal.setAttribute("onclick", "editarModulosPermisoModal(" + idDivItemAcordion + "," + posicionListaObjeto + ")");
    }

    for (var catalogo of listaCatalogos) {
        let menuRolEditar
        if (objetoRolEditar) {
            for (var menuEditar of objetoRolEditar.Menus) {
                if (menuEditar.NombreMenu == catalogo.nombrecatalogo) {
                    menuRolEditar = menuEditar
                    break
                }
            }
        }

        var inputHiddenSeleccion = document.createElement('INPUT');
        inputHiddenSeleccion.setAttribute("type", "hidden");
        inputHiddenSeleccion.setAttribute("value", idNombreModuloElemento);
        inputHiddenSeleccion.setAttribute("id", idModuloSeleccionOculto);
        //Fin campos Ocultos

        //Inicio creación contenido Modal
        var divColumnaIzquierda = document.createElement('div');
        divColumnaIzquierda.className = "flex-column col-xl-4";
        divColumnaIzquierda.setAttribute("id", idNombreModuloElemento);

        var divColumnaDerecha = document.createElement('div');
        divColumnaDerecha.className = "flex-column col-xl-8";
        divColumnaDerecha.setAttribute("id", "checkNombrePermisos");

        var divSuperior = document.createElement('div');

        divSuperior.className = "form-check form-check-custom form-check-solid mb-5";
        divSuperior.setAttribute("id", "divSuperior");

        var toolTip = document.createElement('i');
        toolTip.className = "fas fa-exclamation-circle ms-2 fs-7";
        toolTip.setAttribute("data-bs-toggle", "tooltip");
        toolTip.setAttribute("title", catalogo.datoadicional);

        var checkBoxMenu = document.createElement('INPUT');
        checkBoxMenu.className = "form-check-input me-3";
        checkBoxMenu.setAttribute("type", "checkbox");
        checkBoxMenu.setAttribute("value", catalogo.nombrecatalogo);
        checkBoxMenu.setAttribute("id", catalogo.idCatalogo);
        checkBoxMenu.setAttribute("name", catalogo.idCatalogo);
        checkBoxMenu.setAttribute("title", catalogo.nombrecatalogo);
        checkBoxMenu.setAttribute("title", catalogo.nombrecatalogo);
        checkBoxMenu.setAttribute("onchange", "desSeleccinarTodos('" + catalogo.idCatalogo + "')");
        ///aqui
        if (menuRolEditar != undefined) {
            if (menuRolEditar.NombreMenu == catalogo.nombrecatalogo)
                checkBoxMenu.setAttribute("checked", "checked");
        }

        var rutaMenuOculto = document.createElement('INPUT');
        rutaMenuOculto.setAttribute("type", "hidden");
        rutaMenuOculto.setAttribute("value", catalogo.datoadicional);
        rutaMenuOculto.setAttribute("id", "rutaMenuOculto");

        var label = document.createElement('LABEL');
        label.className = "form-check-label";

        var divInterno = document.createElement('div');

        divInterno.className = "fw-bolder text-gray-800";
        divInterno.innerHTML = catalogo.nombrecatalogo;

        label.appendChild(divInterno);
        divInterno.appendChild(toolTip);

        divColumnaIzquierda.appendChild(checkBoxMenu)
        divColumnaIzquierda.appendChild(rutaMenuOculto)
        divColumnaIzquierda.appendChild(label)
        divColumnaIzquierda.appendChild(inputHiddenSeleccion);
        divSuperior.appendChild(divColumnaIzquierda);

        //Permisos
        var listaPerimos = document.getElementById("listaPermisos").value;
        var listaPerimosJSON = JSON.parse(listaPerimos)

        for (var catalogoPermiso of listaPerimosJSON) {
            var divPermisos = document.createElement('div');
            divPermisos.className = "btn-group btn-group-sm";
            divPermisos.setAttribute("role", "group");
            divPermisos.setAttribute("aria-label", "Basic checkbox toggle button group");

            var checkBoxPermisos = document.createElement('INPUT');
            checkBoxPermisos.className = "btn-check";
            checkBoxPermisos.setAttribute("type", "checkbox");
            checkBoxPermisos.setAttribute("value", catalogoPermiso.nombreCatalogo);
            checkBoxPermisos.setAttribute("id", catalogo.idCatalogo + "-" + catalogoPermiso.idCatalogo);
            checkBoxPermisos.setAttribute("autocomplete", "off");
            checkBoxPermisos.setAttribute("onchange", "completarSeleccion('" + catalogo.idCatalogo + "','" + catalogo.idCatalogo + "-" + catalogoPermiso.idCatalogo + "')");
            if (menuRolEditar != undefined) {
                for (var rol in menuEditar.Permisos)
                    if (menuEditar.Permisos[rol].NombrePermiso == catalogoPermiso.nombreCatalogo)
                        checkBoxPermisos.setAttribute("checked", "checked");
            }

            var labelPermisos = document.createElement('LABEL');
            labelPermisos.className = catalogoPermiso.datoadicional;
            labelPermisos.innerHTML = catalogoPermiso.nombreCatalogo;
            labelPermisos.setAttribute("for", catalogo.idCatalogo + "-" + catalogoPermiso.idCatalogo);

            divPermisos.appendChild(checkBoxPermisos);
            divPermisos.appendChild(labelPermisos);

            divColumnaDerecha.appendChild(divPermisos)
        }
        divSuperior.appendChild(divColumnaDerecha);

        cuerpoModal.appendChild(divSuperior);
    }
}

//Recupera la información necesaria de los modulos seleccionados DESDE EL ACORDION, para construir el acordion inferior
function crearObjetoJSAcordion() {
    let btnModuloAcordion = document.getElementsByClassName("accordion-button fs-4 fw-bold")
    let IdRol = document.getElementById("IdRol").value

    listaRoles.listaModulos = [];

    for (var modulo of btnModuloAcordion) {
        let listaModulos = new Object();

        listaModulos.Menus = [];
        listaModulos.Nombre = modulo.innerHTML;
        listaModulos.IdRol = IdRol
        let divMenuRutaPermisos = document.getElementsByName(listaModulos.Nombre + "datosMenu");

        for (var divMenu of divMenuRutaPermisos) {
            var Menus = new Object();
            let divMenuRutaPermisosHijos = divMenu.childNodes
            Menus.NombreMenu = divMenuRutaPermisosHijos[1].innerHTML

            Menus.RutaMenu = divMenuRutaPermisosHijos[3].innerHTML
            listaMenuRutaPermisos = divMenuRutaPermisosHijos[5].childNodes
            Menus.Permisos = [];

            for (var datosMenu of listaMenuRutaPermisos) {
                if (datosMenu.nodeName.toLowerCase() == "span") {
                    var Permisos = new Object();
                    Permisos.NombrePermiso = datosMenu.innerHTML;
                    Permisos.Concedido = true;
                    Permisos.CssPermiso = datosMenu.className;
                    Menus.Permisos.push(Permisos);
                }
            }
            listaModulos.Menus.push(Menus);
        }
        listaRoles.listaModulos.push(listaModulos);
    }
}


//Recupera la información necesaria de los modulos seleccionados, para construir el acordion inferior
function crearObjetoJSCreacion(Nombre) {
    let listaModulos = new Object();
    listaModulos.Menus = [];

    let IdRolCampo = document.getElementById("IdRol")

    if (IdRolCampo != undefined) {
        let IdRol = IdRolCampo.value
        listaModulos.IdRol = IdRol
    }
    
    if (Nombre == undefined) {
        let elementoSelectModulo = document.getElementById("IdModuloSelect");
        Nombre = elementoSelectModulo.options[elementoSelectModulo.selectedIndex].text;
    }
    
    listaModulos.Nombre = Nombre;

    var elementosCuerpoModal = document.getElementById("cuerpoModal").childNodes;
    for (var elemento of elementosCuerpoModal) {
        var Menus = new Object();
        var divSuperior = elemento.childNodes;
        for (var divContenedor of divSuperior) {
            Menus.Permisos = []
            if (divContenedor.className == "flex-column col-xl-4") {
                var elementosInformacion = divContenedor.childNodes;
                for (var informacion of elementosInformacion) {
                    if (informacion.className == "form-check-input me-3") {
                        if (informacion.checked) {
                            Menus.NombreMenu = informacion.defaultValue;
                        }
                    }
                    else if (informacion.id == "rutaMenuOculto") {
                        Menus.RutaMenu = informacion.value;
                    }
                }
            }
            else if (divContenedor.id == "checkNombrePermisos" && Menus.NombreMenu !== undefined) {
                var elementosPermisos = divContenedor.childNodes;
                for (var divPermiso of elementosPermisos) {
                    if (divPermiso.className == "btn-group btn-group-sm") {
                        var datosMenu = divPermiso.childNodes;
                        for (informacion of datosMenu) {
                            if (informacion.checked) {
                                var Permisos = new Object();
                                Permisos.NombrePermiso = informacion.defaultValue;
                                Permisos.Concedido = true;
                                Permisos.CssPermiso = informacion.labels[0].className;
                                Menus.Permisos.push(Permisos);
                            }
                        }

                    }
                }
            }
        }
        if (Menus.NombreMenu !== undefined)
            listaModulos.Menus.push(Menus);
    }

    if (listaModulos.Menus.length <= 0) {
        Swal.fire({
            title: 'Advertencia',
            text: "Debe seleccionar por lo menos un menu y un permiso.",
            icon: 'info',
        })
    }
    else {
        let continuar = true;
        for (var listaPermisos of listaModulos.Menus) {
            if (listaPermisos.Permisos.length <= 0) {
                continuar = false;
                break;
            }
        }

        if (continuar) {
            let posicionArreglo = listaRoles.listaModulos.length;
            listaRoles.listaModulos.push(listaModulos);
            construirAcordionPermisos(listaModulos, posicionArreglo)
        }
        else {
            Swal.fire({
                title: 'Advertencia',
                text: "Debe seleccionar por lo menos un permiso por menu.",
                icon: 'info',
            })
        }
    }
}

function construirAcordionPermisos(listaModulos, posicionArreglo) {
    var elementoAcordion = document.getElementById("acordionResumen");
    let idDivItemAcordion = "div" + listaModulos.Nombre;

    let divAcordionItem = document.createElement('DIV');
    divAcordionItem.setAttribute("id", idDivItemAcordion);

    divAcordionItem.className = "accordion-item";
    elementoAcordion.appendChild(divAcordionItem)

    //Configuracion idEmpresa y idModulo selección, la idea es tener el id del módulo al que corresponden estas selección       
    let idModuloSeleccionado = document.getElementById(idModuloSeleccionOculto)


    let tituloAcordion = document.createElement('H2');
    tituloAcordion.className = "accordion-header accordion-icon-toggle";
    tituloAcordion.setAttribute("id", "cabeceraRol" + listaModulos.Nombre);

    divAcordionItem.appendChild(tituloAcordion);


    let botonTituloAcordion = document.createElement('BUTTON');
    botonTituloAcordion.className = "accordion-button fs-4 fw-bold";
    botonTituloAcordion.setAttribute("type", "button");
    botonTituloAcordion.setAttribute("data-bs-toggle", "collapse");
    botonTituloAcordion.setAttribute("data-bs-target", "#cabeceraRol" + listaModulos.Nombre + "_Cuerpo");
    botonTituloAcordion.setAttribute("aria-expanded", "true");
    botonTituloAcordion.setAttribute("aria-controls", "cabeceraRol" + listaModulos.Nombre + "_Cuerpo");
    botonTituloAcordion.innerHTML = listaModulos.Nombre;

    tituloAcordion.appendChild(botonTituloAcordion)

    let divCuerpoRol = document.createElement('DIV');
    divCuerpoRol.className = "accordion-collapse collapse show";
    divCuerpoRol.setAttribute("id", "cabeceraRol" + listaModulos.Nombre + "_Cuerpo");

    divAcordionItem.appendChild(divCuerpoRol);

    let divCuerpoRolInterno = document.createElement('DIV');
    divCuerpoRolInterno.className = "accordion-body";

    divCuerpoRol.appendChild(divCuerpoRolInterno);

    for (permisoRol of listaModulos.Menus) {
        //Llenado de datos dentro del acordion
        let divCuerpoRolDatos = document.createElement('DIV');
        divCuerpoRolInterno.className = "alert bg-light-primary d-flex flex-column flex-sm-row p-5 mb-10";

        divCuerpoRolInterno.appendChild(divCuerpoRolDatos)

        let iconoCuerpoDatos = document.createElement('I');
        iconoCuerpoDatos.className = "bi bi-check2-square fs-2x";
        divCuerpoRolInterno.appendChild(iconoCuerpoDatos)

        //Contenido Alert
        let divContenidoAlert = document.createElement('DIV');
        divContenidoAlert.className = "d-flex flex-column pe-0 pe-sm-10";

        divCuerpoRolInterno.appendChild(divContenidoAlert)

        let tituloContenidoAlert = document.createElement('h4');
        tituloContenidoAlert.className = "fw-bold";
        tituloContenidoAlert.innerHTML = permisoRol.NombreMenu;
        divContenidoAlert.appendChild(tituloContenidoAlert)

        //Span para mostrar la ruta
        let textoRutaMenu = document.createElement('SPAN');
        textoRutaMenu.innerHTML = permisoRol.RutaMenu;
        divContenidoAlert.appendChild(textoRutaMenu)

        //Div para mostrar los permisos otorgados
        let divPermisos = document.createElement('DIV');
        divPermisos.className = "d-flex flex-wrap";
        divContenidoAlert.appendChild(divPermisos)

        for (permiso of permisoRol.Permisos) {
            let spanPermiso = document.createElement('SPAN');
            let recuperarCSS = permiso.CssPermiso.split('-');
            spanPermiso.className = "badge badge-" + recuperarCSS[2];
            spanPermiso.innerHTML = permiso.NombrePermiso
            divPermisos.appendChild(spanPermiso)
        }
    }
    //Botones editar e eliminar
    let divBotonesEditarEliminar = document.createElement('DIV');
    divBotonesEditarEliminar.className = "position-absolute position-sm-relative m-2 m-sm-0 top-0 end-0 btn btn-icon ms-sm-auto";
    divCuerpoRolInterno.appendChild(divBotonesEditarEliminar)

    let divEditar = document.createElement('DIV');
    divEditar.className = "badge badge-light-success fs-8 fw-bolder";
    divEditar.setAttribute("onclick", "editarPermisosModulo(" + posicionArreglo + ",'" + idDivItemAcordion + "','"  + idModuloSeleccionado.value + "','" + listaModulos.Nombre + "')")

    divBotonesEditarEliminar.appendChild(divEditar)

    let iconoEditar = document.createElement('I');
    iconoEditar.className = "far fa-edit";
    iconoEditar.setAttribute("style", "color:green");
    divEditar.appendChild(iconoEditar)

    let divEliminar = document.createElement('DIV');
    divEliminar.className = "badge badge-light-danger fs-8 fw-bolder";
    divEliminar.setAttribute("onclick", "MostarModalConfirmarElimnar(" + posicionArreglo + "," + idDivItemAcordion + ",'" + listaModulos.Nombre + "')")
    divBotonesEditarEliminar.appendChild(divEliminar)

    let iconoEliminar = document.createElement('I');
    iconoEliminar.className = "far fa-trash-alt";
    iconoEliminar.setAttribute("style", "color:red");
    divEliminar.appendChild(iconoEliminar)
    cerrarModelRol("modalRolCategoria")
}

function eliminarPermisosModulo(posicionArreglo, idDivItemAcordion, nombreModal, btnConfirmarEliminarModal) {
    delete listaRoles.listaModulos[posicionArreglo];
    let divElminar = document.getElementById(idDivItemAcordion)

    if (divElminar != undefined) {
        divElminar.remove();
    }
    else {
        idDivItemAcordion.remove();
    }

    if (nombreModal != undefined) {
        cerrarModelRol(nombreModal)
        //Se borra el boton porque se crea uno nuevo con diferentes parametros en el eveto onclick
        document.getElementById(btnConfirmarEliminarModal).remove()
    }

}

function editarPermisosModulo(posicionArreglo, idDivItemAcordion, Nombre) {
    recuperarEstadoCatalogAPI(Nombre, true).then(listaCatalogos => {
        crearContenidoModal(listaCatalogos, Nombre, posicionArreglo, idDivItemAcordion)
        let modalModulo = document.getElementById("modalRolCategoria");
        modalModulo.classList.add("show");
        modalModulo.setAttribute('aria-labelledby', 'modalRolCategoria');
        modalModulo.setAttribute('style', 'display: block; padding-left: 0px;');
        modalModulo.setAttribute('aria-modal', 'true;');
        modalModulo.setAttribute('role', 'dialog;');
        modalModulo.removeAttribute('aria-hidden', 'true');
        document.getElementById("idTituloModal").innerHTML = "Editar Módulo \"" + Nombre + "\"";
    }
    );
}

function editarModulosPermisoModal(idDivItemAcordion, posicionObjetoActual) {
    let Nombre = listaRoles.listaModulos[posicionObjetoActual].Nombre
    delete listaRoles.listaModulos[posicionObjetoActual];
    idDivItemAcordion.remove();

    crearObjetoJSCreacion(Nombre);
}

function MostarModalConfirmarElimnar(posicionArreglo, idDivItemAcordion, Nombre) {

    let modalModulo = document.getElementById("modalConfirmarEliminar");
    let divBotonesConfirmarEliminar = document.getElementById("divBotonesConfirmarEliminar");
    let spanTextoEliminar = document.getElementById("spanConfirmarEliminar");
    spanTextoEliminar.innerHTML = Nombre;

    modalModulo.classList.add("show");
    modalModulo.setAttribute('aria-labelledby', 'modalRolCategoria');
    modalModulo.setAttribute('style', 'display: block; padding-left: 0px;');
    modalModulo.setAttribute('aria-modal', 'true;');
    modalModulo.setAttribute('role', 'dialog;');
    modalModulo.removeAttribute('aria-hidden', 'true');

    let btnConfirmarEliminar = document.createElement('div');
    btnConfirmarEliminar.className = "btn btn-danger";

    if (idDivItemAcordion.id == undefined) {
        btnConfirmarEliminar.setAttribute("onclick", "eliminarPermisosModulo(" + posicionArreglo + ",'" + idDivItemAcordion + "','modalConfirmarEliminar','btnConfirmarEliminarModal')")
    }
    else {
        btnConfirmarEliminar.setAttribute("onclick", "eliminarPermisosModulo(" + posicionArreglo + "," + idDivItemAcordion.id + ",'modalConfirmarEliminar','btnConfirmarEliminarModal')")
    }

    btnConfirmarEliminar.setAttribute("id", "btnConfirmarEliminarModal")
    btnConfirmarEliminar.innerHTML = "Eliminar"
    divBotonesConfirmarEliminar.append(btnConfirmarEliminar)
}

function sendFormAjaxEditarRol(btnSubmit, formAjax) {
    let continueSummit = true;
    let elementLoading = document.getElementById("imagenCargando");

    if (elementLoading != null) {
        elementLoading.classList.remove("invisible");
    }
    else {
        btnSubmit.setAttribute("data-kt-indicator", "on");
    }

    $(btnSubmit).attr('disabled', true);

    event.preventDefault();
    event.stopImmediatePropagation();

    let myForm = document.getElementById(formAjax);

    myForm.classList.add("formOpacityDisabled");

    $(':input', 'form').each(function (i, inputForm) {
        if ($(inputForm).attr("required")) {
            if (!$(inputForm).val()) {
                let campo;

                try {
                    campo = $("label[for='" + $(inputForm).attr('id') + "']")[0].innerText
                }
                catch {
                    try {
                        campo = $(inputForm).attr('placeholder');
                    }
                    catch {
                        campo = "Por favor revisa los campos obligatorios.";
                    }
                }

                Swal.fire(
                    campo,
                    "Por favor Ingresa la información solicitada.",
                    "error"
                )
                continueSummit = false;
                ocultarCargando(elementLoading, btnSubmit)

                myForm.classList.remove("formOpacityDisabled");
                $(btnSubmit).attr('disabled', false);

                return false;
            }
        }
    })
    console.log("*********************************")
    if (continueSummit) {        
        let Nombrerol = document.getElementById("NombreRol").value;
        let IdPaginaInicioRol = recuperarValueSelect("IdPaginaInicioRol")

        listaRoles["IdPaginaInicioRol"] = IdPaginaInicioRol
        
        listaRoles["NombreRol"] = Nombrerol

        let estado = false
        if (document.getElementById("Estado").checked)
            estado = true
        listaRoles["Estado"] = estado

        let RolRestringido = document.querySelector('input[name="RolRestringido"]:checked')
        let valorRolRestringido = false
        if (RolRestringido != undefined) {
            valorRolRestringido = Boolean(RolRestringido.value.toLowerCase() === 'true')
        }
        listaRoles["RolRestringido"] = valorRolRestringido

        let AccesoTodos = document.querySelector('input[name="AccesoTodos"]:checked')
        let valorAccesoTodos = false
        if (AccesoTodos != undefined) {
            valorAccesoTodos = Boolean(AccesoTodos.value.toLowerCase() === 'true')
        }

        listaRoles["AccesoTodos"] = valorAccesoTodos

        //let idRol = document.getElementById("IdRol").value        

        /**********************/
        let IdRolCampo = document.getElementById("IdRol")

        if (IdRolCampo != undefined) {
            let IdRol = IdRolCampo.value

            listaRoles["IdRol"] = IdRol
        }
        /**********************/


        if (listaRoles.listaModulos.length <= 0) {
            Swal.fire(
                "Por seleccione por lo menos un menu.",
                "Error",
                "error"
            )
            continueSummit = false
            ocultarCargando(elementLoading, btnSubmit)
            return false;
        }
    }

    var jsonTemp = JSON.stringify(listaRoles);

    console.log(jsonTemp);


    if (continueSummit) {


        $.ajax({
            url: myForm.action,
            type: myForm.method,
            data: JSON.stringify(listaRoles),
            dataType: 'JSON',
            contentType: "application/json",
            processData: false,
            success: function (result) {
                var responseRead = JSON.stringify(result);
                var jsonObject = JSON.parse(responseRead);
                // Result.
                Swal.fire(
                    jsonObject.result.message,
                    jsonObject.result.state,
                    jsonObject.result.icon,
                ).then((resulThen) => {
                    if (jsonObject.result.urlRetorno != undefined) {
                        window.location.href = jsonObject.result.urlRetorno
                    }
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Swal.fire(
                    "Ocurró un error inesperado",
                    "Error",
                    "error"
                )
            },
            complete: function (data) {
                ocultarCargando(elementLoading, btnSubmit)

                myForm.classList.remove("formOpacityDisabled");

            }
        });
    }
}

function ocultarCargando(elementLoading, btnSubmit) {
    if (elementLoading != null) {
        elementLoading.classList.add("invisible");
    }
    $(btnSubmit).attr('disabled', false);

}

//Si activo un permiso y tengo activo el check del Menu se activa el menu automaticamente, recibe id del menu a selecciónar y el id del check permisos
function completarSeleccion(idModuloSeleccion, estadoCheckPermiso) {
    var check = document.getElementById(estadoCheckPermiso);
    if (check.checked) {
        document.getElementById(idModuloSeleccion).checked = true;
    }
}

//Si se desmarca el menu y aun se tiene permisos selecionados automaticamente se desmarcan los permisos
function desSeleccinarTodos(idModuloSeleccion) {
    var check = document.getElementById(idModuloSeleccion);
    if (!check.checked) {

        var divMenu = check.parentElement;
        var divSuperior = divMenu.parentElement;
        var hijosDivSuperior = divSuperior.childNodes;
        for (var divContenedor of hijosDivSuperior) {
            if (divContenedor.id == "checkNombrePermisos") {
                var elementosPermisos = divContenedor.childNodes;
                for (var divPermiso of elementosPermisos) {
                    if (divPermiso.className == "btn-group btn-group-sm") {
                        var datosMenu = divPermiso.childNodes;
                        for (informacion of datosMenu) {
                            if (informacion.checked) {
                                informacion.checked = false;
                            }
                        }
                    }
                }
            }
        }
    }
}

function marcarTodos() {
    var divPrincipalModal = document.getElementById("cuerpoModal");

    $(':input', divPrincipalModal).each(function (i, inputForm) {

        if (inputForm.type == "checkbox") {
            inputForm.checked = true;
        }
    })
}
function desmarTodos() {
    var divPrincipalModal = document.getElementById("cuerpoModal");

    $(':input', divPrincipalModal).each(function (i, inputForm) {

        if (inputForm.type == "checkbox") {
            inputForm.checked = false;
        }
    })
}
