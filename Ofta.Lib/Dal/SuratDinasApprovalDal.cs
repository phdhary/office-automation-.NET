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
    public interface ISuratDinasApprovalDal :
        IInsert<SuratDinasApprovalModel>,
        IDelete<ISuratDinasKey>,
        IListFilter<SuratDinasApprovalModel, ISuratDinasKey>
    { 
    }


    public class SuratDinasApprovalDal : ISuratDinasApprovalDal
    {
        public void Insert(SuratDinasApprovalModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_SuratDinasApproval (
                        SuratDinasID, PegID, ApprovalTypeID)
                VALUES (
                        @SuratDinasID, @PegID, @ApprovalTypeID) ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", entity.SuratDinasID, SqlDbType.VarChar);
                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@ApprovalTypeID", entity.ApprovalTypeID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }        
        }

        public void Delete(ISuratDinasKey key)
        {
            var sql = @"
                DELETE
                    OFTA_SuratDinasApproval
                WHERE
                    SuratDinasID = @SuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", key.SuratDinasID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<SuratDinasApprovalModel> ListData(ISuratDinasKey filter)
        {
            List<SuratDinasApprovalModel> result = null;
            var sql = @"
                SELECT
                    aa.SuratDinasID, aa.PegID, aa.ApprovalTypeID,
                    ISNULL(bb.ApprovalTypeName, '') ApprovalTypeName
                FROM
                    OFTA_SuratDinasApproval aa
                    LEFT JOIN OFTA_ApprovalType bb ON aa.ApprovalTypeID = bb.ApprovalTypeID
                WHERE
                    aa.SuratDinasID = @SuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", filter.SuratDinasID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if(!dr.HasRows)
                        return null;
                    result = new List<SuratDinasApprovalModel>();
                    while (dr.Read())
                    {
                        var item = new SuratDinasApprovalModel 
                        { 
                            SuratDinasID = dr["SuratDinasID"].ToString(),
                            PegID = dr["PegID"].ToString(),
                            ApprovalTypeID = dr["ApprovalTypeID"].ToString(),
                            ApprovalTypeName = dr["ApprovalTypeName"].ToString()
                        };
                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}
