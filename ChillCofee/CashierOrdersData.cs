﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillCofee
{
    internal class CashierOrdersData
    {
        MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;");

        //public int ID { set; get; }
        public int CID { set; get; }
        public string ProdID { set; get; }
        public string ProdName { set; get; }
        public string ProdType { set; get; }
        public int Qty { set; get; }
        public string Price { set; get; }

        public List<CashierOrdersData> ordersListData()
        {
            List<CashierOrdersData> listData = new List<CashierOrdersData>();
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    int custID = 0;
                    string selectCustData = "SELECT MAX(transaction_id) FROM orders";

                    using (MySqlCommand getCustData = new MySqlCommand(selectCustData, connect))
                    {
                        object result = getCustData.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            int temp = Convert.ToInt32(result);

                            if (temp == 0)
                            {
                                custID = 1;
                            }
                            else
                            {
                                custID = temp;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error ID");
                        }
                    }

                    string selectOrders = "SELECT * FROM orders WHERE transaction_id = @customerID";

                    using (MySqlCommand cmd = new MySqlCommand(selectOrders, connect))
                    {
                        cmd.Parameters.AddWithValue("@customerID", custID);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CashierOrdersData coData = new CashierOrdersData();

                            //coData.ID = (int)reader["id"];
                            coData.CID = (int)reader["transaction_id"];
                            coData.ProdID = reader["prod_id"].ToString();
                            coData.ProdName = reader["prod_name"].ToString();
                            coData.ProdType = reader["prod_type"].ToString();
                            coData.Qty = (int)reader["qty"];
                            coData.Price = reader["prod_price"].ToString();

                            listData.Add(coData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }
}

