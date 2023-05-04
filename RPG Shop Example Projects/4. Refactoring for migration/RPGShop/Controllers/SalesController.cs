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
                double totalPrice = 0;

                foreach (var item in customerOrder.Items)
                {
                    Item foundItem = SellItem(item);
                    IncreaseTotalPrice(ref totalPrice, item, foundItem);
                }

                ProcessCustomerOrder(customerOrder, totalPrice);

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

                double totalPrice = CalculateTotalPrice(tab);
                MakeSale(customerName, tab, totalPrice);

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

        private void ProcessCustomerOrder(CustomerOrder customerOrder, double totalPrice)
        {
            if (customerOrder.IsTab)
            {
                _noSqlDb.AddToTab(new Tab { Items = customerOrder.Items, CustomerName = customerOrder.CustomerDetails.Name });
            }
            else
            {
                _noSqlDb.MakeSale(new Sale { Items = customerOrder.Items, CustomerName = customerOrder.CustomerDetails.Name, Price = totalPrice });
            }

            _noSqlDb.AddCustomerDetails(customerOrder.CustomerDetails);
        }

        private void IncreaseTotalPrice(ref double price, Item item, Item foundItem)
        {
            price += foundItem.Price * item.Count;
        }

        private Item SellItem(Item item)
        {
            var foundItem = _sqlDb.GetItemByName(item.Name);
            _sqlDb.RemoveStock(item.Name, item.Count);
            return foundItem;
        }

        private void MakeSale(string customerName, Tab tab, double totalPrice)
        {
            var sale = new Sale { Items = tab.Items, CustomerName = customerName, Price = totalPrice };
            _noSqlDb.MakeSale(sale);
            _noSqlDb.RemoveFromTab(customerName);
        }

        private double CalculateTotalPrice(Tab tab)
        {
            double totalPrice = 0;
            foreach (var item in tab.Items)
            {
                var foundItem = _sqlDb.GetItemByName(item.Name);
                IncreaseTotalPrice(ref totalPrice, item, foundItem);
            }

            return totalPrice;
        }
    }
}
