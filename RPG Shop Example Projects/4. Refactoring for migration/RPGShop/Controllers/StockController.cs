using Microsoft.AspNetCore.Mvc;
using RPGShop.Database;
using RPGShop.Logging;

namespace RPGShop.Controllers
{
    [ApiController]
    [Route("Shop/[controller]")]
    public class StockController : ControllerBase
    {
        private ISqlDatabase _sqlDb;
        private FileLogger _logger;

        public StockController(ISqlDatabase sqlDb)
        {
            _sqlDb = sqlDb;
            _logger = new FileLogger("C:/RPGLogs/log.txt");
        }

        [HttpGet]
        [Route("GetAllItems")]
        public IActionResult GetAllItems()
        {
            try
            {
                var items = _sqlDb.GetAllItems();
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetItemByName")]
        public IActionResult GetItemByName(string name)
        {
            try
            {
                var item = _sqlDb.GetItemByName(name);
                return Ok(item);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetItemsByType")]
        public IActionResult GetItemsByType(string type)
        {
            try
            {
                var items = _sqlDb.GetItemsbyType(type);
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetStockForItem")]
        public IActionResult GetStockForItem(string itemName)
        {
            try
            {
                var stockCount = _sqlDb.GetStockForItem(itemName);
                return Ok(stockCount);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Restock")]
        public IActionResult Restock(string itemName, int numberToRestock)
        {
            try
            {
                _sqlDb.AddStock(itemName, numberToRestock);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        
    }
}