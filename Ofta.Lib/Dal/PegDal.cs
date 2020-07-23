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
    public interface IPegDal :
        IInsert<PegModel>,
        IUpdate<PegModel>,
        IDelete<IPegKey>,
        IGetData<PegModel, IPegKey>,
        IListData<PegModel>
    {
    }

    public class PegDal :
        IPegDal
    {
        public void Insert(PegModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_Peg (
                        PegID, PegName, JabatanID)
                VALUES (
                        @PegID, @PegName, @JabatanID) ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@PegName", entity.PegName, SqlDbType.VarChar);
                cmd.AddParam("@JabatanID", entity.JabatanID, SqlDbType.VarChar);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(PegModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_Peg 
                SET
                    PegName = @PegName,
                    JabatanID = @JabatanID
                WHERE
                     PegID = @PegID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@PegName", entity.PegName, SqlDbType.VarChar);
                cmd.AddParam("@JabatanID", entity.JabatanID, SqlDbType.VarChar);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IPegKey key)
        {
            var sql = @"
                DELETE
                    OFTA_Peg 
                WHERE
                     PegID = @PegID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@PegID", key.PegID, SqlDbType.VarChar);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public PegModel GetData(IPegKey key)
        {
            PegModel result = null;
            var sql = @"
                SELECT
                    aa.PegID, aa.PegName, aa.JabatanID,
                    ISNULL(bb.JabatanName, '') JabatanName
                FROM
                    OFTA_Peg aa
                    LEFT JOIN OFTA_Jabatan bb ON aa.JabatanID = bb.JabatanID
                WHERE
                     PegID = @PegID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@PegID", key.PegID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new PegModel
                    {
                        PegID = dr["PegID"].ToString(),
                        PegName = dr["PegName"].ToString(),
                        JabatanID = dr["JabatanID"].ToString(),
                        JabatanName = dr["JabatanName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<PegModel> ListData()
        {
            List<PegModel> result = null;
            var sql = @"
                SELECT
                    aa.PegID, aa.PegName, aa.JabatanID,
                    ISNULL(bb.JabatanName, '') JabatanName
                FROM
                    OFTA_Peg aa
                    LEFT JOIN OFTA_Jabatan bb ON aa.JabatanID = bb.JabatanID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<PegModel>();
                    while (dr.Read())
                    {
                        var item = new PegModel
                        {
                            PegID = dr["PegID"].ToString(),
                            PegName = dr["PegName"].ToString(),
                            JabatanID = dr["JabatanID"].ToString(),
                            JabatanName = dr["JabatanName"].ToString()
                        };
                        result.Add(item);
                    }                
                }
            }
            return result;
        }
    }
}
