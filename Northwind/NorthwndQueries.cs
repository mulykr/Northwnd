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
