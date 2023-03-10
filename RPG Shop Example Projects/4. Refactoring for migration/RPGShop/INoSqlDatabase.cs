using MongoDB.Driver;

namespace RPGShop
{
    public interface INoSqlDatabase
    {
        void MakeSale(Sale sale);
        List<Sale> GetSalesHistory();
        void AddToTab(Tab tab);
        Tab GetTabForCustomer(string customerName);
        void RemoveFromTab(string customerName);
        void AddCustomerDetails(CustomerDetails customerDetails);
        CustomerDetails GetCustomerByName(string customerName);
    }
}
