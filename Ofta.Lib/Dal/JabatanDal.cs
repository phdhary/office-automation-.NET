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
    public interface IJabatanDal :
        IInsert<JabatanModel>,
        IUpdate<JabatanModel>,
        IDelete<IJabatanKey>,
        IGetData<JabatanModel, IJabatanKey>,
        IListData<JabatanModel>
    {
    }

    public class JabatanDal : IJabatanDal
    {
        public void Insert(JabatanModel entity)
        {
			var sql = @"
                INSERT INTO 
                    OFTA_Jabatan (
                        JabatanID, JabatanName )
                VALUES (
                        @JabatanID, @JabatanName)";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@JabatanID", entity.JabatanID, SqlDbType.VarChar);
				cmd.AddParam("@JabatanName", entity.JabatanName, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

        public void Update(JabatanModel entity)
        {
			var sql = @"
                UPDATE
                    OFTA_Jabatan 
                SET
                    JabatanName = @JabatanName
                WHERE
                    JabatanID = @JabatanID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@JabatanID", entity.JabatanID, SqlDbType.VarChar);
				cmd.AddParam("@JabatanName", entity.JabatanName, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

        public void Delete(IJabatanKey key)
        {
			var sql = @"
                DELETE
                    OFTA_Jabatan 
                WHERE
                    JabatanID = @JabatanID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@JabatanID", key.JabatanID, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

        public JabatanModel GetData(IJabatanKey key)
        {
			JabatanModel result = null;
			var sql = @"
                SELECT
                    JabatanName
                FROM
                    OFTA_Jabatan
                WHERE
                    JabatanID = @JabatanID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@JabatanID", key.JabatanID, SqlDbType.VarChar);
				conn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					if (!dr.HasRows)
						return null;
					dr.Read();
					result = new JabatanModel
					{
						JabatanID = key.JabatanID,
						JabatanName = dr["JabatanName"].ToString()
					};
				}
			}
			return result;
		}

        public IEnumerable<JabatanModel> ListData()
        {
			List<JabatanModel> result = null;
			var sql = @"
                SELECT
                    JabatanID, JabatanName
                FROM
                    OFTA_Jabatan ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					if (!dr.HasRows)
						return null;
					result = new List<JabatanModel>();
					while (dr.Read())
					{
						var itemResult = new JabatanModel
						{
							JabatanID = dr["JabatanID"].ToString(),
							JabatanName = dr["JabatanName"].ToString()
						};
						result.Add(itemResult);
					}
				}
			}
			return result;
		}
    }
}
