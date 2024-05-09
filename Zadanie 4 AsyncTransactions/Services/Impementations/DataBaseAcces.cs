using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using Zadanie_4_AsyncTransactions.Models.DTOs;
using Zadanie_4_AsyncTransactions.Services.Interfaces;

namespace Zadanie_4_AsyncTransactions.Services.Impementations
{
	public class DataBaseAcces : IDataBaseAcces
	{
		private readonly IConfiguration _configuration;

		public DataBaseAcces(IConfiguration configuration)
		{
			this._configuration = configuration;
		}

		public void AddNewPrductWarehouse(Product_WarehouseInsertDTO product_WarehouseDTO)
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "insert into Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
					"values " +
					"(" +
					"@idWarehouse," +
					"@idProduct," +
					"(select IdOrder from [order] where IdProduct = @idProduct and Amount = @amount)," +
					"@amount," +
					"(select Price * @amount from Product where IdProduct = @idProduct)," +
					"@createdAt" +
					")";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idWarehouse", product_WarehouseDTO.IdWarehouse);
					sqlCommand.Parameters.AddWithValue("@idProduct", product_WarehouseDTO.IdProduct);
					sqlCommand.Parameters.AddWithValue("@amount", product_WarehouseDTO.Amount);
					sqlCommand.Parameters.AddWithValue("@createdAt", product_WarehouseDTO.CreatedAt);
					sqlCommand.ExecuteNonQuery();
				}
			}
		}

		public bool DoesOrderExist(int idProduct, int amount, DateTime createdAt)
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "select 1 from [order] where IdProduct = @idProduct and Amount = @amount and CreatedAt < @createdAt";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idProduct", idProduct);
					sqlCommand.Parameters.AddWithValue("@amount", amount);
					sqlCommand.Parameters.AddWithValue("@createdAt", createdAt);
					using (var reader = sqlCommand.ExecuteReader())
					{
						return reader.HasRows;
					}
				}
			}
		}

		public bool DoesProductExist(int productId) 
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "select 1 from product where IdProduct = @idProduct";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idProduct", productId);
					using (var reader = sqlCommand.ExecuteReader())
					{
						return reader.HasRows;
					}
				}
			}
		}

		public bool DoesProductwarehouseExist(int productId, int amount)
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "select 1 from Product_Warehouse where IdOrder = (select IdOrder from [order] where IdProduct = @idProduct and Amount = @amount)";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idProduct", productId);
					sqlCommand.Parameters.AddWithValue("@amount", amount);
					
					using (var reader = sqlCommand.ExecuteReader())
					{
						return reader.HasRows;
					}
				}
			}
		}

		public bool DoesWareHouseExist(int warehouseId)
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "select 1 from warehouse where IdWarehouse = @idWarehouse";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idWarehouse", warehouseId);
					using (var reader = sqlCommand.ExecuteReader())
					{
						return reader.HasRows;
					}
				}
			}

		}

		public string GetNewestProductWarehouseId()
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "select max(IdProductWarehouse) as maxId from Product_warehouse";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					using (var reader = sqlCommand.ExecuteReader())
					{
						reader.Read();
						return reader["maxId"].ToString();
					}
				}
			}
		}

		public void UpdateOrder(int productId, int amount)
		{
			string connectionString = _configuration["ConnectionString"];
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var query = "update [order] set FulfilledAt = getdate() where IdProduct = @idProduct and Amount = @amount";

				sqlConnection.Open();
				using (var sqlCommand = new SqlCommand(query, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@idProduct", productId);
					sqlCommand.Parameters.AddWithValue("@amount", amount);
					sqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
