using Microsoft.AspNetCore.Mvc;
using Zadanie_4_AsyncTransactions.Models.DTOs;

namespace Zadanie_4_AsyncTransactions.Services.Interfaces
{
	public interface IProduct_WarehouseService
	{
		public string AddNewProductWarehouse(Product_WarehouseInsertDTO product_WarehouseInsertDTO);
		public string GetNewestProductWarehouseId();
	}
}
