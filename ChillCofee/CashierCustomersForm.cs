using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ChillCofee
{
    public partial class CashierCustomersForm : UserControl
    {
        public CashierCustomersForm()
        {
            InitializeComponent();

            displayCustomersData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCustomersData();
        }

        public void displayCustomersData()
        {
            CustomersData cData = new CustomersData();

            List<CustomersData> listData = cData.allCustomersData();

            datagridview1.DataSource = listData;

            datagridview1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (datagridview1.Columns.Contains("TransactionID"))
            {
                datagridview1.Columns["TransactionID"].Visible = false;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            
        }

        private void clearFilterButton_Click(object sender, EventArgs e)
        {
            LoadAllTransactions();
        }

        private void LoadTransactionsByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();
                    string query = "SELECT transaction_id, total_price, amount, `change`, date " +
                                   "FROM customers " +
                                   "WHERE date BETWEEN @startDate AND @endDate " +
                                   "ORDER BY transaction_id ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            datagridview1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllTransactions()
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();
                    string query = "SELECT transaction_id, total_price, amount, `change`, date FROM customers ORDER BY  transaction_id ASC";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            datagridview1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void adminAddProducts_addBtn_Click(object sender, EventArgs e)
        {

        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // full day range
            LoadTransactionsByDate(startDate, endDate);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all transactions? This action cannot be undone.",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                    {
                        connect.Open();

                        // 🧹 Delete all records from customers table
                        string deleteCustomers = "DELETE FROM customers";
                        using (MySqlCommand cmd = new MySqlCommand(deleteCustomers, connect))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // (Optional) If you also want to clear orders table:
                        string deleteOrders = "DELETE FROM orders";
                        using (MySqlCommand cmd2 = new MySqlCommand(deleteOrders, connect))
                        {
                           cmd2.ExecuteNonQuery();
                        }

                        MessageBox.Show("All transactions have been cleared successfully.",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 🔄 Refresh the DataGridView after deleting
                        LoadAllTransactions();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing transactions: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
