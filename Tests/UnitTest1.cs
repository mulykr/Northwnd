using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwnd;

namespace Tests
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

        [TestMethod]
        public void Q2()
        {
            var q = _queries.Q2();
            var res = ExecuteQuery(q, "LastName", "FirstName");
            Assert.AreEqual(4, res.Count);
            var result = new Dictionary<object, object>();
            result.Add("Buchanan", "Steve");
            result.Add("Suyama", "Michael");
            result.Add("King", "Robert");
            result.Add("DodsWorth", "Anne");
            Assert.AreEqual(res, result);
            //NORTHWND_QA
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

        public Dictionary<object, object> ExecuteQuery(string query, string ordinal, string ordinal2)
        {
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            var objs = new Dictionary<object, object>();
            while (reader.Read())
            {
                objs.Add(reader.GetValue(reader.GetOrdinal(ordinal)), reader.GetOrdinal(ordinal2));
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
