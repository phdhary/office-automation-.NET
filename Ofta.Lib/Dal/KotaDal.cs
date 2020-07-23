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
    public interface IKotaDal :
        IInsert<KotaModel>,
        IUpdate<KotaModel>,
        IDelete<IKotaKey>,
        IGetData<KotaModel, IKotaKey>,
        IListData<KotaModel>
    {
    }

    public class KotaDal : IKotaDal
    {
        public void Insert(KotaModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_Kota (
                        KotaID, KotaName )
                VALUES (
                        @KotaID, @KotaName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@KotaID", entity.KotaID, SqlDbType.VarChar);
                cmd.AddParam("@KotaName", entity.KotaName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(KotaModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_Kota 
                SET
                    KotaName = @KotaName
                WHERE
                    KotaID = @KotaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@KotaID", entity.KotaID, SqlDbType.VarChar);
                cmd.AddParam("@KotaName", entity.KotaName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IKotaKey key)
        {
            var sql = @"
                DELETE
                    OFTA_Kota 
                WHERE
                    KotaID = @KotaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@KotaID", key.KotaID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public KotaModel GetData(IKotaKey key)
        {
            KotaModel result = null;
            var sql = @"
                SELECT
                    KotaName
                FROM
                    OFTA_Kota
                WHERE
                    KotaID = @KotaID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@KotaID", key.KotaID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new KotaModel
                    {
                        KotaID = key.KotaID,
                        KotaName = dr["KotaName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<KotaModel> ListData()
        {
            List<KotaModel> result = null;
            var sql = @"
                SELECT
                    KotaID, KotaName
                FROM
                    OFTA_Kota ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<KotaModel>();
                    while (dr.Read())
                    {
                        var itemResult = new KotaModel
                        {
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
