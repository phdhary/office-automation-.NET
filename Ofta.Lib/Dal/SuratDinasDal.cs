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
    public interface ISuratDinasDal :
        IInsert<SuratDinasModel>,
        IUpdate<SuratDinasModel>,
        IDelete<ISuratDinasKey>,
        IGetData<SuratDinasModel, ISuratDinasKey>,
        IListPeriode<SuratDinasModel>
    {

    }
    public class SuratDinasDal : ISuratDinasDal
    {
        public void Insert(SuratDinasModel entity)
        {
            var sql = @"
                INSERT INTO 
                    OFTA_SuratDinas (
                        SuratDinasID, TglJamCreate, 
                        PegID, NoSurat, NoKontrak,
                        TglMulai, TglSelesai, Keperluan,
                        TransportID, KMAwal, 
                        RSID, JenisBiayaID, KasBon)
                VALUES (
                        @SuratDinasID, @TglJamCreate, 
                        @PegID, @NoSurat, @NoKontrak,
                        @TglMulai, @TglSelesai, @Keperluan,
                        @TransportID, @KMAwal, 
                        @RSID, @JenisBiayaID, @KasBon) ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", entity.SuratDinasID, SqlDbType.VarChar);
                cmd.AddParam("@TglJamCreate", entity.TglJamCreate, SqlDbType.DateTime);

                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@NoSurat", entity.NoSurat, SqlDbType.VarChar);
                cmd.AddParam("@NoKontrak", entity.NoKontrak, SqlDbType.VarChar);

                cmd.AddParam("@TglMulai", entity.TglMulai, SqlDbType.DateTime);
                cmd.AddParam("@TglSelesai", entity.TglSelesai, SqlDbType.DateTime);
                cmd.AddParam("@Keperluan", entity.Keperluan, SqlDbType.VarChar);

                cmd.AddParam("@TransportID", entity.TransportID, SqlDbType.VarChar);
                cmd.AddParam("@KMAwal", entity.KMAwal, SqlDbType.BigInt);

                cmd.AddParam("@RSID", entity.RSID, SqlDbType.VarChar);
                cmd.AddParam("@JenisBiayaID", entity.JenisBiayaID, SqlDbType.VarChar);
                cmd.AddParam("@KasBon", entity.KasBon, SqlDbType.Decimal);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(SuratDinasModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_SuratDinas 
                SET
                    TglJamCreate = @TglJamCreate, 
                    PegID = @PegID,
                    NoSurat = @NoSurat,
                    NoKontrak = @NoKontrak,
                    TglMulai = @TglMulai,
                    TglSelesai = @TglSelesai,
                    Keperluan = @Keperluan,
                    TransportID = @TransportID,
                    KMAwal = @KMAwal,
                    RSID = @RSID,
                    JenisBiayaID = @JenisBiayaID,
                    KasBon = @KasBon
                WHERE
                    SuratDinasID = @SuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", entity.SuratDinasID, SqlDbType.VarChar);
                cmd.AddParam("@TglJamCreate", entity.TglJamCreate, SqlDbType.DateTime);

                cmd.AddParam("@PegID", entity.PegID, SqlDbType.VarChar);
                cmd.AddParam("@NoSurat", entity.NoSurat, SqlDbType.VarChar);
                cmd.AddParam("@NoKontrak", entity.NoKontrak, SqlDbType.VarChar);

                cmd.AddParam("@TglMulai", entity.TglMulai, SqlDbType.DateTime);
                cmd.AddParam("@TglSelesai", entity.TglSelesai, SqlDbType.DateTime);
                cmd.AddParam("@Keperluan", entity.Keperluan, SqlDbType.VarChar);

                cmd.AddParam("@TransportID", entity.TransportID, SqlDbType.VarChar);
                cmd.AddParam("@KMAwal", entity.KMAwal, SqlDbType.BigInt);

                cmd.AddParam("@RSID", entity.RSID, SqlDbType.VarChar);
                cmd.AddParam("@JenisBiayaID", entity.JenisBiayaID, SqlDbType.VarChar);
                cmd.AddParam("@KasBon", entity.KasBon, SqlDbType.Decimal);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(ISuratDinasKey key)
        {
            var sql = @"
                DELETE
                    OFTA_SuratDinas 
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
        public SuratDinasModel GetData(ISuratDinasKey key)
        {
            SuratDinasModel result = null;
            var sql = @"
                SELECT
                    aa.SuratDinasID, aa.TglJamCreate, 
                    aa.PegID, aa.NoSurat, aa.NoKontrak,
                    aa.TglMulai, aa.TglSelesai, aa.Keperluan,
                    aa.TransportID, aa.KMAwal, 
                    aa.RSID, aa.JenisBiayaID, aa.KasBon,
                    ISNULL(bb.PegName, '') PegName,
                    ISNULL(cc.TransportName, '') TransportName,
                    ISNULL(dd.RSName, '') RSName,
                    ISNULL(ee.JenisBiayaName, '') JenisBiayaName
                FROM
                    OFTA_SuratDinas aa
                    LEFT JOIN OFTA_Peg bb ON aa.PegID = bb.PegID
                    LEFT JOIN OFTA_Transport cc oN aa.TransportID = cc.TransportID
                    LEFT JOIN OFTA_RS dd ON aa.RSID = dd.RSID
                    LEFT JOIN OFTA_JenisBiaya ee ON aa.JenisBiayaID = ee.JenisBiayaID
                WHERE
                    SuratDinasID = @SuratDinasID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@SuratDinasID", key.SuratDinasID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new SuratDinasModel
                    {
                        SuratDinasID = dr["SuratDinasID"].ToString(),
                        TglJamCreate = Convert.ToDateTime(dr["TglJamCreate"]),
                        PegID = dr["PegID"].ToString(),
                        PegName = dr["PegName"].ToString(),
                        NoSurat = dr["NoSurat"].ToString(),
                        NoKontrak = dr["NoKontrak"].ToString(),
                        TglMulai = Convert.ToDateTime(dr["TglMulai"]),
                        TglSelesai = Convert.ToDateTime(dr["TglSelesai"]),
                        Keperluan = dr["Keperluan"].ToString(),
                        TransportID = dr["TransportID"].ToString(),
                        TransportName = dr["TransportName"].ToString(),
                        KMAwal = Convert.ToInt64(dr["KMAwal"]),
                        RSID = dr["RSID"].ToString(),
                        RSName = dr["RSName"].ToString(),
                        JenisBiayaID = dr["JenisBiayaID"].ToString(),
                        JenisBiayaName = dr["JenisBiayaName"].ToString(),
                        KasBon = Convert.ToDecimal(dr["KasBon"])
                    };
                }
            }
            return result;
        }

        public IEnumerable<SuratDinasModel> ListData(DateTime tgl1, DateTime tgl2)
        {
            List<SuratDinasModel> result = null;
            var sql = @"
                SELECT
                    aa.SuratDinasID, aa.TglJamCreate, 
                    aa.PegID, aa.NoSurat, aa.NoKontrak,
                    aa.TglMulai, aa.TglSelesai, aa.Keperluan,
                    aa.TransportID, aa.KMAwal, 
                    aa.RSID, aa.JenisBiayaID, aa.KasBon,
                    ISNULL(bb.PegName, '') PegName,
                    ISNULL(cc.TransportName, '') TransportName,
                    ISNULL(dd.RSName, '') RSName,
                    ISNULL(ee.JenisBiayaName, '') JenisBiayaName
                FROM
                    OFTA_SuratDinas aa
                    LEFT JOIN OFTA_Peg bb ON aa.PegID = bb.PegID
                    LEFT JOIN OFTA_Transport cc oN aa.TransportID = cc.TransportID
                    LEFT JOIN OFTA_RS dd ON aa.RSID = dd.RSID
                    LEFT JOIN OFTA_JenisBiaya ee ON aa.JenisBiayaID = ee.JenisBiayaID
                WHERE
                    TglJamCreate BETWEEN @Tgl1 AND @Tgl2 ";
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
                    result = new List<SuratDinasModel>();
                    while (dr.Read())
                    {
                        var item = new SuratDinasModel
                        {
                            SuratDinasID = dr["SuratDinasID"].ToString(),
                            TglJamCreate = Convert.ToDateTime(dr["TglJamCreate"]),
                            PegID = dr["PegID"].ToString(),
                            PegName = dr["PegName"].ToString(),
                            NoSurat = dr["NoSurat"].ToString(),
                            NoKontrak = dr["NoKontrak"].ToString(),
                            TglMulai = Convert.ToDateTime(dr["TglMulai"]),
                            TglSelesai = Convert.ToDateTime(dr["TglSelesai"]),
                            Keperluan = dr["Keperluan"].ToString(),
                            TransportID = dr["TransportID"].ToString(),
                            TransportName = dr["TransportName"].ToString(),
                            KMAwal = Convert.ToInt64(dr["KMAwal"]),
                            RSID = dr["RSID"].ToString(),
                            RSName = dr["RSName"].ToString(),
                            JenisBiayaID = dr["JenisBiayaID"].ToString(),
                            JenisBiayaName = dr["JenisBiayaName"].ToString(),
                            KasBon = Convert.ToDecimal(dr["KasBon"])
                        };
                        result.Add(item);
                    }   
                }
            }
            return result;
        }
    }
}
