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
    public interface ITransportDal :
        IInsert<TransportModel>,
        IUpdate<TransportModel>,
        IDelete<ITransportKey>,
        IGetData<TransportModel, ITransportKey>,
        IListData<TransportModel>
    {
    }

    public class TransportDal : ITransportDal
    {
        public void Insert(TransportModel entity)
        {
            var sql = @"
                INSERT INTO
                    OFTA_Transport (
                        TransportID, TransportName)
                VALUES(
                        @TransportID, @TransportName)";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@TransportID", entity.TransportID, SqlDbType.VarChar);
                cmd.AddParam("@TransportName", entity.TransportName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(TransportModel entity)
        {
            var sql = @"
                UPDATE
                    OFTA_Transport 
                SET
                    TransportName = @TransportName
                WHERE
                    TransportID = @TransportID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@TransportID", entity.TransportID, SqlDbType.VarChar);
                cmd.AddParam("@TransportName", entity.TransportName, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(ITransportKey key)
        {
            var sql = @"
                DELETE
                    OFTA_Transport 
                WHERE
                    TransportID = @TransportID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@TransportID", key.TransportID, SqlDbType.VarChar);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TransportModel GetData(ITransportKey key)
        {
            TransportModel result = null;
            var sql = @"
                SELECT
                    TransportName
                FROM
                    OFTA_Transport
                WHERE
                    TransportID = @TransportID ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.AddParam("@TransportID", key.TransportID, SqlDbType.VarChar);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    dr.Read();
                    result = new TransportModel
                    {
                        TransportID = key.TransportID,
                        TransportName = dr["TransportName"].ToString()
                    };
                }
            }
            return result;
        }

        public IEnumerable<TransportModel> ListData()
        {
            List<TransportModel> result = null;
            var sql = @"
                SELECT
                    TransportID,TransportName
                FROM
                    OFTA_Transport ";
            using (var conn = new SqlConnection(ConnStringHelper.Get()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                        return null;
                    result = new List<TransportModel>();
                    while (dr.Read())
                    {
                        var itemResult = new TransportModel
                        {
                            TransportID = dr["TransportID"].ToString(),
                            TransportName = dr["TransportName"].ToString()
                        };
                        result.Add(itemResult);
                    }
                }
            }
            return result;
        }
    }

}

