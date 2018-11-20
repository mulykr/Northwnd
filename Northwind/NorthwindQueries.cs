using System;
using System.Data.SqlClient;

namespace Northwnd
{
    public class NorthwndQueries : IDisposable
    {
        private string _connectionString;
        private SqlConnection _connection;

        public NorthwndQueries()
        {
            _connectionString = "Data source=LAPTOP-D9N84HG1\\SQLEXPRESS; Initial catalog=NORTHWND;Integrated security=true";
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        public void Q1()
        {
            var query = "SELECT * FROM Employees WHERE EmployeeID = 8";
            ExecuteQuery(query);
        }
        
        public void Q2()
        {
            var query = "SELECT LastName, FirstName FROM Employees WHERE City='London'";
            ExecuteQuery(query);
        }

        public void Q3()
        {
            var query = "SELECT LastName, FirstName FROM Employees WHERE FirstName LIKE 'A%'";
            ExecuteQuery(query);
        }

        public void Q5()
        {
            var query = "SELECT COUNT(*) FROM Employees WHERE City='London'";
            ExecuteQuery(query);
        }
        public void Q9()
        {
            var query = "SELECT LastName, FirstName  FROM Employees WHERE BirthDate=(Select MIN(BirthDate) From Employees)";
            ExecuteQuery(query);
        }
        public void Q10()
        {
            var query = "SELECT TOP 3 LastName, FirstName  FROM Employees ORDER BY BirthDate DESC";
            ExecuteQuery(query);
        }

        public void Q11()
        {
            var query = "SELECT DISTINCT City FROM Employees";
            ExecuteQuery(query);
        }

        public void Q17()
        {
            var query = "SELECT c.ContactName, COUNT(o.OrderID) Count FROM Customers c JOIN Orders o ON c.CustomerID=o.CustomerID WHERE c.Country='France' GROUP BY c.ContactName";
            ExecuteQuery(query);
        }

        public void Q13()
        {
            var query = "SELECT FirstName, LastName FROM Employees e JOIN Orders o ON e.EmployeeID = o.EmployeeID AND o.ShipCity = 'Madrid'";
            ExecuteQuery(query);
        }

        public void Q14()
        {
            var query = "SELECT e.FirstName, e.LastName, COUNT(o.OrderID) Count FROM Employees e LEFT JOIN Orders o ON e.EmployeeID = o.EmployeeID AND o.OrderDate >= '19970101' AND o.OrderDate <= '19971231' GROUP BY e.FirstName, e.LastName";
            ExecuteQuery(query);
        }

        public void Q30()
        {
            var query = "SELECT DISTINCT e.City EmployeeCity, c.City CustomerCity, o.ShipCity FROM Orders o JOIN Employees e ON e.EmployeeID = o.EmployeeID JOIN Customers c ON c.CustomerID = o.CustomerID";
            ExecuteQuery(query);
        }

        public void Q33()
        {
            var query = "UPDATE Employees SET City = 'Lviv' WHERE EmployeeID = 1";
            var command = new SqlCommand(query, _connection);
            var result = command.ExecuteNonQuery();

            Console.WriteLine($"Row(s) affected: {result}");
        }

        public void Q35()
        {
            var query = "DELETE FROM Employees WHERE EmployeeID = 2";
            var command = new SqlCommand(query, _connection);
            var result = command.ExecuteNonQuery();

            Console.WriteLine($"Row(s) affected: {result}");
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
