using DatabaseSetup.SqlQueryStrings;
using DatabaseSetup.TableData;
using System.Data.SqlClient;

namespace DatabaseSetup
{
    internal class Database
    {
        private SqlConnection _initialDbConnection;
        private SqlConnection _dbConnection;

        internal Database()
        {
            _initialDbConnection = new SqlConnection(Settings._initialConnectionString);
            _dbConnection = new SqlConnection(Settings._dbConnectionString);
        }

        internal void CreateDatabase()
        {
            using (SqlCommand command = new SqlCommand(DatabaseSetupQueries.CreateDatabase, _initialDbConnection))
            {
                _initialDbConnection.Open();
                command.ExecuteNonQuery();
                _initialDbConnection.Close();
            }
        }

        internal void CreateTables()
        {
            using (SqlCommand command = new SqlCommand(DatabaseSetupQueries.AllTables, _dbConnection))
            {
                _dbConnection.Open();
                command.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }

        internal void CreateTableData()
        {
            CreateTableData(DescriptionsData.Query);
            CreateTableData(ItemsData.Query);
            CreateTableData(ItemTypeData.Query);
            CreateTableData(StockData.Query);
        }

        private void CreateTableData(string query)
        {
            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                _dbConnection.Open();
                command.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }
    }
}
