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
    public interface IJenisKontrakDal :
        IInsert<JenisKontrakModel>,
        IUpdate<JenisKontrakModel>,
        IDelete<IJenisKontrakKey>,
        IGetData<JenisKontrakModel, IJenisKontrakKey>,
        IListData<JenisKontrakModel>
    {

    }
    public class JenisKontrakDal : IJenisKontrakDal
    {
        public void Insert(JenisKontrakModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_JenisKontrak (
                        JenisKontrakID, JenisKontrakName)
                VALUES (
                        @JenisKontrakID, @JenisKontrakName) ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisKontrakID", entity.JenisKontrakID, SqlDbType.VarChar);
                cmd.AddParam("@JenisKontrakName", entity.JenisKontrakName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(JenisKontrakModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_JenisKontrak 
                SET
                    JenisKontrakName = @JenisKontrakName
                WHERE
                    JenisKontrakID = @JenisKontrakID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisKontrakID", entity.JenisKontrakID, SqlDbType.VarChar);
                cmd.AddParam("@JenisKontrakName", entity.JenisKontrakName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IJenisKontrakKey key)
        {
            var sql = @"
                DELETE
                    OFTA_JenisKontrak 
                WHERE
                    JenisKontrakID = @JenisKontrakID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisKontrakID", key.JenisKontrakID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public JenisKontrakModel GetData(IJenisKontrakKey key)
        {
            JenisKontrakModel result = null;
            var sql = @"
                SELECT
                    JenisKontrakID, JenisKontrakName
                FROM
                    OFTA_JenisKontrak
                WHERE
                    JenisKontrakID = @JenisKontrakID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisKontrakID", key.JenisKontrakID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new JenisKontrakModel
                    {
                        JenisKontrakID = dr["JenisKontrakID"].ToString(),
                        JenisKontrakName = dr["JenisKontrakName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<JenisKontrakModel> ListData()
        {
            List<JenisKontrakModel> result = null;
            var sql = @"
                SELECT
                    JenisKontrakID, JenisKontrakName
                FROM
                    OFTA_JenisKontrak ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<JenisKontrakModel>();
                    while (dr.Read())
                    {
                        var item = new JenisKontrakModel
                        {
                            JenisKontrakID = dr["JenisKontrakID"].ToString(),
                            JenisKontrakName = dr["JenisKontrakName"].ToString()
                        };
                        result.Add(item);
                    }                
                }
            }
            return result;
        }
    }
}
