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
    public class ScheduleDAO : DAOBase, IScheduleDAO
    {
        public ScheduleDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        private ScheduleDto CargarScheduleDto(SqlDataReader dr)
        {
            ScheduleDto model = new ScheduleDto();
            model.ScheduleId = SqlHelper.GetInt32(dr, dr.GetOrdinal("ScheduleId"));
            model.UserId = SqlHelper.GetInt32(dr, dr.GetOrdinal("UserId"));
            model.RecyclingTypeId = SqlHelper.GetInt32(dr, dr.GetOrdinal("RecyclingTypeId"));
            model.QuantityKg = SqlHelper.GetDecimal(dr, dr.GetOrdinal("QuantityKg"));
            model.ScheduleDate = SqlHelper.GetDateTime(dr, dr.GetOrdinal("ScheduleDate"));
            model.StatusId = SqlHelper.GetInt32(dr, dr.GetOrdinal("StatusId"));
            return model;

        }
        public Task<int> CrearSchedule(Schedule request)
        {
            int response = 0;
            const string sql = @"INSERT INTO dbo.Schedule
                                            (UserId, 
                                             RecyclingTypeId, 
                                             QuantityKg, 
                                             ScheduleDate, 
                                             StatusId, 
                                             EsDeleted, 
                                             CreatedUser, 
                                             CreatedDate, 
                                             CreatedIp
                                            )
                                            VALUES
                                            (@UserId, 
                                             @RecyclingTypeId, 
                                             @QuantityKg, 
                                             @ScheduleDate, 
                                             @StatusId, 
                                             @EsDeleted, 
                                             @CreatedUser, 
                                             @CreatedDate, 
                                             @CreatedIp
                                            ); ";

            SqlParameter[] parametro = {
                    new SqlParameter("@UserId", request.UserId),
                    new SqlParameter("@RecyclingTypeId", request.RecyclingTypeId),
                    new SqlParameter("@QuantityKg", request.QuantityKg),
                    new SqlParameter("@ScheduleDate", request.ScheduleDate),
                    new SqlParameter("@StatusId", request.StatusId),
                    new SqlParameter("@EsDeleted", false),
                    new SqlParameter("@CreatedUser", request.CreatedUser),
                    new SqlParameter("@CreatedDate", request.CreatedDate),
                    new SqlParameter("@CreatedIp", request.CreatedIp)
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

        public Task<int> DesactivarSchedule(Schedule request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScheduleDto>> GetScheduleById(int scheduleId)
        {
            const string sql = @"SELECT ScheduleId, 
                                           UserId, 
                                           RecyclingTypeId, 
                                           QuantityKg, 
                                           ScheduleDate, 
                                           StatusId, 
                                           EsDeleted, 
                                           CreatedUser, 
                                           CreatedDate, 
                                           CreatedIp, 
                                           ModifiedUser, 
                                           ModifiedDate, 
                                           ModifiedIp
                                    FROM dbo.Schedule
                                    WHERE ScheduleId = @ScheduleId; ";

            SqlParameter[] parametro = { new SqlParameter("@ID_POSTULACION", scheduleId) };
            SqlDataReader dr = null;
            try
            {
                List<ScheduleDto> response = new List<ScheduleDto>();
                using (SqlConnection cn = new SqlConnection(this.txtConnectionString))
                {
                    await cn.OpenAsync();
                    dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                    {
                        while (dr.ReadAsync().Result)
                        {
                            ScheduleDto model = this.CargarScheduleDto(dr);
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

        public Task<int> ModificarSchedule(Schedule request)
        {
            int response = 0;
            const string sql = @"UPDATE dbo.Schedule
                                              SET 
                                                  UserId = @UserId, 
                                                  RecyclingTypeId = @RecyclingTypeId, 
                                                  QuantityKg = @QuantityKg, 
                                                  ScheduleDate = @ScheduleDate, 
                                                  StatusId = @StatusId, 
                                                  EsDeleted = @EsDeleted, 
                                                  ModifiedUser = @ModifiedUser, 
                                                  ModifiedDate = @ModifiedDate, 
                                                  ModifiedIp = @ModifiedIp
                                            WHERE ScheduleId = @ScheduleId; ";
            SqlParameter[] parametro = {
                    new SqlParameter("@ScheduleId", request.ScheduleId),
                    new SqlParameter("@UserId", request.UserId),
                    new SqlParameter("@RecyclingTypeId", request.RecyclingTypeId),
                    new SqlParameter("@QuantityKg", request.QuantityKg),
                    new SqlParameter("@ScheduleDate", request.ScheduleDate),
                    new SqlParameter("@StatusId", request.StatusId),
                    new SqlParameter("@EsDeleted", false),
                    new SqlParameter("@ModifiedUser", request.ModifiedUser),
                    new SqlParameter("@ModifiedDate", request.ModifiedDate),
                    new SqlParameter("@ModifiedIp", request.ModifiedIp)
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
    }
}
