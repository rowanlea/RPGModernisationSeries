using Microsoft.AspNetCore.Mvc;

namespace RPGShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private SQLDatabase _sqlDb;
        private NoSQLDatabase _noSqlDb;
        private FileLogger _logger;

        public ShopController()
        {
            _sqlDb = new SQLDatabase();
            _noSqlDb = new NoSQLDatabase();
            _logger = new FileLogger("C:/RPGLogs/log.txt");gedrgerdfgfer
        }

        [HttpGet]
        [Route("Stock/GetAllItems")]
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
        [Route("Stock/GetItemsByName")]
        public IActionResult GetItemsByName(string name)
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
        [Route("Stock/GetItemsByType")]
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
        [Route("Stock/GetStockForItem")]
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
        [Route("Stock/Restock")]
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

        [HttpPost]
        [Route("Sales/SellItemsToCustomer")]
        public IActionResult SellItemsToCustomer(CustomerOrder customerOrder)
        {
            try
            {
                double price = 0;

                foreach (var item in customerOrder.Items)
                {
                    var foundItem = _sqlDb.GetItemByName(item.Name);
                    _sqlDb.RemoveStock(item.Name, item.Count);
                    price += foundItem.Price * item.Count;
                }

                if (customerOrder.IsTab)
                {
                    _noSqlDb.AddToTab(new Tab { Items = customerOrder.Items, CustomerName = customerOrder.CustomerDetails.Name });
                }
                else
                {
                    _noSqlDb.MakeSale(new Sale { Items = customerOrder.Items, CustomerName = customerOrder.CustomerDetails.Name, Price = price });
                }

                _noSqlDb.AddCustomerDetails(customerOrder.CustomerDetails);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Sales/GetSalesHistory")]
        public IActionResult GetSalesHistory()
        {
            try
            {
                var salesHistory = _noSqlDb.GetSalesHistory();
                return Ok(salesHistory);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Sales/PayTab")]
        public IActionResult PayTab(string customerName)
        {
            try
            {
                var tab = _noSqlDb.GetTabForCustomer(customerName);

                double price = 0;
                foreach (var item in tab.Items)
                {
                    var foundItem = _sqlDb.GetItemByName(item.Name);
                    price += foundItem.Price * item.Count;
                }

                var sale = new Sale { Items = tab.Items, CustomerName = customerName, Price = price };
                _noSqlDb.MakeSale(sale);
                _noSqlDb.RemoveFromTab(customerName);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Sales/GetCustomerDetails")]
        public IActionResult GetCustomerDetails(string customerName)
        {
            try
            {
                var customerDetails = _noSqlDb.GetCustomerByName(customerName);
                return Ok(customerDetails);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                return NotFound();
            }
        }
    }
}