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
    public interface IJenisCutiDal :
        IInsert<JenisCutiModel>,
        IUpdate<JenisCutiModel>,
        IDelete<IJenisCutiKey>,
        IGetData<JenisCutiModel, IJenisCutiKey>,
        IListData<JenisCutiModel>
    {
    }

    public class JenisCutiDal : IJenisCutiDal
    {
        public void Insert(JenisCutiModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_JenisCuti (
                        JenisCutiID, JenisCutiName )
                VALUES (
                        @JenisCutiID, @JenisCutiName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisCutiID", entity.JenisCutiID, SqlDbType.VarChar);
                cmd.AddParam("@JenisCutiName", entity.JenisCutiName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(JenisCutiModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_JenisCuti
                SET
                    JenisCutiName = @JenisCutiName
                WHERE
                    JenisCutiID = @JenisCutiID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisCutiID", entity.JenisCutiID, SqlDbType.VarChar);
                cmd.AddParam("@JenisCutiName", entity.JenisCutiName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IJenisCutiKey key)
        {
            var sql = @"
                DELETE
                    OFTA_JenisCuti
                WHERE
                    JenisCutiID = @JenisCutiID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisCutiID", key.JenisCutiID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public JenisCutiModel GetData(IJenisCutiKey key)
        {
            JenisCutiModel result = null;
            var sql = @"
                SELECT
                    JenisCutiName
                FROM
                    OFTA_JenisCuti
                WHERE
                    JenisCutiID = @JenisCutiID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisCutiID", key.JenisCutiID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new JenisCutiModel
                    {
                        JenisCutiID = key.JenisCutiID,
                        JenisCutiName = dr["JenisCutiName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<JenisCutiModel> ListData()
        {
            List<JenisCutiModel> result = null;
            var sql = @"
                SELECT
                    JenisCutiID,JenisCutiName
                FROM
                    OFTA_JenisCuti ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<JenisCutiModel>();
                    while (dr.Read())
                    {
                        var itemResult = new JenisCutiModel
                        {
                            JenisCutiID = dr["JenisCutiID"].ToString(),
                            JenisCutiName = dr["JenisCutiName"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }
            }
            return result;
        }
    }
}