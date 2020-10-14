using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace MyAssignment2
{
    public class DataOperation<T> where T:Entity
    {
        private readonly string _connectionString;

        public DataOperation(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Insert(T item)
        {
            Type t1 = item.GetType();

            PropertyInfo prop = t1.GetProperty("HouseName");
            PropertyInfo prop1 = t1.GetProperty("Rooms");

            object list = prop.GetValue(item);
            object obj = prop1.GetValue(item);

            List<Room> myModel = (List<Room>)obj;

            string query = "";
            for (int i = 0; i < myModel.Count; i++)
            {
                int tt = myModel[i].Id;
                query = @"INSERT INTO House ([HouseName], [RoomId])  
                             VALUES ('" + list + "', '" + tt + "')";
                ExecuteNonQuery(query);
            }


            return query;

        }

        public string Update(T item)
        {
            Type t1 = item.GetType();

            PropertyInfo prop = t1.GetProperty("Id");
            PropertyInfo prop1 = t1.GetProperty("HouseName");

            object list = prop.GetValue(item);
            object list1 = prop1.GetValue(item);

            string query = "Update House set HouseName = '" + list1 + "' where Id = '" + list + "'";
            ExecuteNonQuery(query);
            return query;
        }

        public string Delete(T item)
        {
            Type t1 = item.GetType();

            PropertyInfo prop = t1.GetProperty("Id");

            object list = prop.GetValue(item);

            string query = "Delete From House where Id = '" + list + "'";
            ExecuteNonQuery(query);
            return query;
        }

        public List<House> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM House";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader Reader = cmd.ExecuteReader();

                List<House> aList = new List<House>();
                House aHouse = null;

                while (Reader.Read())
                {
                    aHouse = new House();
                    aHouse.Id = (int)Reader["Id"];
                    aHouse.HouseName = Reader["HouseName"].ToString();

                    aList.Add(aHouse);
                }
                Reader.Close();
                cmd.ExecuteNonQuery();
                conn.Close();
                return aList;

            }
        }

        public House GetById(T item)
        {
            Type t1 = item.GetType();

            PropertyInfo prop = t1.GetProperty("Id");

            object list = prop.GetValue(item);

            using (var conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM House where Id = '" + list + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader Reader = cmd.ExecuteReader();

                List<House> aaList = new List<House>();
                House aHouse = null;

                while (Reader.Read())
                {
                    aHouse = new House();
                    aHouse.Id = (int)Reader["Id"];
                    aHouse.HouseName = Reader["HouseName"].ToString();
                    aaList.Add(aHouse);
                }
                Reader.Close();
                cmd.ExecuteNonQuery();
                conn.Close();

                return aHouse;

            }
        }

        public void ExecuteNonQuery(string sql)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return;
        }
    }
}
