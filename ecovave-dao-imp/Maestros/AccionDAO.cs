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
    public class AccionDAO : DAOBase, IAccionDAO
    {
        public AccionDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        private Accion CargarAccion(SqlDataReader dr)
        {
            Accion model = new Accion();
            int index = 0;
            model.idAccion = SqlHelper.GetInt32(dr, index);
            index++;
            model.codigoAccion = SqlHelper.GetInt32(dr, index);
            index++;
            model.descripcionAccion = SqlHelper.GetNullableString(dr, index);
            index++;
            model.activo = SqlHelper.GetBoolean(dr, index);
            return model;
        }

        public async Task<IEnumerable<Accion>> GetAccionesByGrupo(int? idGrupoAccion, int? idRegimenLaboral, string codigoRolPassport, bool? activo)
        {
            const string sql = @"SELECT DISTINCT rga.ID_ACCION, 
		                                a.CODIGO_ACCION,
                                        a.DESCRIPCION_ACCION,
		                                a.ACTIVO
                                FROM dbo.regimen_grupo_accion rga WITH(NOLOCK)
                                     INNER JOIN dbo.accion a WITH(NOLOCK) ON rga.ID_ACCION = a.ID_ACCION
                                     INNER JOIN dbo.rol_passport rp ON rga.ID_ROL_PASSPORT = rga.ID_ROL_PASSPORT
                                WHERE ((rga.ID_GRUPO_ACCION = @IdGrupoAccion) OR @IdGrupoAccion IS NULL)
                                      AND ((rga.ID_REGIMEN_LABORAL = @idRegimenLaboral) OR @idRegimenLaboral IS NULL)
                                      AND ((rp.CODIGO_ROL = @CODIGO_ROL) OR @CODIGO_ROL IS NULL)
                                      AND ((rga.ACTIVO = @activo) OR @activo IS NULL)
	                              ORDER BY a.DESCRIPCION_ACCION ASC;";

            SqlParameter[] parametro = {
                new SqlParameter("@idGrupoAccion", idGrupoAccion),
                new SqlParameter("@idRegimenLaboral", idRegimenLaboral),
                new SqlParameter("@CODIGO_ROL", codigoRolPassport),
                new SqlParameter("@activo", activo),
            };


            SqlDataReader dr = null;
            try
            {
                List<Accion> response = new List<Accion>();
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    if (dr.HasRows)
                    {
                        while (dr.ReadAsync().Result)
                        {
                            Accion model = this.CargarAccion(dr);
                            response.Add(model);
                        }
                    }
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
            }
        }

        public async Task<Accion> GetAccionById(int idAccion)
        {
            const string sql = @"SELECT ID_ACCION, 
                                   CODIGO_ACCION, 
                                   DESCRIPCION_ACCION, 
                                   ACTIVO
                            FROM dbo.accion
                            WHERE ID_ACCION = @ID_ACCION;";

            SqlParameter[] parametro = {
                new SqlParameter("@ID_ACCION", idAccion),
            };
            SqlDataReader dr = null;
            try
            {
                Accion model = null;
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    if (dr.HasRows)
                    {
                        if (dr.ReadAsync().Result)
                        {
                            model = new Accion();
                            model.idAccion = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_ACCION"));
                            model.codigoAccion = SqlHelper.GetInt32(dr, dr.GetOrdinal("CODIGO_ACCION"));
                            model.descripcionAccion = SqlHelper.GetNullableString(dr, dr.GetOrdinal("DESCRIPCION_ACCION"));
                            model.activo = SqlHelper.GetBoolean(dr, dr.GetOrdinal("ACTIVO"));
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
                SqlHelper.CloseDataReader(dr);
            }
        }

    }
}

