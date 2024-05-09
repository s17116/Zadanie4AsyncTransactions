using Zadanie_4_AsyncTransactions.Models.DTOs;

namespace Zadanie_4_AsyncTransactions.Services.Interfaces
{
	public interface IDataBaseAcces
	{
		public bool DoesProductExist(int productId);
		public bool DoesWareHouseExist(int idWareHouse);
		public bool DoesOrderExist(int idPrduct, int amount, DateTime createdAt);
		public bool DoesProductwarehouseExist(int productId, int amount);
		public void UpdateOrder(int productId, int amount);
		public void AddNewPrductWarehouse(Product_WarehouseInsertDTO product_WarehouseDTO);
		public string GetNewestProductWarehouseId();
	}
}
