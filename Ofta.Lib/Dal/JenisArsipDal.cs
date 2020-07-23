using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ofta.Lib.Dal
{
    public interface IJenisArsipDal :
        IInsert<JenisArsipModel>,
        IUpdate<JenisArsipModel>,
        IDelete<IJenisArsipKey>,
        IGetData<JenisArsipModel, IJenisArsipKey>,
        IListData<JenisArsipModel>
    {
    }
    public class JenisArsipDal : IJenisArsipDal
    {
        public void Insert(JenisArsipModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_JenisArsip(
                            JenisArsipID, JenisArsipName)
                VALUES(
                        @JenisArsipID, @JenisArsipName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisArsipID", entity.JenisArsipID, SqlDbType.VarChar);
                cmd.AddParam("@JenisArsipName", entity.JenisArsipName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(JenisArsipModel entity)
        {
            var sql = @"
                UPDATE 
                    OFTA_JenisArsip
                SET
                    JenisArsipName = @JenisArsipName 
                WHERE
                    JenisArsipID = @JenisArsipID";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisArsipID", entity.JenisArsipID, SqlDbType.VarChar);
                cmd.AddParam("@JenisArsipName", entity.JenisArsipName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IJenisArsipKey key)
        {
            var sql = @"
                DELETE 
                    OFTA_JenisArsip
                WHERE
                    JenisArsipID = @JenisArsipID";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@JenisArsipID", key.JenisArsipID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public JenisArsipModel GetData(IJenisArsipKey key)
        {
            JenisArsipModel result = null;

            var sql = @"
                SELECT 
                    JenisArsipName
                FROM 
                    OFTA_JenisArsip
                WHERE
                    JenisArsipID = @JenisArsipID";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand (sql ,conn))
            {
                cmd.AddParam("@JenisArsipID", key.JenisArsipID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new JenisArsipModel
                    {
                        JenisArsipID = key.JenisArsipID,
                        JenisArsipName = dr["JenisArsipName"].ToString()
                    };

                }
            
            }
                return result;
        }

        public IEnumerable<JenisArsipModel> ListData()
        {
            List<JenisArsipModel> result = null;
            var sql = @"
                SELECT
                    JenisArsipID,JenisArsipName
                FROM
                    OFTA_JenisArsip";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using(var cmd = new SqlCommand(sql , conn)) 
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<JenisArsipModel>();
                    while (dr.Read())
                    {
                        var itemResult = new JenisArsipModel
                        {
                            JenisArsipID = dr["JenisArsipID"].ToString(),
                            JenisArsipName = dr["JenisArsipName"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }

            }
           return result;
        }
    }
}
