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
    public interface IRSDal :
        IInsert<RSModel>,
        IUpdate<RSModel>,
        IDelete<IRSKey>,
        IGetData<RSModel, IRSKey>,
        IListData<RSModel>
    {
    }
    public class RSDal : IRSDal
    {
        public void Insert(RSModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_RS (
                        RSID, RSName, KotaID )
                VALUES (
                        @RSID, @RSName, @KotaID )";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@RSID", entity.RSID, SqlDbType.VarChar);
                cmd.AddParam("@RSName", entity.RSName, SqlDbType.VarChar);
                cmd.AddParam("@KotaID", entity.KotaID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(RSModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_RS 
                SET
                    RSname = @RSName,
                    KotaID = @KotaID
                WHERE
                    RSID = @RSID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@RSID", entity.RSID, SqlDbType.VarChar);
                cmd.AddParam("@RSName", entity.RSName, SqlDbType.VarChar);
                cmd.AddParam("@KotaID", entity.KotaID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public void Delete(IRSKey key)
        {
            var sql = @"
                DELETE
                    OFTA_RS 
                WHERE
                    RSID = @RSID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@RSID", key.RSID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public RSModel GetData(IRSKey key)
        {
            RSModel result = null;
            var sql = @"
                SELECT
                    aa.RSName, aa.KotaID,
                    ISNULL(bb.KotaName,'') KotaName
                FROM
                    OFTA_RS aa
                    LEFT JOIN OFTA_Kota bb ON aa.KotaID = bb.KotaID
                WHERE
                    RSID = @RSID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@RSID", key.RSID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new RSModel
                    {
                        RSID = key.RSID,
                        RSName = dr["RSName"].ToString(),
                        KotaID = dr["KotaID"].ToString(),
                        KotaName = dr["KotaName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<RSModel> ListData()
        {
            List<RSModel> result = null;
            var sql = @"
                SELECT
                    aa.RSID, aa.RSName, aa.KotaID,
                    ISNULL(bb.KotaName, '') KotaName
                FROM
                    OFTA_RS aa
                    LEFT JOIN OFTA_Kota bb ON aa.KotaID = bb.KotaID ";

            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<RSModel>();
                    while (dr.Read())
                    {
                        var itemResult = new RSModel
                        {
                            RSID = dr["RSID"].ToString(),
                            RSName = dr["RSName"].ToString(),
                            KotaID = dr["KotaID"].ToString(),
                            KotaName = dr["KotaName"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }
            }
            return result;
        }

    }
}
