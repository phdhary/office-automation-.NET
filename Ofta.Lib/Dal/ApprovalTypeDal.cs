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
	public interface IApprovalTypeDal :
		IInsert<ApprovalTypeModel>,
		IUpdate<ApprovalTypeModel>,
		IDelete<IApprovalTypeKey>,
		IGetData<ApprovalTypeModel, IApprovalTypeKey>,
		IListData<ApprovalTypeModel>
	{
	}

	public class ApprovalTypeDal : IApprovalTypeDal
	{
		public void Insert(ApprovalTypeModel entity)
		{
			var sql = @"
                INSERT INTO 
                    OFTA_ApprovalType (
                        ApprovalTypeID, ApprovalTypeName )
                VALUES (
                        @ApprovalTypeID, @ApprovalTypeName)";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@ApprovalTypeID", entity.ApprovalTypeID, SqlDbType.VarChar);
				cmd.AddParam("@ApprovalTypeName", entity.ApprovalTypeName, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void Update(ApprovalTypeModel entity)
		{
			var sql = @"
                UPDATE
                    OFTA_ApprovalType 
                SET
                    ApprovalTypeName = @ApprovalTypeName
                WHERE
                    ApprovalTypeID = @ApprovalTypeID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@ApprovalTypeID", entity.ApprovalTypeID, SqlDbType.VarChar);
				cmd.AddParam("@ApprovalTypeName", entity.ApprovalTypeName, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void Delete(IApprovalTypeKey key)
		{
			var sql = @"
                DELETE
                    OFTA_ApprovalType 
                WHERE
                    ApprovalTypeID = @ApprovalTypeID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@ApprovalTypeID", key.ApprovalTypeID, SqlDbType.VarChar);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public ApprovalTypeModel GetData(IApprovalTypeKey key)
		{
			ApprovalTypeModel result = null;
			var sql = @"
                SELECT
                    ApprovalTypeName
                FROM
                    OFTA_ApprovalType
                WHERE
                    ApprovalTypeID = @ApprovalTypeID ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				cmd.AddParam("@ApprovalTypeID", key.ApprovalTypeID, SqlDbType.VarChar);
				conn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					if (!dr.HasRows)
						return null;
					dr.Read();
					result = new ApprovalTypeModel
					{
						ApprovalTypeID = key.ApprovalTypeID,
						ApprovalTypeName = dr["ApprovalTypeName"].ToString()
					};
				}
			}
			return result;
		}

		public IEnumerable<ApprovalTypeModel> ListData()
		{
			List<ApprovalTypeModel> result = null;
			var sql = @"
                SELECT
                    ApprovalTypeID, ApprovalTypeName
                FROM
                    OFTA_ApprovalType ";
			using (var conn = new SqlConnection(ConnStringHelper.Get()))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					if (!dr.HasRows)
						return null;
					result = new List<ApprovalTypeModel>();
					while (dr.Read())
					{
						var itemResult = new ApprovalTypeModel
						{
							ApprovalTypeID = dr["ApprovalTypeID"].ToString(),
							ApprovalTypeName = dr["ApprovalTypeName"].ToString()
						};
						result.Add(itemResult);
					}
				}
			}
			return result;
		}
	}
}
