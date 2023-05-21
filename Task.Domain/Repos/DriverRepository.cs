using System.Data.SqlClient;
using System.Data;
using Task.Domain.Helpers;
using Task.Domain.Helpers.Traanslators;
using Task.Domain.Interfaces.Repos;
using Task.Domain.Models;
using Task.Domain.Dto;

namespace Task.Domain.Repos
{
    public class DriverRepository : IDriverRepository
    {
        public DBSetting _dbSettings { get; }

        public DriverRepository(DBSetting dbSettings)
        {
            _dbSettings = dbSettings;
        }
        public string CreateAsync(DriverDto entity)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@FName",entity.FirstName),
                new SqlParameter("@LName",entity.LastName),
                new SqlParameter("@Mobile",entity.PhoneNumber),
                new SqlParameter("@EmailId",entity.Email),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(_dbSettings.DbCon, "ADDNewDriver", param);
            return (string)outParam.Value;
        }

        public string DeleteAsync(int id)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(_dbSettings.DbCon, "DeleteDriver", param);
            return (string)outParam.Value;
        }

        public IEnumerable<DriverModel> GetAllAsync(int page, int size)
        {
            var outParam = new SqlParameter("@totalrow", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@page",page),
                new SqlParameter("@size",size),
                outParam
            };
            return SqlHelper.ExtecuteProcedureReturnData<List<DriverModel>>(_dbSettings.DbCon,
               "GetAllDrivers", r => r.TranslateAsDriverList(),param);
        }

        public DriverModel GetbyIdAsync(int id)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            return SqlHelper.ExtecuteProcedureReturnData<DriverModel>(_dbSettings.DbCon,
               "GetDriverbyId", r => r .TranslateAsDriver(), param);
        }
        public string UpdateAsync(DriverModel entity)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",entity.Id),
                new SqlParameter("@FName",entity.FirstName),
                new SqlParameter("@LName",entity.LastName),
                new SqlParameter("@Mobile",entity.PhoneNumber),
                new SqlParameter("@EmailId",entity.Email),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(_dbSettings.DbCon, "UpdateDriver", param);
            return (string)outParam.Value;
        }
    }
}
