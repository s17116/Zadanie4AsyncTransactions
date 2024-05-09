using Microsoft.AspNetCore.Mvc;
using Zadanie_4_AsyncTransactions.Models.DTOs;
using Zadanie_4_AsyncTransactions.Services.Interfaces;

namespace Zadanie_4_AsyncTransactions.Services.Impementations
{
	public class Product_WarehouseService : IProduct_WarehouseService
	{
		private readonly IDataBaseAcces _baseAcces;
		public Product_WarehouseService(IDataBaseAcces dataBaseAcces) 
		{
			this._baseAcces = dataBaseAcces;
		}
		public string AddNewProductWarehouse(Product_WarehouseInsertDTO product_Warehouse)
		{
			if (product_Warehouse.Amount <= 0)
			{
				return "amount less or equal than 0";
			}
			if (!_baseAcces.DoesProductExist(product_Warehouse.IdProduct))
			{
				return "product does not exist";
			}
			if (!_baseAcces.DoesWareHouseExist(product_Warehouse.IdWarehouse))
			{
				return "warehouse does not exist";
			}
			if (!_baseAcces.DoesOrderExist(product_Warehouse.IdProduct, product_Warehouse.Amount, product_Warehouse.CreatedAt)) 
			{
				return "order does not exist";
			}
			if(_baseAcces.DoesProductwarehouseExist(product_Warehouse.IdProduct, product_Warehouse.Amount))
			{
				return "Product warehouse already exist";
			}
			_baseAcces.UpdateOrder(product_Warehouse.IdProduct, product_Warehouse.Amount);
			_baseAcces.AddNewPrductWarehouse(product_Warehouse);
			return null;
		}

		public string GetNewestProductWarehouseId()
		{
			var maxId = _baseAcces.GetNewestProductWarehouseId();
			return maxId;
		}
	}
}
