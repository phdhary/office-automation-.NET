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
    public interface IJenisBiayaDal :
        IInsert<JenisBiayaModel>,
        IUpdate<JenisBiayaModel>,
        IDelete<IJenisBiayaKey>,
        IGetData<JenisBiayaModel, IJenisBiayaKey>,
        IListData<JenisBiayaModel>
    {
    }
    public class JenisBiayaDal : IJenisBiayaDal
    {
        public void Insert(JenisBiayaModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_JenisBiaya (
                        JenisBiayaID, JenisBiayaName )
                VALUES (
                        @JenisBiayaID, @JenisBiayaName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisBiayaID", entity.JenisBiayaID, SqlDbType.VarChar);
                cmd.AddParam("@JenisBiayaName", entity.JenisBiayaName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(IJenisBiayaKey key)
        {
            var sql = @"
                DELETE
                    OFTA_JenisBiaya
                WHERE
                    JenisBiayaID = @JenisBiayaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisBiayaID", key.JenisBiayaID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(JenisBiayaModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_JenisBiaya 
                SET
                    JenisBiayaName = @JenisBiayaName
                WHERE
                    JenisBiayaID = @JenisBiayaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisBiayaID", entity.JenisBiayaID, SqlDbType.VarChar);
                cmd.AddParam("@JenisBiayaName", entity.JenisBiayaName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public JenisBiayaModel GetData(IJenisBiayaKey key)
        {
            JenisBiayaModel result = null;
            var sql = @"
                SELECT
                    JenisBiayaName
                FROM
                    OFTA_JenisBiaya
                WHERE
                    JenisBiayaID = @JenisBiayaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisBiayaID", key.JenisBiayaID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new JenisBiayaModel
                    {
                        JenisBiayaID = key.JenisBiayaID,
                        JenisBiayaName = dr["JenisBiayaName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<JenisBiayaModel> ListData()
        {
            List<JenisBiayaModel> result = null;
            var sql = @"
                SELECT
                    JenisBiayaID, JenisBiayaName
                FROM
                    OFTA_JenisBiaya ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<JenisBiayaModel>();
                    while (dr.Read())
                    {
                        var itemResult = new JenisBiayaModel
                        {
                            JenisBiayaID = dr["JenisBiayaID"].ToString(),
                            JenisBiayaName = dr["JenisBiayaName"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }
            }
            return result;
        }

    }
}
