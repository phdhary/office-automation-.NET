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
    public interface IJenisSuratDinasDal :
        IInsert<JenisSuratDinasModel>,
        IUpdate<JenisSuratDinasModel>,
        IDelete<IJenisSuratDinasKey>,
        IGetData<JenisSuratDinasModel, IJenisSuratDinasKey>,
        IListData<JenisSuratDinasModel>
    {
    }

    public class JenisSuratDinasDal : IJenisSuratDinasDal
    {
        public void Insert(JenisSuratDinasModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_JenisSuratDinas (
                        JenisSuratDinasID, JenisSuratDinasName)
                VALUES (
                        @JenisSuratDinasID, @JenisSuratDinasName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisSuratDinasID", entity.JenisSuratDinasID, SqlDbType.VarChar);
                cmd.AddParam("@JenisSuratDinasName", entity.JenisSuratDinasName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(JenisSuratDinasModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_JenisSuratDinas 
                SET
                    JenisSuratDinasName = @JenisSuratDinasName
                WHERE
                    JenisSuratDinasID = @JenisSuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisSuratDinasID", entity.JenisSuratDinasID, SqlDbType.VarChar);
                cmd.AddParam("@JenisSuratDinasName", entity.JenisSuratDinasName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IJenisSuratDinasKey key)
        {
            var sql = @"
                DELETE
                    OFTA_JenisSuratDinas 
                WHERE
                    JenisSuratDinasID = @JenisSuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisSuratDinasID", key.JenisSuratDinasID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public JenisSuratDinasModel GetData(IJenisSuratDinasKey key)
        {
            JenisSuratDinasModel result = null;
            var sql = @"
                SELECT
                    JenisSuratDinasID, JenisSuratDinasName
                FROM
                    OFTA_JenisSuratDinas
                WHERE
                    JenisSuratDinasID = @JenisSuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisSuratDinasID", key.JenisSuratDinasID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new JenisSuratDinasModel
                    {
                        JenisSuratDinasID = dr["JenisSuratDinasID"].ToString(),
                        JenisSuratDinasName = dr["JenisSuratDinasName"].ToString(),
                    };
                }
            }
            return result;
        }

        public IEnumerable<JenisSuratDinasModel> ListData()
        {
            List<JenisSuratDinasModel> result = null;
            var sql = @"
                SELECT
                    JenisSuratDinasID, JenisSuratDinasName
                FROM
                    OFTA_JenisSuratDinas ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<JenisSuratDinasModel>();
                    while (dr.Read())
                    {
                        var item = new JenisSuratDinasModel
                        {
                            JenisSuratDinasID = dr["JenisSuratDinasID"].ToString(),
                            JenisSuratDinasName = dr["JenisSuratDinasName"].ToString(),
                        };
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }
}