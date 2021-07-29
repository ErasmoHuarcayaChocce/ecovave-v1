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
    public class DocumentoSustentoDAO : DAOBase, IDocumentoSustentoDAO
    {

        public DocumentoSustentoDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public DocumentoSustentoDAO()
        {
        }

        public async Task<int> CrearDocumentoSustento(DocumentoSustento request, TransactionBase transaction)
        {
            const string sql = @"INSERT INTO dbo.documento_sustento
                                (ID_DOCUMENTO_SUSTENTO, 
                                 ID_LICENCIA, 
                                 ID_TIPO_DOCUMENTO_SUSTENTO, 
                                 ID_TIPO_FORMATO_SUSTENTO, 
                                 ID_ORIGEN_REGISTRO,
                                 NUMERO_DOCUMENTO_SUSTENTO, 
                                 ENTIDAD_EMISORA, 
                                 FECHA_EMISION, 
                                 NUMERO_FOLIOS, 
                                 SUMILLA, 
                                 CODIGO_DOCUMENTO_SUSTENTO, 
                                 FECHA_REGISTRO, 
                                 ELIMINADO, 
                                 VISTO_PROYECTO,
                                 FECHA_CREACION, 
                                 USUARIO_CREACION, 
                                 IP_CREACION
                                )
                                OUTPUT INSERTED.ID_DOCUMENTO_SUSTENTO
                                VALUES
                                (NEXT VALUE FOR dbo.seq_documento_sustento, 
                                 @ID_LICENCIA, 
                                 @ID_TIPO_DOCUMENTO_SUSTENTO, 
                                 @ID_TIPO_FORMATO_SUSTENTO, 
                                 @ID_ORIGEN_REGISTRO,
                                 @NUMERO_DOCUMENTO_SUSTENTO, 
                                 @ENTIDAD_EMISORA, 
                                 @FECHA_EMISION, 
                                 @NUMERO_FOLIOS, 
                                 @SUMILLA, 
                                 @CODIGO_DOCUMENTO_SUSTENTO, 
                                 @FECHA_REGISTRO, 
                                 @ELIMINADO, 
                                 @VISTO_PROYECTO,
                                 @FECHA_CREACION, 
                                 @USUARIO_CREACION, 
                                 @IP_CREACION
                                );";
            SqlParameter[] parametro = {
                new SqlParameter("@ID_LICENCIA", request.idLicencia),
                new SqlParameter("@ID_TIPO_DOCUMENTO_SUSTENTO", request.idTipoDocumentoSustento),
                new SqlParameter("@ID_TIPO_FORMATO_SUSTENTO", request.idTipoFormatoSustento),
                new SqlParameter("@ID_ORIGEN_REGISTRO", request.idOrigenRegistro),
                new SqlParameter("@NUMERO_DOCUMENTO_SUSTENTO", request.numeroDocumentoSustento),
                new SqlParameter("@ENTIDAD_EMISORA", request.entidadEmisora),
                new SqlParameter("@FECHA_EMISION", request.fechaEmision),
                new SqlParameter("@NUMERO_FOLIOS", request.numeroFolios),
                new SqlParameter("@SUMILLA", request.sumilla),
                new SqlParameter("@CODIGO_DOCUMENTO_SUSTENTO", request.codigoDocumentoSustento),
                new SqlParameter("@FECHA_REGISTRO", request.fechaRegistro),
                new SqlParameter("@ELIMINADO", request.eliminado),
                new SqlParameter("@VISTO_PROYECTO", request.vistoProyecto),
                new SqlParameter("@FECHA_CREACION", request.fechaCreacion),
                new SqlParameter("@USUARIO_CREACION", request.usuarioCreacion),
                new SqlParameter("@IP_CREACION", request.ipCreacion),
            };

            try
            {
                var insertId = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parametro);
                return Convert.ToInt32(insertId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private DocumentoSustentoDto CargarDocumentoSustentoDto(SqlDataReader dr)
        {
            DocumentoSustentoDto model = new DocumentoSustentoDto();
            model.idDocumentoSustento = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_DOCUMENTO_SUSTENTO"));
            model.idLicencia = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_LICENCIA"));
            model.idTipoDocumentoSustento = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_TIPO_DOCUMENTO_SUSTENTO"));
            model.idTipoFormatoSustento = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_TIPO_FORMATO_SUSTENTO"));
            model.idOrigenRegistro = SqlHelper.GetInt32(dr, dr.GetOrdinal("ID_ORIGEN_REGISTRO"));
            model.numeroDocumentoSustento = SqlHelper.GetNullableString(dr, dr.GetOrdinal("NUMERO_DOCUMENTO_SUSTENTO"));
            model.entidadEmisora = SqlHelper.GetNullableString(dr, dr.GetOrdinal("ENTIDAD_EMISORA"));
            model.fechaEmision = SqlHelper.GetNullableString(dr, dr.GetOrdinal("FECHA_EMISION"));
            model.numeroFolios = SqlHelper.GetInt32(dr, dr.GetOrdinal("NUMERO_FOLIOS"));
            model.sumilla = SqlHelper.GetNullableString(dr, dr.GetOrdinal("SUMILLA"));
            model.codigoDocumentoSustento = SqlHelper.GetNullableString(dr, dr.GetOrdinal("CODIGO_DOCUMENTO_SUSTENTO"));
            model.fechaRegistro = SqlHelper.GetNullableString(dr, dr.GetOrdinal("FECHA_REGISTRO"));
            model.descripcionTipoSustento = SqlHelper.GetNullableString(dr, dr.GetOrdinal("DESCRIPCION_TIPO_SUSTENTO"));
            model.descripcionTipoFormato = SqlHelper.GetNullableString(dr, dr.GetOrdinal("DESCRIPCION_TIPO_FORMATO"));
            model.codigoOrigenRegistro = SqlHelper.GetInt32(dr, dr.GetOrdinal("CODIGO_ORIGEN_REGISTRO"));
            model.descripcionVistoProyecto = SqlHelper.GetNullableString(dr, dr.GetOrdinal("DESCRIPCION_VISTO_PROYECTO"));
            model.vistoProyecto = SqlHelper.GetBoolean(dr, dr.GetOrdinal("VISTO_PROYECTO"));
            return model;
        }
        public async Task<IEnumerable<DocumentoSustentoDto>> GetDocumentoSustentoCustomByIdLicencia(int idLicencia)
        {
            const string sql = @"SELECT ds.ID_DOCUMENTO_SUSTENTO, 
                                        ds.ID_LICENCIA, 
                                        ds.ID_TIPO_DOCUMENTO_SUSTENTO, 
                                        ds.ID_TIPO_FORMATO_SUSTENTO, 
                                        ds.ID_ORIGEN_REGISTRO, 
                                        ds.NUMERO_DOCUMENTO_SUSTENTO, 
                                        ds.ENTIDAD_EMISORA, 
                                        CONVERT(VARCHAR(103), ds.FECHA_EMISION, 103) AS FECHA_EMISION, 
                                        ds.NUMERO_FOLIOS, 
                                        ds.SUMILLA, 
                                        CONVERT(VARCHAR(50), ds.CODIGO_DOCUMENTO_SUSTENTO) AS CODIGO_DOCUMENTO_SUSTENTO, 
                                        CONVERT(VARCHAR(103), ds.FECHA_REGISTRO, 103) AS FECHA_REGISTRO, 
                                        tds.DESCRIPCION_CATALOGO_ITEM AS DESCRIPCION_TIPO_SUSTENTO, 
                                        tfs.DESCRIPCION_CATALOGO_ITEM AS DESCRIPCION_TIPO_FORMATO, 
                                        ori.CODIGO_CATALOGO_ITEM AS CODIGO_ORIGEN_REGISTRO,
                                        CASE
                                            WHEN ds.VISTO_PROYECTO = 1
                                            THEN 'SI'
                                            ELSE 'NO'
                                        END AS DESCRIPCION_VISTO_PROYECTO,
                                        ds.VISTO_PROYECTO
                                FROM dbo.documento_sustento ds WITH(NOLOCK)
                                        INNER JOIN dbo.catalogo_item tds ON ds.ID_TIPO_DOCUMENTO_SUSTENTO = tds.ID_CATALOGO_ITEM
                                        INNER JOIN dbo.catalogo_item tfs ON ds.ID_TIPO_FORMATO_SUSTENTO = tfs.ID_CATALOGO_ITEM
                                        INNER JOIN dbo.catalogo_item ori ON ori.ID_CATALOGO_ITEM = ds.ID_ORIGEN_REGISTRO
                              WHERE ds.ID_LICENCIA = @ID_LICENCIA
                                    AND ds.ELIMINADO = 0
                              ORDER BY ds.ID_DOCUMENTO_SUSTENTO ASC;";

            SqlParameter[] parametro = { new SqlParameter("@ID_LICENCIA", idLicencia) };
            SqlDataReader dr = null;
            try
            {
                List<DocumentoSustentoDto> response = new List<DocumentoSustentoDto>();
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    {
                        while (dr.ReadAsync().Result)
                        {
                            DocumentoSustentoDto model = this.CargarDocumentoSustentoDto(dr);
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

        public async Task<int> ModificarDocumentoSustento(DocumentoSustento request, TransactionBase transaction)
        {
            int response = 0;
            const string sql = @"UPDATE dbo.documento_sustento SET
                                  ID_TIPO_DOCUMENTO_SUSTENTO = @ID_TIPO_DOCUMENTO_SUSTENTO
                                  ,ID_TIPO_FORMATO_SUSTENTO = @ID_TIPO_FORMATO_SUSTENTO
                                  ,NUMERO_DOCUMENTO_SUSTENTO = @NUMERO_DOCUMENTO_SUSTENTO
                                  ,ENTIDAD_EMISORA = @ENTIDAD_EMISORA
                                  ,FECHA_EMISION = @FECHA_EMISION
                                  ,NUMERO_FOLIOS = @NUMERO_FOLIOS
                                  ,SUMILLA = @SUMILLA
                                  ,CODIGO_DOCUMENTO_SUSTENTO = @CODIGO_DOCUMENTO_SUSTENTO
                                  ,FECHA_MODIFICACION = @FECHA_MODIFICACION
                                  ,USUARIO_MODIFICACION = @USUARIO_MODIFICACION
                                  ,IP_MODIFICACION = @IP_MODIFICACION
                             WHERE ID_DOCUMENTO_SUSTENTO = @ID_DOCUMENTO_SUSTENTO;";
            SqlParameter[] parametro = {
                    new SqlParameter("@ID_DOCUMENTO_SUSTENTO", request.idDocumentoSustento),
                new SqlParameter("@ID_TIPO_DOCUMENTO_SUSTENTO", request.idTipoDocumentoSustento),
                new SqlParameter("@ID_TIPO_FORMATO_SUSTENTO", request.idTipoFormatoSustento),
                new SqlParameter("@NUMERO_DOCUMENTO_SUSTENTO", request.numeroDocumentoSustento),
                new SqlParameter("@ENTIDAD_EMISORA", request.entidadEmisora),
                new SqlParameter("@FECHA_EMISION", request.fechaEmision),
                new SqlParameter("@NUMERO_FOLIOS", request.numeroFolios),
                new SqlParameter("@SUMILLA", request.sumilla),
                new SqlParameter("@CODIGO_DOCUMENTO_SUSTENTO", request.codigoDocumentoSustento),
                new SqlParameter("@FECHA_MODIFICACION", request.fechaModificacion),
                new SqlParameter("@USUARIO_MODIFICACION", request.usuarioModificacion),
                new SqlParameter("@IP_MODIFICACION", request.ipModificacion)
            };

            try
            {

                var result = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parametro);
                response = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public async Task<int> EliminarDocumentoSustento(DocumentoSustento request, TransactionBase transaction)
        {
            int response = 0;
            const string sql = @"UPDATE dbo.documento_sustento
                                  SET 
                                      ELIMINADO = 1, 
                                      FECHA_MODIFICACION = @FECHA_MODIFICACION, 
                                      USUARIO_MODIFICACION = @USUARIO_MODIFICACION, 
                                      IP_MODIFICACION = @IP_MODIFICACION
                                WHERE ID_DOCUMENTO_SUSTENTO = @ID_DOCUMENTO_SUSTENTO";
            SqlParameter[] parametro = {
                new SqlParameter("@ID_DOCUMENTO_SUSTENTO", request.idDocumentoSustento),
                new SqlParameter("@FECHA_MODIFICACION", request.fechaModificacion),
                new SqlParameter("@USUARIO_MODIFICACION", request.usuarioModificacion),
                new SqlParameter("@IP_MODIFICACION", request.ipModificacion)
            };

            try
            {

                var result = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parametro);
                response = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }
    }
}
