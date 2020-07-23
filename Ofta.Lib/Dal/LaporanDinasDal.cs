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
    public interface ILaporanDinasDal :
        IInsert<LaporanDinasModel>,
        IUpdate<LaporanDinasModel>,
        IDelete<ILaporanDinasKey>,
        IGetData<LaporanDinasModel, ILaporanDinasKey>,
        IGetData<LaporanDinasModel, ISuratDinasKey>,
        IListPeriode<LaporanDinasModel>
    {
    }

    public class LaporanDinasDal : ILaporanDinasDal
    {
        public void Insert(LaporanDinasModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_LaporanDinas (
                        LaporanDinasID, TglJamCreate,
                        ReportedPegID, SuratDinasID, TglSelesai,
                        HasilKerja, KMAkhir, DiketahuiPegID,
                        IsSignedDiketahui)
                VALUES ( 
                        @LaporanDinasID, @TglJamCreate,
                        @ReportedPegID, @SuratDinasID, @TglSelesai,
                        @HasilKerja, @KMAkhir, @DiketahuiPegID,
                        @IsSignedDiketahui)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@LaporanDinasID", entity.LaporanDinasID, SqlDbType.VarChar);
                cmd.AddParam("@TglJamCreate", entity.TglJamCreate, SqlDbType.DateTime);

                cmd.AddParam("@ReportedPegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@SuratDinasID", entity.SuratDinasID, SqlDbType.VarChar);

                cmd.AddParam("@TglSelesai", entity.TglSelesai, SqlDbType.DateTime);

                cmd.AddParam("@HasilKerja", entity.HasilKerja, SqlDbType.VarChar);
                cmd.AddParam("@KMAkhir", entity.KMAkhir, SqlDbType.BigInt);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(LaporanDinasModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_LaporanDinas 
                SET
                    TglJamCreate = @TglJamCreate, 
                    ReportedPegID = @ReportedPegID,
                    SuratDinasID = @SuratDinasID,
                    TglSelesai = @TglSelesai,
					HasilKerja = @HasilKerja,
                    KMAkhir = @KMAkhir,
                    DiketahuiPegID = @DiketahuiPegID,
                    IsSignedDiketahui = @IsSignedDiketahui
                WHERE
                    LaporanDinasID = @LaporanDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@LaporanDinasID", entity.LaporanDinasID, SqlDbType.VarChar);
                cmd.AddParam("@TglJamCreate", entity.TglJamCreate, SqlDbType.DateTime);

                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@SuratDinasID", entity.SuratDinasID, SqlDbType.VarChar);

                cmd.AddParam("@TglSelesai", entity.TglSelesai, SqlDbType.DateTime);

                cmd.AddParam("@HasilKerja", entity.HasilKerja, SqlDbType.VarChar);
                cmd.AddParam("@KMAkhir", entity.KMAkhir, SqlDbType.BigInt);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(ILaporanDinasKey key)
        {
            var sql = @"
                DELETE
                    OFTA_LaporanDinas
                WHERE
                LaporanDinasID = @LaporanDinasID";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@LaporanDinasID", key.LaporanDinasID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public LaporanDinasModel GetData(ILaporanDinasKey key)
        {
            LaporanDinasModel result = null;
            var sql = @"
                SELECT
                    aa.LaporanDinasID, aa.TglJamCreate,
                    aa.ReportedPegID, aa.SuratDinasID,
                    aa.TglSelesai, aa.HasilKerja, aa.KMAkhir,
                    aa.DiketahuiPegID, aa.IsSignedDiketahui,
                    ISNULL( bb.PegName,'') ReportedPegName,
                    ISNULL( cc.PegName, '') DiketahuiPegName,
                    ISNULL( dd.TglMulai, '1900-01-01T00:00:00') TglMulai
                FROM
                    OFTA_LaporanDinas aa
                    LEFT JOIN Peg bb ON aa.ReportedPegID = bb.PegID
                    LEFT JOIN Peg cc ON aa.DiketahuiPegID = cc.PegID
                    LEFT JOIN SuratDinas dd ON aa.SuratDinasID = dd.SuratDinasID
                WHERE
                    LaporanDinasID = @LaporanDinasID ";

            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@LaporanDinasID", key.LaporanDinasID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new LaporanDinasModel
                    {
                        LaporanDinasID = dr["LaporanDinasID"].ToString(),
                        TglJamCreate = Convert.ToDateTime(dr["TglJamCreate"]),
                        PegID = dr["ReportedPegID"].ToString(),
                        PegName = dr["ReportedPegName"].ToString(),
                        SuratDinasID = dr["SuratDinasID"].ToString(),
                        TglMulai = Convert.ToDateTime(dr["TglMulai"]),
                        TglSelesai = Convert.ToDateTime(dr["TglSelesai"]),
                        HasilKerja = dr["HasilKerja"].ToString(),
                        KMAkhir = Convert.ToInt64(dr["KMAkhir"])
                    };
                }
            }
            return result;

        }
        public LaporanDinasModel GetData(ISuratDinasKey key)
        {
            LaporanDinasModel result = null;
            var sql = @"
                SELECT
                    aa.LaporanDinasID, aa.TglJamCreate,
                    aa.ReportedPegID, aa.SuratDinasID,
                    aa.TglSelesai, aa.HasilKerja, aa.KMAkhir,
                    aa.DiketahuiPegID, aa.IsSignedDiketahui,
                    ISNULL( bb.PegName,'') ReportedPegName,
                    ISNULL( cc.PegName, '') DiketahuiPegName,
                    ISNULL( dd.TglMulai, '1900-01-01T00:00:00') TglMulai
                FROM
                    OFTA_LaporanDinas aa
                    LEFT JOIN Peg bb ON aa.ReportedPegID = bb.PegID
                    LEFT JOIN Peg cc ON aa.DiketahuiPegID = cc.PegID
                    LEFT JOIN SuratDinas dd ON aa.SuratDinasID = dd.SuratDinasID
                WHERE
                    SuratDinasID = @SuratDinasID ";

            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@LaporanDinasID", key.SuratDinasID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new LaporanDinasModel
                    {
                        LaporanDinasID = dr["LaporanDinasID"].ToString(),
                        TglJamCreate = Convert.ToDateTime(dr["TglJamCreate"]),
                        PegID = dr["ReportedPegID"].ToString(),
                        PegName = dr["ReportedPegName"].ToString(),
                        SuratDinasID = dr["SuratDinasID"].ToString(),
                        TglMulai = Convert.ToDateTime(dr["TglMulai"]),
                        TglSelesai = Convert.ToDateTime(dr["TglSelesai"]),
                        HasilKerja = dr["HasilKerja"].ToString(),
                        KMAkhir = Convert.ToInt64(dr["KMAkhir"])
                    };
                }
            }
            return result;
        }

        public IEnumerable<LaporanDinasModel> ListData(DateTime tgl1, DateTime tgl2)
        {
            List<LaporanDinasModel> result = null;
            var sql = @"
                SELECT
                    aa.LaporanDinasID, aa.TglJamCreate, 
                    aa.PegID, aa.SuratDinasID, aa.TglSelesai,
                    aa.HasilKerja, aa.KMAkhir, 
                    aa.IsSignedDiketahui,
                    ISNULL(bb.PegName, '') PegName,
                    ISNULL(dd.TglMulai, '1900-01-01T00:00:00') TglMulai 
                FROM
                    OFTA_LaporanDinas aa
                    LEFT JOIN SuratDinas dd ON aa.SuratDinasID = dd.SuratDinasID
                WHERE
                    aa.TglJamCreate BETWEEN @Tgl1 AND @Tgl2 ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@Tgl1", tgl1, SqlDbType.DateTime);
                cmd.AddParam("@Tgl2", tgl2, SqlDbType.DateTime);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<LaporanDinasModel>();
                    while (dr.Read())
                    {
                        var item = new LaporanDinasModel
                        {
                            LaporanDinasID = dr["LaporanDinasID"].ToString(),
                            TglJamCreate = Convert.ToDateTime(dr["TglJamCreate"]),
                            PegID = dr["ReportedPegID"].ToString(),
                            PegName = dr["ReportedPegName"].ToString(),
                            SuratDinasID = dr["SuratDinasID"].ToString(),
                            TglMulai = Convert.ToDateTime(dr["TglMulai"]),
                            TglSelesai = Convert.ToDateTime(dr["TglSelesai"]),
                            HasilKerja = dr["HasilKerja"].ToString(),
                            KMAkhir = Convert.ToInt64(dr["KMAkhir"]),
                        };
                        result.Add(item);
                    }
                }
            }
            return result;
        }

    }
}