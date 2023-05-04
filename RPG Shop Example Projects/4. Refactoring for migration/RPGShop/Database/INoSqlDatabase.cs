using RPGShop.Model;

namespace RPGShop.Database
{
    public interface INoSqlDatabase
    {
        void MakeSale(Sale sale);
        IEnumerable<Sale> GetSalesHistory();
        void AddToTab(Tab tab);
        Tab GetTabForCustomer(string customerName);
        void RemoveFromTab(string customerName);
        void AddCustomerDetails(CustomerDetails customerDetails);
        CustomerDetails GetCustomerByName(string customerName);
    }
}
