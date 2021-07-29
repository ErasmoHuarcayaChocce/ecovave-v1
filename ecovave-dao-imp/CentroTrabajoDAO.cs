using ecovave.common;
using ecovave.dao.intf;
using ecovave.model;
using minedu.tecnologia.util.lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ecovave.dao.imp
{
    public class CentroTrabajoDAO : DAOBase, ICentroTrabajoDAO
    {
        public CentroTrabajoDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public CentroTrabajoDAO()
        {
        }

        private CentroTrabajoGrilla CargarCentroTrabajoGrilla(SqlDataReader dr)
        {
            int index = 0;
            CentroTrabajoGrilla model = new CentroTrabajoGrilla();
            model.registro = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.totalRegistro = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.idCentroTrabajo = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.codigoCentroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.id = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.centroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.instancia = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.subinstancia = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idTipoCentroTrabajo = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.tipoCentroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.modalidadEducativa = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idNivelEducativo = SqlHelper.GetNullableInt32(dr, index);
            index = index + 1;
            model.nivelEducativo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idUnidadEjecutora = SqlHelper.GetNullableInt32(dr, index);
            index = index + 1;
            model.tieneEstructuraOrganica = SqlHelper.GetBoolean(dr, index);
            index = index + 1;
            model.idNivelSede = SqlHelper.GetInt32(dr, index);

            return model;
        }

        public async Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajo(CentroTrabajoFiltro request)
        {
            string sql = @" 
                        SELECT
                        CAST(ROW_NUMBER() OVER(ORDER BY a.ID_CENTRO_TRABAJO ASC) as int) registro,
                        COUNT(1) OVER() AS totalRegistro,
                        a.ID_CENTRO_TRABAJO idCentroTrabajo,
                        a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,                        
                        (SELECT 
	                        CASE (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN 0 THEN a.ID_INSTITUCION_EDUCATIVA
	                        ELSE
		                        (SELECT 
			                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
			                        WHEN @idNivelSedeMinedu THEN a.ID_OTRA_INSTANCIA
	                                WHEN @idNivelSedeDre THEN a.ID_DRE
	                                WHEN @idNivelSedeUgel THEN a.ID_UGEL
		                        END)
                        END) id,                        
                        (SELECT 
	                        CASE (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN 0 THEN isnull((SELECT INSTITUCION_EDUCATIVA FROM [dbo].[institucion_educativa] WITH (NOLOCK) WHERE ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA), ' ')
	                        ELSE
		                        (SELECT 
			                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
			                        WHEN @idNivelSedeMinedu THEN isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ')
			                        WHEN @idNivelSedeDre THEN isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ')
			                        WHEN @idNivelSedeUgel THEN isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ')
		                        END)
                        END) centroTrabajo,
                        (SELECT 
	                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN @idNivelSedeMinedu THEN isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ')
	                        ELSE isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ')
                        END) instancia,
                        (SELECT 
	                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN @idNivelSedeMinedu THEN ' '
	                        WHEN @idNivelSedeDre THEN 'SEDE DRE'
	                        ELSE isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ')
                        END) subinstancia,
                        a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
                        (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
                        /*isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ') */ '' AS modalidadEducativa,
                        /*b.ID_NIVEL_EDUCATIVO */ 0 AS idNivelEducativo, 
                        /*isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ') */ '' AS nivelEducativo,
                        (SELECT 
	                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN @idNivelSedeMinedu THEN (SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA)
	                        WHEN @idNivelSedeDre THEN 0 /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE)*/
	                        WHEN @idNivelSedeUgel THEN 0 /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL)*/
                        END) idUnidadEjecutora,
                        (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
                        (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
                        FROM [dbo].[centro_trabajo] a WITH (NOLOCK)
                        LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
                        -- LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1
                        WHERE a.ACTIVO = 1
                        and a.ID_TIPO_CENTRO_TRABAJO = @idTipoCentroTrabajo";

            switch (request.idNivelInstancia)
            {
                case (int)TablaNivelInstancia.MINEDU:
                    sql = sql + @" 
                        and a.ID_OTRA_INSTANCIA = isnull(@idInstancia, a.ID_OTRA_INSTANCIA) 
                        and a.ID_DRE is null
                        and a.ID_UGEL is null
                    ";
                    break;
                case (int)TablaNivelInstancia.DRE:
                    switch (request.idTipoCentroTrabajo)
                    {
                        case (int)TablaTipoCentroTrabajo.SedeAdministrativaDRE:
                            sql = sql + @"
                                and a.ID_DRE = isnull(@idInstancia, a.ID_DRE) 
                                and a.ID_UGEL is null
                            ";
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitucionEducativaDRE:
                            sql = sql + @"
                                and a.ID_DRE = isnull(@idInstancia, a.ID_DRE) 
                                and a.ID_UGEL is not null
                                and b.INSTITUCION_EDUCATIVA like '%' + isnull(@institucionEducativa, b.INSTITUCION_EDUCATIVA) + '%'
                            ";
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitutoSuperiorDRE:
                            sql = sql + @"
                                and a.ID_DRE = isnull(@idInstancia, a.ID_DRE) 
                                and a.ID_UGEL is not null
                                and b.INSTITUCION_EDUCATIVA like '%' + isnull(@institucionEducativa, b.INSTITUCION_EDUCATIVA) + '%'
                            ";
                            break;
                    }
                    break;
                case (int)TablaNivelInstancia.UGEL:
                    switch (request.idTipoCentroTrabajo)
                    {
                        case (int)TablaTipoCentroTrabajo.SedeAdministrativaUGEL:
                            sql = sql + @"
                                and a.ID_DRE = isnull(@idInstancia, a.ID_DRE)
                                and a.ID_UGEL = isnull(@idSubinstancia, a.ID_UGEL) ";
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitucionEducativaUgel:
                            sql = sql + @"
                                and a.ID_DRE = isnull(@idInstancia, a.ID_DRE) 
                                and a.ID_UGEL = isnull(@idSubinstancia, a.ID_UGEL)
                                and b.INSTITUCION_EDUCATIVA like '%' + isnull(@institucionEducativa, b.INSTITUCION_EDUCATIVA) + '%'
                            ";
                            break;
                    }
                    break;
            }

            sql = sql + @"
                ORDER BY a.ID_CENTRO_TRABAJO ";

            sql = sql + SqlHelper.PaginacionPorNumeroPagina(request.paginaActual, request.tamanioPagina);

            SqlParameter[] parametro = new SqlParameter[8];
            parametro[0] = new SqlParameter("@idNivelInstancia", SqlDbType.Int);
            parametro[0].Value = request.idNivelInstancia;
            parametro[1] = new SqlParameter("@idInstancia", SqlDbType.Int);
            parametro[1].Value = request.idInstancia;
            parametro[2] = new SqlParameter("@idSubinstancia", SqlDbType.Int);
            parametro[2].Value = request.idSubinstancia;
            parametro[3] = new SqlParameter("@idTipoCentroTrabajo", SqlDbType.Int);
            parametro[3].Value = request.idTipoCentroTrabajo;
            parametro[4] = new SqlParameter("@institucionEducativa", SqlDbType.VarChar, 200);
            parametro[4].Value = request.institucionEducativa;

            parametro[5] = new SqlParameter("@idNivelSedeMinedu", SqlDbType.Int);
            parametro[5].Value = (int)TablaNivelInstancia.MINEDU;
            parametro[6] = new SqlParameter("@idNivelSedeDre", SqlDbType.Int);
            parametro[6].Value = (int)TablaNivelInstancia.DRE;
            parametro[7] = new SqlParameter("@idNivelSedeUgel", SqlDbType.Int);
            parametro[7].Value = (int)TablaNivelInstancia.UGEL;

            SqlDataReader dr = null;
            SqlConnection cn = null;

            try
            {
                List<CentroTrabajoGrilla> response = new List<CentroTrabajoGrilla>();
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (!dr.HasRows) return response;
                while (dr.ReadAsync().Result)
                {
                    CentroTrabajoGrilla model = CargarCentroTrabajoGrilla(dr);
                    response.Add(model);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }
        }

        public async Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajoDREUGEL(CentroTrabajoFiltro request)
        {
            string sql = @" 
                    SELECT 
                    CAST(ROW_NUMBER() OVER(ORDER BY a.idCentroTrabajo ASC) as int) registro,
                    COUNT(1) OVER() AS totalRegistro,
                    a.idCentroTrabajo,
                    a.codigoCentroTrabajo,
                    a.id,
                    a.centroTrabajo,
                    a.instancia,
                    a.subinstancia,
                    a.idTipoCentroTrabajo,
                    a.tipoCentroTrabajo,
                    a.modalidadEducativa,	
                    a.idNivelEducativo,
                    a.nivelEducativo,
                    a.idUnidadEjecutora,
                    a.tieneEstructuraOrganica,
                    a.idNivelSede
                    FROM ( ";

            if (request.idNivelInstancia == (int)TablaNivelInstancia.MINEDU && request.idInstancia == 1)
            {
                sql = sql + @" 
                             SELECT 
                                a.ID_CENTRO_TRABAJO idCentroTrabajo,
                                a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,
                                a.ID_OTRA_INSTANCIA id,
                                isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ') centroTrabajo,
                                isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ') instancia,
                                ' ' subinstancia,
                                a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
                                (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
                                /*isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ')*/ '' AS modalidadEducativa,
                                /*b.ID_NIVEL_EDUCATIVO*/ 0 AS idNivelEducativo, 
                                /*isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ')*/ '' AS nivelEducativo,
                                /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE)*/ 0 AS idUnidadEjecutora,
                                (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
                                (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
                                FROM (
	                                SELECT a.ID_CENTRO_TRABAJO, a.ID_TIPO_CENTRO_TRABAJO, a.ID_OTRA_INSTANCIA, a.ID_DRE, a.ID_UGEL, a.ID_INSTITUCION_EDUCATIVA, a.CODIGO_CENTRO_TRABAJO
	                                FROM [dbo].[centro_trabajo] a WITH (NOLOCK)
	                                WHERE a.ACTIVO = 1
	                                and a.ID_OTRA_INSTANCIA = @idInstancia  
	                                and a.ID_DRE is null
	                                and a.ID_UGEL is null 
	                                and a.ID_INSTITUCION_EDUCATIVA is null
                                ) a
                                LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
                                -- LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1
	                    UNION ALL ";
            }

            if (request.idInstancia == request.idSubinstancia || request.idSubinstancia == null)
            {
                sql = sql + @" 
                       SELECT 
	                    a.ID_CENTRO_TRABAJO idCentroTrabajo,
	                    a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,
	                    a.ID_DRE id,
	                    isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ') centroTrabajo,
	                    isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ') instancia,
	                    'SEDE DRE' subinstancia,
	                    a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
	                    (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
	                    /*isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ') */ '' AS modalidadEducativa,
	                    /*b.ID_NIVEL_EDUCATIVO */ 0 AS idNivelEducativo, 
	                    /*isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ') */ '' AS nivelEducativo,
	                    /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE)*/ 0 AS idUnidadEjecutora,
	                    (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
	                    (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
	                    FROM (
		                    SELECT a.ID_CENTRO_TRABAJO, a.ID_TIPO_CENTRO_TRABAJO, a.ID_OTRA_INSTANCIA, a.ID_DRE, a.ID_UGEL, a.ID_INSTITUCION_EDUCATIVA, a.CODIGO_CENTRO_TRABAJO
		                    FROM [dbo].[centro_trabajo] a WITH (NOLOCK)
		                    WHERE a.ACTIVO = 1
		                    and a.ID_DRE = @idInstancia 		                    
							and a.ID_UGEL is null 
		                    and a.ID_INSTITUCION_EDUCATIVA is null
	                    ) a
	                    LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
	                    -- LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1
	                    UNION ALL ";
            }

            sql = sql + @" 
                        SELECT 
	                    a.ID_CENTRO_TRABAJO idCentroTrabajo,
	                    a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,
	                    a.ID_UGEL id,
	                    isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ') centroTrabajo,
	                    isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ') instancia,
	                    isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ') subinstancia,
	                    a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
	                    (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
	                    /*isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ') */ '' AS modalidadEducativa,
	                    /*b.ID_NIVEL_EDUCATIVO */ 0 AS idNivelEducativo, 
	                    /*isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ') */ '' AS nivelEducativo,
	                    /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL) */ 0 AS idUnidadEjecutora,
	                    (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
	                    (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
	                    FROM (
		                    SELECT a.ID_CENTRO_TRABAJO, a.ID_TIPO_CENTRO_TRABAJO, a.ID_OTRA_INSTANCIA, a.ID_DRE, a.ID_UGEL, a.ID_INSTITUCION_EDUCATIVA, a.CODIGO_CENTRO_TRABAJO
		                    FROM [dbo].[centro_trabajo] a WITH (NOLOCK)	
		                    WHERE a.ACTIVO = 1
		                    and a.ID_DRE = isnull(@idInstancia, a.ID_DRE)
                        ";

            if (request.idInstancia == request.idSubinstancia || request.idSubinstancia == null)
            {
                sql = sql + @" 
		                    and a.ID_UGEL is not null 
                        ";
            }
            else
            {
                sql = sql + @" 
		                    and a.ID_UGEL = @idSubinstancia 
                        ";
            }

            sql = sql + @"
                            and a.ID_INSTITUCION_EDUCATIVA is null
	                    ) a
	                    LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
	                    -- LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1
	                    UNION ALL ";

            sql = sql + @" 
                        SELECT 
	                    a.ID_CENTRO_TRABAJO idCentroTrabajo,
	                    a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,
	                    a.ID_INSTITUCION_EDUCATIVA id,
	                    isnull((SELECT INSTITUCION_EDUCATIVA FROM [dbo].[institucion_educativa] WITH (NOLOCK) WHERE ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA), ' ') centroTrabajo,
	                    isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ') instancia,
	                    isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ') subinstancia,
	                    a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
	                    (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
	                    /*isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ') */ '' AS modalidadEducativa,
	                    /*b.ID_NIVEL_EDUCATIVO */ 0 AS idNivelEducativo, 
	                    /*isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ')*/ '' AS nivelEducativo,
	                    /*(SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL)*/ 0 AS idUnidadEjecutora,
	                    (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
	                    (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
	                    FROM (
		                    SELECT a.ID_CENTRO_TRABAJO, a.ID_TIPO_CENTRO_TRABAJO, a.ID_OTRA_INSTANCIA, a.ID_DRE, a.ID_UGEL, a.ID_INSTITUCION_EDUCATIVA, a.CODIGO_CENTRO_TRABAJO
		                    FROM [dbo].[centro_trabajo] a WITH (NOLOCK)		
		                    WHERE a.ACTIVO = 1
                            and a.ID_DRE = isnull(@idInstancia, a.ID_DRE)
                        ";

            if (request.idInstancia == request.idSubinstancia || request.idSubinstancia == null)
            {
                sql = sql + @" 
		                    and a.ID_UGEL is not null 
                        ";
            }
            else
            {
                sql = sql + @" 
		                    and a.ID_UGEL = @idSubinstancia 
                        ";
            }

            sql = sql + @"                             
		                    and a.ID_INSTITUCION_EDUCATIVA is not null 
		                    and a.ID_TIPO_CENTRO_TRABAJO is not null			                 
	                    ) a
	                    LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
	                    -- LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1 ";

            sql = sql + @"
                    ) a 
                    ORDER BY a.idCentroTrabajo ";

            sql = sql + SqlHelper.PaginacionPorNumeroPagina(request.paginaActual, request.tamanioPagina);

            SqlParameter[] parametro = new SqlParameter[5];
            parametro[0] = new SqlParameter("@idNivelInstancia", SqlDbType.Int);
            parametro[0].Value = request.idNivelInstancia;
            parametro[1] = new SqlParameter("@idInstancia", SqlDbType.Int);
            parametro[1].Value = request.idInstancia;
            parametro[2] = new SqlParameter("@idSubinstancia", SqlDbType.Int);
            parametro[2].Value = request.idSubinstancia;
            parametro[3] = new SqlParameter("@idTipoCentroTrabajo", SqlDbType.Int);
            parametro[3].Value = request.idTipoCentroTrabajo;
            parametro[4] = new SqlParameter("@institucionEducativa", SqlDbType.VarChar, 200);
            parametro[4].Value = request.institucionEducativa;

            SqlDataReader dr = null;
            SqlConnection cn = null;

            try
            {
                List<CentroTrabajoGrilla> response = new List<CentroTrabajoGrilla>();
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (!dr.HasRows) return response;
                while (dr.ReadAsync().Result)
                {
                    CentroTrabajoGrilla model = CargarCentroTrabajoGrilla(dr);
                    response.Add(model);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }
        }

        private CentroTrabajoRegistro CargarCentroTrabajoRegistro(SqlDataReader dr)
        {
            int index = 0;
            CentroTrabajoRegistro model = new CentroTrabajoRegistro();
            model.idCentroTrabajo = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.codigoCentroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.id = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.centroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.instancia = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.subinstancia = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idTipoCentroTrabajo = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.tipoCentroTrabajo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.modalidadEducativa = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idNivelEducativo = SqlHelper.GetNullableInt32(dr, index);
            index = index + 1;
            model.nivelEducativo = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.idUnidadEjecutora = SqlHelper.GetNullableInt32(dr, index);
            index = index + 1;
            model.tieneEstructuraOrganica = SqlHelper.GetBoolean(dr, index);
            index = index + 1;
            model.idNivelSede = SqlHelper.GetInt32(dr, index);

            return model;
        }

        public async Task<CentroTrabajoRegistro> GetCentroTrabajo(CentroTrabajoConsulta request)
        {
            string sql = @" 
                        SELECT 
                        a.ID_CENTRO_TRABAJO idCentroTrabajo,
                        a.CODIGO_CENTRO_TRABAJO codigoCentroTrabajo,
                        (SELECT 
	                        CASE (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN 0 THEN a.ID_INSTITUCION_EDUCATIVA
	                        ELSE
		                        (SELECT 
			                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
			                        WHEN @idNivelSedeMinedu THEN a.ID_OTRA_INSTANCIA
	                                WHEN @idNivelSedeDre THEN a.ID_DRE
	                                WHEN @idNivelSedeUgel THEN a.ID_UGEL
		                        END)
                        END) id,                        
                        (SELECT 
	                        CASE (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN 0 THEN isnull((SELECT INSTITUCION_EDUCATIVA FROM [dbo].[institucion_educativa] WITH (NOLOCK) WHERE ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA), ' ')
	                        ELSE
		                        (SELECT 
			                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
			                        WHEN @idNivelSedeMinedu THEN isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ')
			                        WHEN @idNivelSedeDre THEN isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ')
			                        WHEN @idNivelSedeUgel THEN isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ')
		                        END)
                        END) centroTrabajo,                        
                        (SELECT 
	                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)	
                            WHEN @idNivelSedeMinedu THEN isnull((SELECT DESCRIPCION_OTRA_INSTANCIA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA), ' ')
	                        ELSE isnull((SELECT DESCRIPCION_DRE FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE), ' ')
                        END) instancia,
                        (SELECT 
	                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN @idNivelSedeMinedu THEN ' '
	                        WHEN @idNivelSedeDre THEN 'SEDE DRE'
	                        ELSE isnull((SELECT DESCRIPCION_UGEL FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL), ' ')
                        END) subinstancia,
                        a.ID_TIPO_CENTRO_TRABAJO idTipoCentroTrabajo,
                        (SELECT DESCRIPCION_TIPO_CENTRO_TRABAJO FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tipoCentroTrabajo,
                        isnull((SELECT DESCRIPCION_MODALIDAD_EDUCATIVA FROM [dbo].[modalidad_educativa] WITH (NOLOCK) WHERE ID_MODALIDAD_EDUCATIVA = c.ID_MODALIDAD_EDUCATIVA), ' ') modalidadEducativa,
                        b.ID_NIVEL_EDUCATIVO idNivelEducativo, 
                        isnull(c.DESCRIPCION_NIVEL_EDUCATIVO, ' ') nivelEducativo,                        
                        (SELECT 
	                        CASE (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
	                        WHEN 0 THEN (SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL)
	                        ELSE
		                        (SELECT 
			                        CASE (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO)
			                        WHEN @idNivelSedeMinedu THEN (SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[otra_instancia] WITH (NOLOCK) WHERE ID_OTRA_INSTANCIA = a.ID_OTRA_INSTANCIA)
	                                WHEN @idNivelSedeDre THEN (SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[dre] WITH (NOLOCK) WHERE ID_DRE = a.ID_DRE)
	                                WHEN @idNivelSedeUgel THEN (SELECT ID_UNIDAD_EJECUTORA FROM [dbo].[ugel] WITH (NOLOCK) WHERE ID_UGEL = a.ID_UGEL)
		                        END)
                        END) idUnidadEjecutora,
                        (SELECT TIENE_ESTRUCTURA_ORGANICA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) tieneEstructuraOrganica,
                        (SELECT ID_NIVEL_INSTANCIA FROM [dbo].[tipo_centro_trabajo] WITH (NOLOCK) WHERE ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO) idNivelSede
                        FROM [dbo].[centro_trabajo] a WITH (NOLOCK)
                        INNER JOIN [dbo].[tipo_centro_trabajo] d WITH (NOLOCK) on d.ID_TIPO_CENTRO_TRABAJO = a.ID_TIPO_CENTRO_TRABAJO and d.ACTIVO = 1
                        LEFT JOIN [dbo].[institucion_educativa] b WITH (NOLOCK) on b.ID_INSTITUCION_EDUCATIVA = a.ID_INSTITUCION_EDUCATIVA and b.ACTIVO = 1
                        LEFT JOIN [dbo].[nivel_educativo] c WITH (NOLOCK) on c.ID_NIVEL_EDUCATIVO = b.ID_NIVEL_EDUCATIVO and c.ACTIVO = 1
                        WHERE a.ACTIVO = 1
                        and a.CODIGO_CENTRO_TRABAJO = @codigoCentroTrabajo";

            switch (request.idNivelInstanciaPassport)
            {
                case (int)TablaNivelInstancia.MINEDU:
                    sql = sql + @" ";
                    break;
                case (int)TablaNivelInstancia.DRE:
                    sql = sql + @" 
                        and d.ID_NIVEL_INSTANCIA in (@idNivelSedeDre, @idNivelSedeUgel) 
                        and a.ID_DRE = @idEntidadPassport ";
                    break;
                case (int)TablaNivelInstancia.UGEL:
                    sql = sql + @"                                
                        and d.ID_NIVEL_INSTANCIA = @idNivelSedeUgel
                        and a.ID_UGEL = @idEntidadPassport ";
                    break;
            }

            SqlParameter[] parametro = new SqlParameter[5];
            parametro[0] = new SqlParameter("@codigoCentroTrabajo", SqlDbType.VarChar, 10);
            parametro[0].Value = request.codigoCentroTrabajo;
            parametro[1] = new SqlParameter("@idEntidadPassport", SqlDbType.Int);
            parametro[1].Value = request.idEntidadPassport;

            parametro[2] = new SqlParameter("@idNivelSedeMinedu", SqlDbType.Int);
            parametro[2].Value = (int)TablaNivelInstancia.MINEDU;
            parametro[3] = new SqlParameter("@idNivelSedeDre", SqlDbType.Int);
            parametro[3].Value = (int)TablaNivelInstancia.DRE;
            parametro[4] = new SqlParameter("@idNivelSedeUgel", SqlDbType.Int);
            parametro[4].Value = (int)TablaNivelInstancia.UGEL;

            SqlDataReader dr = null;
            SqlConnection cn = null;

            CentroTrabajoRegistro response = null;

            try
            {
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (!dr.HasRows) return response;
                if (dr.ReadAsync().Result) response = CargarCentroTrabajoRegistro(dr);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }
        }

        public async Task<int> GetValidarCentroTrabajo(string codigoCentroTrabajo)
        {
            int response = 0;
            const string sql = @" 
                        SELECT count(1) registro 
                        FROM [dbo].[centro_trabajo] a WITH (NOLOCK) 
                        WHERE a.ACTIVO = 1 
                        and a.CODIGO_CENTRO_TRABAJO = @codigoCentroTrabajo;";

            SqlParameter[] parametro = new SqlParameter[1];
            parametro[0] = new SqlParameter("@codigoCentroTrabajo", SqlDbType.VarChar, 10);
            parametro[0].Value = codigoCentroTrabajo;

            SqlDataReader dr = null;
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (dr.HasRows)
                    if (dr.ReadAsync().Result) response = SqlHelper.GetInt32(dr, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }

            return response;
        }

        public Task<int> CrearCentroTrabajoReplica(CentroTrabajo request)
        {
            int response = 0;
            const string sql = @"INSERT INTO dbo.centro_trabajo
                                            (ID_CENTRO_TRABAJO, 
                                             ID_TIPO_CENTRO_TRABAJO, 
                                             ID_DRE, 
                                             ID_UGEL, 
                                             ID_INSTITUCION_EDUCATIVA, 
                                             CODIGO_CENTRO_TRABAJO, 
                                             ACTIVO, 
                                             FECHA_CREACION, 
                                             USUARIO_CREACION, 
                                             IP_CREACION
                                            )
                                            OUTPUT INSERTED.ID_CENTRO_TRABAJO
                                            VALUES
                                            (NEXT VALUE FOR dbo.seq_centro_trabajo, 
                                             @ID_TIPO_CENTRO_TRABAJO, 
                                             @ID_DRE, 
                                             @ID_UGEL, 
                                             @ID_INSTITUCION_EDUCATIVA, 
                                             @CODIGO_CENTRO_TRABAJO, 
                                             @ACTIVO, 
                                             @FECHA_CREACION, 
                                             @USUARIO_CREACION, 
                                             @IP_CREACION
                                            );";

            SqlParameter[] parametro = {
                    new SqlParameter("@ID_TIPO_CENTRO_TRABAJO", request.idTipoCentroTrabajo),
                    new SqlParameter("@ID_DRE", request.idDre),
                    new SqlParameter("@ID_UGEL", request.idUgel),
                    new SqlParameter("@ID_INSTITUCION_EDUCATIVA", request.idInstitucionEducativa),
                    new SqlParameter("@CODIGO_CENTRO_TRABAJO", request.codigoCentroTrabajo),
                    new SqlParameter("@ACTIVO", request.activo),
                    new SqlParameter("@FECHA_CREACION", request.fechaCreacion),
                    new SqlParameter("@USUARIO_CREACION", request.usuarioCreacion),
                    new SqlParameter("@IP_CREACION", request.ipCreacion),
            };

            try
            {

                var InsertId = SqlHelper.ExecuteNonQuery(this.txtConnectionString, CommandType.Text, sql, parametro);
                response = Convert.ToInt32(InsertId);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Task.FromResult(response);
        }

        public Task<int> ModificarCentroTrabajoReplica(CentroTrabajo request)
        {
            int response = 0;
            const string sql = @"UPDATE dbo.centro_trabajo
                                  SET 
                                      ID_INSTITUCION_EDUCATIVA = @ID_INSTITUCION_EDUCATIVA, 
                                      FECHA_MODIFICACION = @FECHA_MODIFICACION, 
                                      USUARIO_MODIFICACION = @USUARIO_MODIFICACION, 
                                      IP_MODIFICACION = @IP_MODIFICACION
                                WHERE ID_CENTRO_TRABAJO = @ID_CENTRO_TRABAJO; ";
            SqlParameter[] parametro = {
                new SqlParameter("@ID_CENTRO_TRABAJO", request.idCentroTrabajo),
                new SqlParameter("@ID_INSTITUCION_EDUCATIVA", request.idInstitucionEducativa),
                new SqlParameter("@FECHA_MODIFICACION", request.fechaModificacion),
                new SqlParameter("@USUARIO_MODIFICACION", request.usuarioModificacion),
                new SqlParameter("@IP_MODIFICACION", request.ipModificacion)
            };

            try
            {
                var result = SqlHelper.ExecuteNonQuery(this.txtConnectionString, CommandType.Text, sql, parametro);
                response = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(response);
        }

        public Task<int> DesactivarCentroTrabajoReplica(CentroTrabajo request)
        {
            int response = 0;
            const string sql = @"UPDATE dbo.centro_trabajo
                                  SET  
                                      ACTIVO = 0, 
                                      FECHA_MODIFICACION = @FECHA_MODIFICACION, 
                                      USUARIO_MODIFICACION = @USUARIO_MODIFICACION, 
                                      IP_MODIFICACION = @IP_MODIFICACION
                                WHERE ID_CENTRO_TRABAJO = @ID_CENTRO_TRABAJO;";
            SqlParameter[] parametro = {
                new SqlParameter("@ID_CENTRO_TRABAJO", request.idCentroTrabajo),
                new SqlParameter("@FECHA_MODIFICACION", request.fechaModificacion),
                new SqlParameter("@USUARIO_MODIFICACION", request.usuarioModificacion),
                new SqlParameter("@IP_MODIFICACION", request.ipModificacion)
            };


            try
            {

                var result = SqlHelper.ExecuteNonQuery(this.txtConnectionString, CommandType.Text, sql, parametro);
                response = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(response);
        }

        public async Task<CentroTrabajoResponse> GetCentroTrabajoByCodigo(string codigoCentroTrabajo, bool? activo)
        {
            // TODO: 

            const string sql = @"SELECT ct.ID_CENTRO_TRABAJO, 
                                       ct.ID_TIPO_CENTRO_TRABAJO, 
                                       ct.ID_OTRA_INSTANCIA, 
                                       ct.ID_DRE, 
                                       ct.ID_UGEL, 
                                       ct.CODIGO_CENTRO_TRABAJO, 
                                       ct.ID_INSTITUCION_EDUCATIVA
                                FROM dbo.centro_trabajo ct WITH(NOLOCK)
                                WHERE CODIGO_CENTRO_TRABAJO = @CODIGO_CENTRO_TRABAJO
                                      AND ((ACTIVO = @ACTIVO) OR @ACTIVO IS NULL);";

            SqlParameter[] parametro = {
                new SqlParameter("@CODIGO_CENTRO_TRABAJO", codigoCentroTrabajo),
                new SqlParameter("@ACTIVO", activo)
            };

            SqlDataReader dr = null;
            try
            {
                CentroTrabajoResponse model = null;
                using (SqlConnection cn = new SqlConnection(txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    if (dr.HasRows)
                    {
                        if (dr.ReadAsync().Result)
                        {
                            model = new CentroTrabajoResponse();
                            model.idCentroTrabajo = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_CENTRO_TRABAJO"));
                            model.idTipoCentroTrabajo = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_TIPO_CENTRO_TRABAJO"));
                            model.idDre = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_DRE"));
                            model.idUgel = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_UGEL"));
                            model.idInstitucionEducativa = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_INSTITUCION_EDUCATIVA"));
                            model.idOtraInstancia = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_OTRA_INSTANCIA"));
                            model.codigoCentroTrabajo = SqlHelper.GetNullableString(dr, dr.GetOrdinal("CODIGO_CENTRO_TRABAJO"));
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await SqlHelper.CloseDataReaderAsync(dr); ;
            }
        }
    }
}
