using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwnd;

namespace NorthwindTests
{
    [TestClass]
    public class TestCase
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionStringQA"].ConnectionString);
        private readonly NorthwndQueries _queries = new NorthwndQueries(ConfigurationManager.ConnectionStrings["connectionStringQA"].ConnectionString);

        public TestCase()
        {
            _connection.Open();
        }

        [TestMethod]
        public void Q1()
        {
            var q = _queries.Q1();
            var res = ExecuteQuery(q, "LastName");
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("Callahan", res[0]);
        }

        public List<object> ExecuteQuery(string query, string ordinal)
        {
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            var objs = new List<object>();
            while (reader.Read())
            {
                objs.Add(reader.GetValue(reader.GetOrdinal(ordinal)));
            }

            return objs;
        }

        public int GetRowsAffected(string query)
        {
            var command = new SqlCommand(query, _connection);
            return command.ExecuteNonQuery();
        }
    }
}
