using System;
using System.Data.SqlClient;

namespace Northwnd
{
    public class NorthwndQueries : IDisposable
    {
        private string _connectionString;
        private SqlConnection _connection;

        public NorthwndQueries(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        public string Q1(int id = 8)
        {
            var query = $"SELECT * FROM Employees WHERE EmployeeID = {id}";
            ExecuteQuery(query);
            return query;
        }
        
        public string Q2(string city = "London")
        {
            var query = $"SELECT LastName, FirstName FROM Employees WHERE City='{city}'";
            ExecuteQuery(query);
            return query;
        }

        public string Q3(string condition = "A%")
        {
            var query = $"SELECT LastName, FirstName FROM Employees WHERE FirstName LIKE '{condition}'";
            ExecuteQuery(query);
            return query;
        }

        public string Q5(string city = "London")
        {
            var query = $"SELECT COUNT(*) AS Count FROM Employees WHERE City='{city}'";
            ExecuteQuery(query);
            return query;
        }
        public string Q9()
        {
            var query = "SELECT LastName, FirstName  FROM Employees WHERE BirthDate=(Select MIN(BirthDate) From Employees)";
            ExecuteQuery(query);
            return query;
        }
        public string Q10()
        {
            var query = "SELECT TOP 3 LastName, FirstName  FROM Employees ORDER BY BirthDate DESC";
            ExecuteQuery(query);
            return query;
        }

        public string Q11()
        {
            var query = "SELECT DISTINCT City FROM Employees";
            ExecuteQuery(query);
            return query;
        }

        public string Q17(string country="France")
        {
            var query = $"SELECT c.ContactName, COUNT(o.OrderID) Count FROM Customers c JOIN Orders o ON c.CustomerID=o.CustomerID WHERE c.Country='{country}' GROUP BY c.ContactName";
            ExecuteQuery(query);
            return query;
        }

        public string Q13(string city="Madrid")
        {
            var query = $"SELECT FirstName, LastName FROM Employees e JOIN Orders o ON e.EmployeeID = o.EmployeeID AND o.ShipCity = '{city}'";
            ExecuteQuery(query);
            return query;
        }

        public string Q19(string country="France", int count =10)
        {
            var query = $"SELECT DISTINCT c.ContactName FROM Customers c JOIN Orders o ON c.CustomerID = o.CustomerID AND c.Country = '{country}' WHERE (SELECT COUNT(*) FROM Orders oo WHERE oo.CustomerID = c.CustomerID) > {count}";
            ExecuteQuery(query);
            return query;
        }

        public string Q30()
        {
            var query = "SELECT DISTINCT e.City EmployeeCity, c.City CustomerCity, o.ShipCity FROM Orders o JOIN Employees e ON e.EmployeeID = o.EmployeeID JOIN Customers c ON c.CustomerID = o.CustomerID";
            ExecuteQuery(query);
            return query;
        }

        public string Q33(string city="Lviv", int id=1)
        {
            var query = $"UPDATE Employees SET City = '{city}' WHERE EmployeeID = {id}";
            var command = new SqlCommand(query, _connection);
            var result = command.ExecuteNonQuery();

            Console.WriteLine($"Row(s) affected: {result}");
            return query;
        }

        public string Q35(int id = 2)
        {
            var query = "DELETE FROM Employees WHERE EmployeeID = {id}";
            var command = new SqlCommand(query, _connection);
            var result = command.ExecuteNonQuery();

            Console.WriteLine($"Row(s) affected: {result}");
            return query;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void ExecuteQuery(string query)
        {
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            ShowFromReader(reader);
            reader.Close();
        }

        private void ShowFromReader(SqlDataReader reader)
        {
            int k = 1;
            Console.WriteLine("data \n{");
            while (reader.Read())
            {
                Console.WriteLine($"\tItem {k++}:");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine("\t\t" + reader.GetName(i) + ": " + reader.GetValue(i));
                }
            }
            Console.WriteLine("}");
        }
    }
}
