namespace ecovave.common
{
    public static class Constante
    {
        public const string Desarrollo = "Development";
        public const string Produccion = "Production";

        public const string EX_GENERICA = "Internal Server Error - Se presento una condicion inesperada que impidio completar el Request.";
        public const string EX_PARAMETROS_INCORRECTOS = "No ingresó todos los parámetros obligatorios.";

        public const string EX_CENTRO_TRABAJO_NOT_FOUND = "No se encuentra registro del centro de trabajo por código modular.";

        // Licencia    
        public const string EX_LICENCIA_CREATE = "No se registró la licencia.";
        public const string EX_LICENCIA_UPDATE = "No se actualizó la licencia.";
        public const string EX_LICENCIA_DELETE = "No se eliminó la licencia.";
        public const string EX_LICENCIA_ENVIAR = "No se envió la licencia.";
        public const string EX_LICENCIA_GENERAR_PROYECTO = "No se generó proyecto.";
        public const string EX_LICENCIA_GENERAR_ACCION_GRABADA = "No se generó acción grabada.";
        public const string EX_LICENCIA_EXPORTAR = "No se exportó la licencia.";

        public const string EX_REGIMEN_LABORAL_CREATE = "No se registró régimen laboral.";
        public const string EX_REGIMEN_LABORAL_UPDATE = "No se actualizó régimen laboral.";
        public const string EX_REGIMEN_LABORAL_DELETE = "No se eliminó régimen laboral.";
        public const string EX_UGEL_CREATE = "No se registró ugel.";
        public const string EX_UGEL_UPDATE = "No se actualizó ugel.";
        public const string EX_UGEL_DELETE = "No se eliminó ugel.";
        public const string EX_DRE_CREATE = "No se registró dre.";
        public const string EX_DRE_UPDATE = "No se actualizó dre.";
        public const string EX_DRE_DELETE = "No se eliminó dre.";

        public const string EX_UNIDAD_EJECUTORA_CREATE = "No se registró unidad ejecutora.";
        public const string EX_UNIDAD_EJECUTORA_UPDATE = "No se actualizó unidad ejecutora.";
        public const string EX_UNIDAD_EJECUTORA_DELETE = "No se eliminó unidad ejecutora.";

        public const string EX_PARENTESCO_CREATE = "No se registró parentesco.";
        public const string EX_PARENTESCO_UPDATE = "No se actualizó parentesco.";
        public const string EX_PARENTESCO_DELETE = "No se eliminó parentesco.";

        public const string EX_PERSONA_CREATE = "No se registró persona.";
        public const string EX_PERSONA_DELETE = "No se eliminó persona.";
        public const string EX_PERSONA_UPDATE = "No se actualizó persona.";
        public const string EX_SERVIDOR_PUBLICO_CREATE = "No se registró servidor público.";
        public const string EX_SERVIDOR_PUBLICO_DELETE = "No se eliminó servidor público.";
        public const string EX_FAMILIAR_SERVIDOR_PUBLICO_CREATE = "No se registró familiar servidor público.";

        public const string EX_CENTRO_TRABAJO_CREATE = "No se registró centro trabajo.";
        public const string EX_CENTRO_TRABAJO_UPDATE = "No se actualizó centro trabajo.";
        public const string EX_CENTRO_TRABAJO_DELETE = "No se eliminó centro trabajo.";

        public const string EX_INSTITUCION_EDUCATIVA_CREATE = "No se registró institución educativa.";
        public const string EX_INSTITUCION_EDUCATIVA_UPDATE = "No se actualizó institución educativa.";
        public const string EX_INSTITUCION_EDUCATIVA_DELETE = "No se eliminó institución educativa.";


        public const string EX_SERVIDOR_PUBLICO_UPDATE = "No se actualizó servidor público.";

        public const string EX_SERVIDOR_PUBLICO_SEARCH = "Debe ingresar al menos un criterio de búsqueda.";
        public const string EX_ESTADO_LICENCIA_NOT_FOUND = "No se encuentra registrado estado de licencia.";
        public const string EX_LICENCIA_FECHAS = "Fecha fin debe ser mayor a fecha de inicio.";
        public const string EX_LICENCIA_DIAS_INVALIDO = "Cantidad de días de licencia no es válida, especifique mayor rango de fechas.";

        public const string EX_LICENCIA_TRASLPAPE_FECHAS = "La fecha seleccionada interfiere con la vigencia de otra licencia registrada, seleccionar otra fecha.";
        public const string EX_LICENCIA_DIAS_IGUAL_A = "La cantidad de días de la licencia se encuentra fuera de lo normado. Igual a {0} días.";
        public const string EX_LICENCIA_DIAS_NO_SUPERAR_A = "La cantidad de días de la licencia se encuentra fuera de lo normado. Máximo {0} días.";
        public const string EX_LICENCIA_FAMILIAR_ADOPCION_NOT_FOUND = "Familiar de servidor público no se encontró para registrar adopción.";
        public const string EX_LICENCIA_ADOPCION_EDAD_PERMITO = "Debe seleccionar hijo(a) menor o igual {0} años.";
        public const string EX_LICENCIA_SERVIDOR_PUBLICO_CESADO = "Servidor público se encuentra cesado, no se puede registrar licencia.";
        public const string EX_LICENCIA_GENERO_NO_PERMITIDO = "No se puede registrar la licencia, eligir otro servidor público.";
        public const string EX_LICENCIA_PREQUISITO_MATERNIDAD = "Para registrar esta licencia primero debe registrar la licencia por maternidad.";
        public const string EX_LICENCIA_POR_ADOPCION_EXISTE = "El servidor público ya cuenta con esta misma licencia.";

        public const string EX_MODALIDAD_EDUCATIVA_VALIDATION = "No se lista el nivel educativo porque la modalidad ingresada no es válido.";
        public const string EX_UNPROCESSABLE_ENTITY = "No ingresó todos los parámetros obligatorios.";
        public const string EX_DRE_VALIDATION = "No se lista la ugel porque el dre ingresado no es válido.";
        public const string EX_INSTITUCION_EDUCATIVA_NOT_FOUND = "No se encuentra registro de la institución educativa por código modular.";
        public const string EX_NIVEL_INSTANCIA_VALIDAR = "El código de nivel instancia no es válido.";
        public const string EX_INSTANCIA_VALIDAR = "El código de instancia no es válido.";
        public const string PAGINA_ACTUAL_VALIDAR = "Página actual no puede ser menor a cero.";
        public const string TAMANO_PAGINA_VALIDAR = "Tamaño de página actual no puede ser menor a cero.";

        public const string EX_SEARCH_ENTITY = "Debe ingresar al menos un criterio de búsqueda.";
        public const string EX01_M01_CENTRO_TRABAJO = EX_SEARCH_ENTITY;
        public const string EX_NIVEL_ORGANIZACIONAL_VALIDAR = "El código de nivel organizacional no es válido.";
        public const string EX_CATALOGO_SEARCH = "Debe ingresar al menos un criterio de búsqueda.";
        public const string EX_REQUERID_ENTITY = "No ingresó los campos obligatorios, por favor ingrese todos los campos obligatorios y proceda con la modificación de la sección, gracias.";

        public const string FECHA_INVALIDA = "Invalid date";
        public const string EX_CATALOGO_NOT_FOUND = "No se pudo encontrar el registro de catalogo.";

        public const string EX_REGIMEN_LABORAL_NOT_FOUND = "No existe régimen laboral.";
        public const string EX_PERSONA_NOT_FOUND = "No existe persona";
        public const string EX_SITUACION_LABORAL_NOT_FOUND = "No existe situación laboral";
        public const string EX_CARGO_NOT_FOUND = "No existe cargo";
        public const string EX_CONDICION_LABORAL_NOT_FOUND = "No existe condición laboral";
        public const string EX_SERVIDOR_PUBLICO_NOT_FOUND = "No existe servidor público";

        public const string EX_GENERO_NOT_FOUND = "No existe género";
        public const string EX_TIPO_DOCUMENTO_IDENTIDAD_NOT_FOUND = "No existe tipo documento identidad";
        public const string EX_ESTADO_CIVIL_NOT_FOUND = "No existe estado civil";
        public const string EX_OTRA_INSTANCIA_NOT_FOUND = "No existe otra instancia";
        public const string EX_DRE_NOT_FOUND = "No existe dre";
        public const string EX_UGEL_NOT_FOUND = "No existe ugel";
        public const string EX_TIPO_CENTRO_TRABAJO_NOT_FOUND = "No existe tipo centro trabajo";
        public const string EX_UNIDAD_EJECUTORA_NOT_FOUND = "No existe unidad ejecutora";
        public const string EX_PARENTESCO_NOT_FOUND = "No existe parentesco";

        // Rabbit
        public const string NOMBRE_SISTEMA = "ecovave-backend";
        public const string RABBIT_EXCHANGE_PERSONAL_LICENCIAS = "ecovave-alertas-direct-exchange";
        public const string RABBIT_QUEUE_PERSONAL_LICENCIAS = "ecovave-alertas-queue";

        // public const string RABBIT_EXCHANGE_PERSONAL_PROYECTOSRESOLUCION = "rrhh-personal-proyectosresolucion-direct-exchange";
        // public const string RABBIT_QUEUE_PERSONAL_PROYECTOSRESOLUCION = "rrhh-personal-proyectosresolucion-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_CENTROTRABAJO = "rrhh-negocio-comunes-maestros-centrotrabajo-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_CENTROTRABAJO = "rrhh-negocio-comunes-maestros-centrotrabajo-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_INSTITUCIONEDUCATIVA = "rrhh-negocio-comunes-maestros-institucioneducativa-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_INSTITUCIONEDUCATIVA = "rrhh-negocio-comunes-maestros-institucioneducativa-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_REGIMENLABORAL = "rrhh-negocio-comunes-maestros-regimenlaboral-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_REGIMENLABORAL = "rrhh-negocio-comunes-maestros-regimenlaboral-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_UGEL = "rrhh-negocio-comunes-maestros-ugel-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_UGEL = "rrhh-negocio-comunes-maestros-ugel-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_DRE = "rrhh-negocio-comunes-maestros-dre-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_DRE = "rrhh-negocio-comunes-maestros-dre-licencias-queue";

        public const string RABBIT_EXCHANGE_RESOLUCIONES_APROBADAS = "rrhh-resoluciones-aprobadas-fanout-exchange";
        public const string RABBIT_QUEUE_RESOLUCIONES_APROBADAS_LICENCIAS = "rrhh-resoluciones-aprobadas-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_PERSONA = "rrhh-negocio-comunes-maestros-persona-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_PERSONA = "rrhh-negocio-comunes-maestros-persona-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_SERVIDORPUBLICO = "rrhh-negocio-comunes-maestros-servidorpublico-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_SERVIDORPUBLICO = "rrhh-negocio-comunes-maestros-servidorpublico-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_FAMILIARSERVIDORPUBLICO = "rrhh-negocio-comunes-maestros-familiarservidorpublico-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_FAMILIARSERVIDORPUBLICO = "rrhh-negocio-comunes-maestros-familiarservidorpublico-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_CATALOGO = "rrhh-negocio-comunes-maestros-catalogo-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_CATALOGO = "rrhh-negocio-comunes-maestros-catalogo-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_CATALOGOITEM = "rrhh-negocio-comunes-maestros-catalogoitem-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_CATALOGOITEM = "rrhh-negocio-comunes-maestros-catalogoitem-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_UNIDADEJECUTORA = "rrhh-negocio-comunes-maestros-unidadejecutora-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_UNIDADEJECUTORA = "rrhh-negocio-comunes-maestros-unidadejecutora-licencias-queue";

        public const string RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_PARENTESCO = "rrhh-negocio-comunes-maestros-parentesco-fanout-exchange";
        public const string RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_PARENTESCO = "rrhh-negocio-comunes-maestros-parentesco-licencias-queue";

    }
}
