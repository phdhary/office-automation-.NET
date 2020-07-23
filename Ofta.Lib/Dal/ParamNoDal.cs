using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Dal
{
    public interface IParamNoDal
    {
        void Insert(IParamNoModel paramNo);
        void Update(IParamNoModel paramNo);
        void Delete(IParamNoKey key);
        IParamNoModel GetData(IParamNoKey key);
        IEnumerable<IParamNoModel> ListData();
    }
    public class ParamNoDal : IParamNoDal
    {

        public void Insert(IParamNoModel paramNo)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_ParamNo (
                        ParamID, ParamValue)
                VALUES (
                        @ParamID, @ParamValue) ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@ParamID", paramNo.ParamID, SqlDbType.VarChar);
                cmd.AddParam("@ParamValue", paramNo.ParamValue, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IParamNoModel paramNo)
        {
            var sql = @"
                UPDATE
                    OFTA_ParamNo 
                SET
                    ParamValue = @ParamValue
                WHERE
                    ParamID = @ParamID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@ParamID", paramNo.ParamID, SqlDbType.VarChar);
                cmd.AddParam("@ParamValue", paramNo.ParamValue, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IParamNoKey key)
        {
            var sql = @"
                DELETE 
                    OFTA_ParamNo
                WHERE
                    ParamID = @ParamID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@ParamID", key.ParamID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IParamNoModel GetData(IParamNoKey key)
        {
            ParamNoModel result = null;
            var sql = @"
                SELECT
                    ParamValue
                FROM
                    OFTA_ParamNo
                WHERE
                    ParamID = @ParamID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@ParamID", key.ParamID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new ParamNoModel()
                    {
                        ParamID = key.ParamID,
                        ParamValue = dr["ParamValue"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<IParamNoModel> ListData()
        {
            List<ParamNoModel> result = null;
            var sql = @"
                SELECT
                    ParamID, ParamValue
                FROM
                    OFTA_ParamNo ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<ParamNoModel>();
                    while (dr.Read())
                    {
                        var itemResult = new ParamNoModel()
                        {
                            ParamID = dr["ParamID"].ToString(),
                            ParamValue = dr["ParamValue"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }
            }
            return result;
        }
    }
}
