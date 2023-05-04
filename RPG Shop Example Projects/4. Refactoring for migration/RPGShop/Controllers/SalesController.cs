using Microsoft.AspNetCore.Mvc;
using RPGShop.Database;
using RPGShop.Logging;
using RPGShop.Model;

namespace RPGShop.Controllers
{
    [Route("Shop/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private ISqlDatabase _sqlDb;
        private INoSqlDatabase _noSqlDb;
        private FileLogger _logger;

        public SalesController(ISqlDatabase sqlDb, INoSqlDatabase noSql)
        {
            _sqlDb = sqlDb;
            _noSqlDb = noSql;
            _logger = new FileLogger("C:/RPGLogs/log.txt");
        }

        [HttpPost]
        [Route("SellItemsToCustomer")]
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
        [Route("GetSalesHistory")]
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
        [Route("PayTab")]
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
        [Route("GetCustomerDetails")]
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
