using ecovave.dao.intf;
using ecovave.model;
using minedu.tecnologia.util.lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ecovave.dao.imp
{
    public class DeliveryDAO : DAOBase, IDeliveryDAO
    {
        public DeliveryDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<int> CrearDelivery(Delivery request, TransactionBase transaction)
        {
            int response = 0;
            string sql = @"INSERT INTO dbo.Delivery
                                        (DeliveryId,
                                         UserId, 
                                         ScheduleId, 
                                         QuantityKg, 
                                         DeliveryDate, 
                                         Description, 
                                         IsDeleted, 
                                         CreatedUser, 
                                         CreatedDate, 
                                         CreatedIp
                                        )                      
                                        VALUES
                                        (@DeliveryId,
                                         @UserId, 
                                         @ScheduleId, 
                                         @QuantityKg, 
                                         @DeliveryDate, 
                                         @Description, 
                                         @IsDeleted, 
                                         @CreatedUser, 
                                         @CreatedDate, 
                                         @CreatedIp
                                        );";

            SqlParameter[] parameters =
            {
              new SqlParameter("@DeliveryId",request.DeliveryId),
              new SqlParameter("@UserId",request.UserId),
              new SqlParameter("@ScheduleId",request.ScheduleId),
              new SqlParameter("@QuantityKg",request.QuantityKg),
              new SqlParameter("@DeliveryDate",request.DeliveryDate),
              new SqlParameter("@Description",request.Description),
              new SqlParameter("@IsDeleted",request.IsDeleted),
              new SqlParameter("@CreatedUser",request.CreatedUser),
              new SqlParameter("@CreatedDate",request.CreatedDate),
              new SqlParameter("@CreatedIp",request.CreateIp)
            };
            try
            {
                var insertId = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parameters);
                response = 1;
                request.DeliveryId = response;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> EliminarDelivery(Delivery request, TransactionBase transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryDto> GetDeliveryById(int idDelivery)
        {
            string sql = @"SELECT DeliveryId, 
                                   UserId, 
                                   ScheduleId, 
                                   QuantityKg, 
                                   DeliveryDate, 
                                   Description, 
                                   IsDeleted
                            FROM dbo.Delivery
                            WHERE DeliveryId = @DeliveryId;";

            SqlParameter[] parametro =  {
                new SqlParameter("@DeliveryId", idDelivery),
            };

            SqlDataReader dr = null;
            try
            {
                DeliveryDto model = null;
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    if (dr.HasRows)
                    {
                        if (dr.ReadAsync().Result)
                        {
                            model = new DeliveryDto();
                            model.DeliveryId = SqlHelper.GetInt32(dr, dr.GetOrdinal("DeliveryId"));
                            model.UserId = SqlHelper.GetInt32(dr, dr.GetOrdinal("UserId"));
                            model.ScheduleId = SqlHelper.GetInt32(dr, dr.GetOrdinal("ScheduleId"));
                            model.QuantityKg = SqlHelper.GetDecimal(dr, dr.GetOrdinal("QuantityKg"));
                            model.DeliveryDate = SqlHelper.GetDateTime(dr, dr.GetOrdinal("DeliveryDate"));
                            model.Description = SqlHelper.GetString(dr, dr.GetOrdinal("Description"));
                            model.IsDeleted = SqlHelper.GetBoolean(dr, dr.GetOrdinal("IsDeleted"));
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

        public async Task<int> ModificarDelivery(Delivery request, TransactionBase transaction)
        {
            int response = 0;
            string sql = @"UPDATE dbo.Delivery
                              SET 
                                  UserId = @UserId, 
                                  ScheduleId = @ScheduleId, 
                                  QuantityKg = @QuantityKg, 
                                  DeliveryDate = @DeliveryDate, 
                                  Description = @Description, 
                                  ModifiedUser = @ModifiedUser, 
                                  ModifiedDate = @ModifiedDate, 
                                  ModifiedIp = @ModifiedIp
                            WHERE DeliveryId = @DeliveryId;";

            SqlParameter[] parameters =
            {
              new SqlParameter("@DeliveryId",request.DeliveryId),
              new SqlParameter("@UserId",request.UserId),
              new SqlParameter("@ScheduleId",request.ScheduleId),
              new SqlParameter("@QuantityKg",request.QuantityKg),
              new SqlParameter("@DeliveryDate",request.DeliveryDate),
              new SqlParameter("@Description",request.Description),
              new SqlParameter("@ModifiedUser",request.CreatedUser),
              new SqlParameter("@ModifiedDate",request.CreatedDate),
              new SqlParameter("@ModifiedIp",request.CreateIp)
            };
            try
            {
                var insertId = await SqlHelper.ExecuteScalarAsync(transaction.connection, transaction.transaction, CommandType.Text, sql, parameters);
                response = Convert.ToInt32(insertId);
                request.DeliveryId = response;
                return Convert.ToInt32(insertId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
