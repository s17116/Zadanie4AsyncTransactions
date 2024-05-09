using Microsoft.AspNetCore.Mvc;
using Zadanie_4_AsyncTransactions.Models.DTOs;
using Zadanie_4_AsyncTransactions.Services.Interfaces;

namespace Zadanie_4_AsyncTransactions.Controllers
{
	[ApiController]
	[Route("api/warehouse")]
	public class WarehouseController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IProduct_WarehouseService _product_Warehouse;
		
		public WarehouseController(IConfiguration configuration, IProduct_WarehouseService product_Warehouse) 
		{
			this._configuration = configuration;
			this._product_Warehouse = product_Warehouse;
		}
		[HttpPost]
		public IActionResult AddProductWarehouse (Product_WarehouseInsertDTO product_Warehouse) 
		{
			var message = _product_Warehouse.AddNewProductWarehouse(product_Warehouse);
			if (message != null) 
			{
				return BadRequest(message);
			}
			var maxId = _product_Warehouse.GetNewestProductWarehouseId();
			return Ok(maxId);
		}
	}
}
