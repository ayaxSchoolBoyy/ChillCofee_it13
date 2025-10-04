using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChillCofee
{
    public partial class CashierOrderForm : UserControl
    {
        public static int getCustID;

        MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;");
        public CashierOrderForm()
        {
            InitializeComponent();

            displayAvailableProds();

            displayAllOrders();

            displayTotalPrice();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayAvailableProds();

            displayAllOrders();

            displayTotalPrice();
        }

        public void displayAvailableProds()
        {
            CashierOrderFormProdData allProds = new CashierOrderFormProdData();

            List<CashierOrderFormProdData> listData = allProds.availableProductsData();

            cashierOrderForm_menuTable.DataSource = listData;
            cashierOrderForm_menuTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void cashierOrderForm_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashierOrderForm_productID.SelectedIndex = -1;
            cashierOrderForm_productID.Items.Clear();
            cashierOrderForm_prodName.Text = "";
            cashierOrderForm_price.Text = "";



            string selectedValue = cashierOrderForm_type.SelectedItem as string;

            if (selectedValue != null)
            {

                try
                {
                    using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                    {
                        connect.Open();
                        string selectData = $"SELECT * FROM products WHERE prod_type = '{selectedValue}' AND prod_status = @status AND date_delete IS NULL";

                        using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["prod_id"].ToString();

                                    cashierOrderForm_productID.Items.Add(value);
                                }
                            }
                        }
                    }
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Error: " + exx, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cashierOrderForm_productID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cashierOrderForm_productID.SelectedItem as string;

            if (selectedValue != null)
            {
                try
                {
                    using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                    {
                        connect.Open();
                        string selectData = $"SELECT * FROM products WHERE prod_id = '{selectedValue}' AND prod_status = @status AND date_delete IS NULL";

                        using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string prodName = reader["prod_name"].ToString();
                                    string prodPrice = reader["prod_price"].ToString();

                                    cashierOrderForm_prodName.Text = prodName;
                                    cashierOrderForm_price.Text = prodPrice;

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public void displayAllOrders()
        {
            cashierOrderForm_orderList.Clear();
            cashierOrderForm_orderList.Font = new Font("Consolas", 10);

            try
            {
                using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();
                    string query = "SELECT prod_name, qty, prod_price FROM orders WHERE transaction_id = @custId";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@custId", idGen);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cashierOrderForm_orderList.AppendText(String.Format("\n"));
                            cashierOrderForm_orderList.AppendText(String.Format("\t{0,-20}{1,-10}{2,-10}\n", "Product", "Qty", "Price"));
                            cashierOrderForm_orderList.AppendText(new string('-', 46) + "\n");

                            while (reader.Read())
                            {
                                string prodName = reader["prod_name"].ToString();
                                string qty = reader["qty"].ToString();
                                string price = reader["prod_price"].ToString();

                                cashierOrderForm_orderList.AppendText(String.Format("\t{0,-20}{1,-10}{2,-10}\n", prodName, qty, "₱" + price));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private float totalPrice;
        public void displayTotalPrice()
        {
            IDGenerator();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    // Multiply qty * price to get correct total
                    string selectData = "SELECT SUM(qty * prod_price) FROM orders WHERE transaction_id = @custId";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@custId", idGen);

                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalPrice = Convert.ToSingle(result);
                            cashierOrderForm_orderPrice.Text = totalPrice.ToString("0.00");
                        }
                        else
                        {
                            cashierOrderForm_orderPrice.Text = "0.00";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }


        private void cashierOrderForm_addBtn_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (cashierOrderForm_type.SelectedIndex == -1 || cashierOrderForm_productID.SelectedIndex == -1
                || cashierOrderForm_prodName.Text == "" || cashierOrderForm_quantity.Text == "0"
                || cashierOrderForm_price.Text == "")
            {
                MessageBox.Show("Please select the product first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        float getPrice = 0;
                        string selectOrder = "SELECT * FROM products WHERE prod_id = @prodID";

                        using (MySqlCommand getOrder = new MySqlCommand(selectOrder, connect))
                        {
                            getOrder.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());

                            using (MySqlDataReader reader = getOrder.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    object rawValue = reader["prod_price"];
                                    if (rawValue != DBNull.Value)
                                    {
                                        getPrice = Convert.ToSingle(rawValue);
                                    }
                                }
                            }
                        }

                        string insertOrder = "INSERT INTO orders (transaction_id, prod_id, prod_name, prod_type, qty, prod_price, order_date) " +
                            "VALUES(@transactionID, @prodID, @prodName, @prodType, @qty, @prodPrice, @orderDate)";

                        DateTime today = DateTime.Today;

                        using (MySqlCommand cmd = new MySqlCommand(insertOrder, connect))
                        {
                            cmd.Parameters.AddWithValue("@transactionID", idGen);
                            cmd.Parameters.AddWithValue("@prodID", cashierOrderForm_productID.Text.Trim());
                            cmd.Parameters.AddWithValue("@prodName", cashierOrderForm_prodName.Text);
                            cmd.Parameters.AddWithValue("@prodType", cashierOrderForm_type.Text.Trim());

                            int qty = int.Parse(cashierOrderForm_quantity.Text);
                            float totalPrice = getPrice * qty;

                            cmd.Parameters.AddWithValue("@qty", cashierOrderForm_quantity.Text);
                            cmd.Parameters.AddWithValue("@prodPrice", totalPrice);
                            cmd.Parameters.AddWithValue("@orderDate", today);

                            cmd.ExecuteNonQuery();


                            displayAllOrders();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
            displayTotalPrice();
        }
        private int idGen = 0;
        public void IDGenerator() 
        {
            using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
            {
                connect.Open();
                string selectID = "SELECT MAX(transaction_id) FROM customers";

                using (MySqlCommand cmd = new MySqlCommand(selectID, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int temp = Convert.ToInt32(result);

                        if (temp == 0)
                        {
                            idGen = 1;
                        }
                        else
                        {
                            idGen = temp + 1;
                        }
                    }
                    else
                    {
                        idGen = 1;
                    }
                    getCustID = idGen;
                }
            }
        }

        private void cashierOrderForm_amount_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(cashierOrderForm_amount.Text, out float getAmount))
            {
                displayTotalPrice(); // make sure totalPrice is fresh
                float getChange = getAmount - totalPrice;

                cashierOrderForm_change.Text = (getChange >= 0) ? getChange.ToString("0.00") : "";
            }
            else
            {
                cashierOrderForm_change.Text = "";
            }
        }

        private void cashierOrderForm_amount_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cashierOrderForm_payBtn_Click(object sender, EventArgs e)
        {
            if (cashierOrderForm_amount.Text == "" || cashierOrderForm_orderTable.Rows.Count < 0)
            {
                MessageBox.Show("Something went wrong.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure for paying?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();

                            IDGenerator();


                            string insertData = "INSERT INTO customers (transaction_id, total_price, amount, `change`, date) " +
                                "VALUES(@custID, @totalprice, @amount, @change, @date)";

                            DateTime today = DateTime.Today;

                            using (MySqlCommand cmd = new MySqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@custID", idGen);
                                cmd.Parameters.AddWithValue("@totalprice", totalPrice);
                                cmd.Parameters.AddWithValue("@amount", cashierOrderForm_amount.Text);
                                cmd.Parameters.AddWithValue("@change", cashierOrderForm_change.Text);
                                cmd.Parameters.AddWithValue("@date", today);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Paid successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
            displayTotalPrice();
        }
        private int rowIndex = 0;
        private void cashierOrderForm_receiptBtn_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            rowIndex = 0;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string header = "Chill Coffee Receipt\n-----------------------------\n";
            e.Graphics.DrawString(header, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 100, 100);

            // Print orders from RichTextBox
            string orderDetails = cashierOrderForm_orderList.Text;
            e.Graphics.DrawString(orderDetails, new Font("Arial", 12), Brushes.Black, new RectangleF(100, 150, e.MarginBounds.Width, e.MarginBounds.Height));

            // ✅ Use totalPrice field (already computed) instead of re-querying DB
            string footer = $"\n-----------------------------\n" +
                            $"Total: ₱{totalPrice.ToString("0.00")}\n" +
                            $"Amount: ₱{cashierOrderForm_amount.Text}\n" +
                            $"Change: ₱{cashierOrderForm_change.Text}\n" +
                            $"{DateTime.Now}";
            e.Graphics.DrawString(footer, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 100, e.MarginBounds.Bottom - 100);
        }

        private void cashierOrderForm_removeBtn_Click(object sender, EventArgs e)
        {
            if (cashierOrderForm_productID.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product to reduce quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string prodID = cashierOrderForm_productID.Text.Trim();

            if (MessageBox.Show("Reduce quantity of this item?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                    {
                        connect.Open();

                        // First check current quantity
                        string checkQtyQuery = "SELECT qty, prod_price FROM orders WHERE transaction_id = @custId AND prod_id = @prodID LIMIT 1";
                        int currentQty = 0;
                        float currentPrice = 0;

                        using (MySqlCommand cmd = new MySqlCommand(checkQtyQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@custId", idGen);
                            cmd.Parameters.AddWithValue("@prodID", prodID);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    currentQty = Convert.ToInt32(reader["qty"]);
                                    currentPrice = Convert.ToSingle(reader["prod_price"]);
                                }
                            }
                        }

                        if (currentQty > 1)
                        {
                            // Get unit price by dividing total price by qty
                            float unitPrice = currentPrice / currentQty;

                            // Reduce quantity and update price
                            string updateQuery = "UPDATE orders SET qty = qty - 1, prod_price = prod_price - @unitPrice " +
                                                 "WHERE transaction_id = @custId AND prod_id = @prodID LIMIT 1";

                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connect))
                            {
                                updateCmd.Parameters.AddWithValue("@custId", idGen);
                                updateCmd.Parameters.AddWithValue("@prodID", prodID);
                                updateCmd.Parameters.AddWithValue("@unitPrice", unitPrice);

                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // If qty = 1, delete the row completely
                            string deleteQuery = "DELETE FROM orders WHERE transaction_id = @custId AND prod_id = @prodID LIMIT 1";

                            using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connect))
                            {
                                deleteCmd.Parameters.AddWithValue("@custId", idGen);
                                deleteCmd.Parameters.AddWithValue("@prodID", prodID);

                                deleteCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Refresh UI
                    displayAllOrders();
                    displayTotalPrice();

                    MessageBox.Show("Quantity updated successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private int getOrderID = 0;




        public void clearFields()
        {
            cashierOrderForm_type.SelectedIndex = -1;
            cashierOrderForm_productID.Items.Clear();
            cashierOrderForm_prodName.Text = "";
            cashierOrderForm_price.Text = "";
            cashierOrderForm_quantity.Text = "0";
        }

        
        private void cashierOrderForm_clearBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(@"Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();

                    // Delete all orders for the current customer
                    string deleteOrders = "DELETE FROM orders WHERE transaction_id = @custId";

                    using (MySqlCommand cmd = new MySqlCommand(deleteOrders, connect))
                    {
                        cmd.Parameters.AddWithValue("@custId", idGen);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Clear UI fields
                cashierOrderForm_orderList.Clear();
                cashierOrderForm_orderPrice.Text = "0.00";
                cashierOrderForm_change.Text = "";
                cashierOrderForm_amount.Text = "";
                quantity = 0;
                clearFields();

                MessageBox.Show("Orders cleared successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int quantity = 0;

        private void add_quanti_Click(object sender, EventArgs e)
        {
            quantity++;
            cashierOrderForm_quantity.Text = quantity.ToString();
        }

        private void minus_quanti_Click(object sender, EventArgs e)
        {
            if (quantity > 0)
                quantity--;
                cashierOrderForm_quantity.Text = quantity.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void CashierOrderForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}
