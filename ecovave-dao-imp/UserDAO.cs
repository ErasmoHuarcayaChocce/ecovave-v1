using ecovave.dao.intf;
using ecovave.model;
using minedu.tecnologia.util.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ecovave.dao.imp
{
    public class UserDAO : DAOBase, IUserDAO
    {
        public UserDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }
        public async Task<int> CrearUsuario(User request, TransactionBase transaction)
        {
            int response = 0;
            string sql = @"INSERT INTO dbo.[User](
                                UserName, 
                                FirsName, 
                                LastName, 
                                DocumentTypeId, 
                                DocumentNumber, 
                                MobilphoneNumber, 
                                TelephoneNumber, 
                                IsActived, 
                                EsDeleted, 
                                CreatedUser, 
                                CreatedDate, 
                                CreateIp)
                                OUTPUT INSERTED.UserId
                            VALUES
                                (@UserName, 
                                 @FirsName, 
                                 @LastName, 
                                 @DocumentTypeId, 
                                 @DocumentNumber, 
                                 @MobilphoneNumber, 
                                 @TelephoneNumber, 
                                 @IsActived, 
                                 @EsDeleted, 
                                 @CreatedUser, 
                                 @CreatedDate, 
                                 @CreateIp
                                );";
            SqlParameter[] parameters =
             { 
              new SqlParameter("@UserName",request.UserName),
              new SqlParameter("@FirsName",request.FirsName),
              new SqlParameter("@LastName",request.LastName),
              new SqlParameter("@DocumentTypeId",request.DocumentTypeId),
              new SqlParameter("@DocumentNumber",request.DocumentNumber),
              new SqlParameter("@MobilphoneNumber",request.MobilphoneNumber),
              new SqlParameter("@TelephoneNumber",request.TelephoneNumber),
              new SqlParameter("@IsActived",request.IsActived),
              new SqlParameter("@EsDeleted",request.EsDeleted),
              new SqlParameter("@CreatedUser",request.CreatedUser),
              new SqlParameter("@CreatedDate",request.CreatedDate),
              new SqlParameter("@CreateIp",request.CreateIp)
            };
            try
            {
                var insertId = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parameters);
                response = Convert.ToInt32(insertId);
                request.UserId = response;
                return Convert.ToInt32(insertId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<int> EliminarUsuario(User request, TransactionBase transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> GetUsuarioCustomById(int idUsuarrio)
        {
            string sql = @"SELECT UserId, 
                                   UserName, 
                                   FirsName, 
                                   LastName, 
                                   DocumentTypeId, 
                                   DocumentNumber, 
                                   MobilphoneNumber, 
                                   TelephoneNumber, 
                                   IsActived, 
                                   EsDeleted
                            FROM dbo.[User]
                            WHERE UserId = @UserId;";


            SqlParameter[] parametro =  {
                new SqlParameter("@ID_POSTULACION", idUsuarrio),
            };

            SqlDataReader dr = null;
            try
            {
                UserResponse model = null;
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    if (dr.HasRows)
                    {
                        if (dr.ReadAsync().Result)
                        {
                            model = new UserResponse();
                            model.UserId = SqlHelper.GetInt32(dr, dr.GetOrdinal("UserId"));
                            model.UserName = SqlHelper.GetString(dr, dr.GetOrdinal("UserName"));
                            model.FirsName = SqlHelper.GetString(dr, dr.GetOrdinal("FirsName"));
                            model.LastName = SqlHelper.GetString(dr, dr.GetOrdinal("LastName"));
                            model.DocumentTypeId = SqlHelper.GetInt32(dr, dr.GetOrdinal("DocumentTypeId"));
                            model.DocumentNumber = SqlHelper.GetString(dr, dr.GetOrdinal("DocumentNumber"));
                            model.MobilphoneNumber = SqlHelper.GetString(dr, dr.GetOrdinal("MobilphoneNumber"));
                            model.TelephoneNumber = SqlHelper.GetString(dr, dr.GetOrdinal("TelephoneNumber"));
                            model.IsActived = SqlHelper.GetBoolean(dr, dr.GetOrdinal("IsActived"));
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

        public async Task<int> ModificarUsuario(User request, TransactionBase transaction)
        {
            string sql = @"UPDATE dbo.[User]
                          SET 
                              UserName = @UserName, 
                              FirsName = @FirsName, 
                              LastName = @LastName, 
                              DocumentTypeId = @DocumentTypeId, 
                              DocumentNumber = @DocumentNumber, 
                              MobilphoneNumber = @MobilphoneNumber, 
                              TelephoneNumber = @TelephoneNumber, 
                              IsActived = @IsActived, 
                              ModifiedUser = @ModifiedUser, 
                              ModifiedDate = @ModifiedDate, 
                              ModifiedIp = @ModifiedIp
                        WHERE UserId = @UserId;";
            SqlParameter[] parameters =
            {
              new SqlParameter("@UserId", request.UserId),
              new SqlParameter("@UserName",request.UserName),
              new SqlParameter("@FirsName",request.FirsName),
              new SqlParameter("@LastName",request.LastName),
              new SqlParameter("@DocumentTypeId",request.DocumentTypeId),
              new SqlParameter("@DocumentNumber",request.DocumentNumber),
              new SqlParameter("@MobilphoneNumber",request.MobilphoneNumber),
              new SqlParameter("@TelephoneNumber",request.TelephoneNumber),
              new SqlParameter("@IsActived",request.IsActived),
              new SqlParameter("@ModifiedUser",request.ModifiedUser),
              new SqlParameter("@ModifiedDate",request.ModifiedDate),
              new SqlParameter("@ModifiedIp",request.ModifiedIp)
            };
            try
            {
                var insertId = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parameters);
                return Convert.ToInt32(insertId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
