namespace ecovave.common
{
    public enum TablaCatalogoType
    {
        TIPO_DOCUMENTO_IDENTIDAD = 1,
        ENTIDAD_ATENCION = 2,
        TIPO_DESCANSO = 3,
        TIPO_SUSTENTO = 4,
        TIPO_FORMATO = 5,
        TIPO_RESOLUCION = 6,
        TIPO_PARTO = 7,
        TIPO_CERTIFICADO = 8,
        SITUACION_LABORAL = 9,
        ESTADO_CIVIL = 10,
        TIPO_CENTRO_TRABAJO = 11,
        GENERO = 12,
        CARGO = 13,
        ESTADO_LICENCIA = 14,
        LUGAR_DESCESO = 15,
        TIPO_DIAGNOSTICO = 16,
        NIVEL_INSTANCIA = 17,
        TIPO_GESTION_INSTITUCION_EDUCATIVA = 18,
        ORIGEN_REGISTRO = 19,
        CONDICION_LABORAL = 20,
    }

    public enum TablaTipoCentro
    {
        MINEDU = 4,
        DRE = 1,
        UGEL = 2
    }

    public enum EstadoLicenciaType
    {
        REGISTRADO = 1,
        PENDIENTE_DE_PROYECTO = 2,
        EN_PROYECTO = 3,
        RESOLUCION = 4,
        ELIMINADO = 5,
        ENVIADO = 6
    }

    public enum MotivoAccionType
    {
        POR_ENFERMEDAD = 70,
        POR_INCAPACIDAD_TEMPORAL = 82,
        POR_INCAPACIDAD_TERMPORAL_PARA_EL_TRABAJO = 83,
        POR_MATERNIDAD = 85,
        EXTENSION_LICENCIA_POR_MATERNIDAD = 86,
        POR_ADOPCION = 59,
        POR_FALLECIMIENTO_DE_PADRES_CONYUGE_E_HIJOS = 65,
        POR_PATERNIDAD = 87,
        POR_ENFERMEDAD_GRAVE_TERMINAL_O_POR_ACCIDENTE_GRAVE = 95,
        POR_DESCANSO_PRE_Y_POST_NATAL = 68,
        POR_GRAVIDEZ = 81,
        POR_ENFERMEDAD_O_ACCIDENTE_O_INCAPACIDAD_TEMPORAL = 73,
        POR_ASUMIR_REPRESENTACION_OFICIAL_DEL_ESTADO_PERUANO = 61,
        POR_DESEMPENO_DE_CARGO_DE_CONSEJERO_REGIONAL_O_REGIDOR_MUNICIPAL = 69
    }

    public enum TablaNivelOrganizacional
    {
        NIVEL_ORGANIZACIONAL1 = 15,
        NIVEL_ORGANIZACIONAL2 = 16,
        NIVEL_ORGANIZACIONAL3 = 17
    }

    public enum TablaNivelInstancia
    {
        MINEDU = 12,
        DRE = 13,
        UGEL = 14
    }

    public enum TablaTipoCentroTrabajo
    {
        Minedu = 1,
        SedeAdministrativaDRE = 2,
        InstitucionEducativaDRE = 3,
        InstitutoSuperiorDRE = 4,
        SedeAdministrativaUGEL = 5,
        InstitucionEducativaUgel = 6
    }

    public enum TablaOrigenRegistroType
    {
        REGISTRO_LICENCIA = 1,
        GENERARACION_PROYECTO = 2
    }

    public enum AccionType
    {
        LICENCIA_CON_GOCE_DE_REMUNERACIONES = 28,
        LICENCIA_SIN_GOCE_DE_REMUNERACIONES = 29,
    }

    public enum TipoDescansoType
    {
        PRE_NATAL = 1,
        POST_NATAL = 2,
        POR_ENFERMEDAD = 3,
        POR_ACCIDENTE = 4,
        PARTO_MULTIPLE = 5,
        RECIEN_NACIDO_POR_DISCAPACIDAD = 6,
        PRE_NATAL_Y_POST_NATAL = 7,
    }

    public enum GrupoReglaType
    {
        REGLAS_CON_TIPO_DE_DESCANSO = 1,
        REGLAS_CON_TIPO_DE_PARTO = 2,
        REGLAS_CON_LUGAR_DESCESO_REGIMEN_LABORAL = 3,
        REGLAS_CON_MOTIVO_DE_ACCION = 4,
        REGLAS_CON_REGIMEN_LABORAL = 5
    }

    public enum SituacionServidorPublicoType
    {
        CESADO = 1,
        ACTIVO = 2
    }

    public enum GeneroType
    {
        FEMENINO = 1,
        MASCULINO = 2
    }

    public enum OrigenEliminacionType
    {
        REGISTRADO = 1,
        ENVIADO = 2
    }
}

public class GenericResult<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}

public static class ResultadoOperacion
{
    public const int SUCCESS = 1;
    public const int FAIL = 0;
}