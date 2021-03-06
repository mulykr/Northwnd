﻿using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwnd;
using System.Linq;
using System;

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

        [TestMethod]
        public void Q2()
        {
            var q = _queries.Q2();
            var res = ExecuteQuery(q, "LastName", "FirstName");
            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(res["Buchanan"], "Steven");
            Assert.AreEqual(res["Suyama"], "Michael");
            Assert.AreEqual(res["King"], "Robert");
            Assert.AreEqual(res["Dodsworth"], "Anne");
        }

        [TestMethod]
        public void Q3()
        {
            var q = _queries.Q3();
            var res = ExecuteQuery(q, "LastName", "FirstName");
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(res["Fuller"], "Andrew");
            Assert.AreEqual(res["Dodsworth"], "Anne");
           
        }

        [TestMethod]
        public void Q5()
        {
            var q = _queries.Q5();
            var command = new SqlCommand(q, _connection);
            var res = command.ExecuteScalar();
            Assert.AreEqual(4, res);

        }

        [TestMethod]
        public void Q10()
        {
            var q = _queries.Q10();
            var res = ExecuteQuery(q, "LastName", "FirstName");
            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(res["Leverling"], "Janet");
            Assert.AreEqual(res["Dodsworth"], "Anne");
            Assert.AreEqual(res["Suyama"], "Michael");
        }

        [TestMethod]
        public void Q11()
        {
            var q = _queries.Q11();
            var res = ExecuteQuery(q, "City" );
            Assert.AreEqual(5, res.Count);
            Assert.AreEqual("Kirkland",res[0]);
            Assert.AreEqual("London", res[1]);
            Assert.AreEqual("Redmond", res[2]);
            Assert.AreEqual("Seattle", res[3]);
            Assert.AreEqual("Tacoma", res[4]);
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
       
        public Dictionary<object,object> ExecuteQuery(string query, string ordinal, string ordinal2)
        {
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            var objs = new Dictionary<object,object>();
            while (reader.Read())
            {
                objs.Add(reader.GetValue(reader.GetOrdinal(ordinal)), reader.GetValue(reader.GetOrdinal(ordinal2)));
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
