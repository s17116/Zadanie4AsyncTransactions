using System.Text.Json.Serialization;

namespace Zadanie_4_AsyncTransactions.Models.DTOs
{
	public class Product_WarehouseInsertDTO
	{
		public int IdProduct {  get; set; }
		public int IdWarehouse { get; set; }
		public int Amount { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
