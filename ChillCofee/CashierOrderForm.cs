using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
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
        private List<(string Product, int Qty, float Total)> printItems = new List<(string, int, float)>();
        private float printSubtotal = 0;
        private float printCash = 0;
        private float printChange = 0;

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

        public void displayAvailableProds(string category = "All")
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();
                    string query = "SELECT prod_id, prod_name, prod_type, prod_price, prod_image FROM products WHERE prod_status = 'Available' AND date_delete IS NULL";

                    if (category != "All")
                    {
                        query += " AND prod_type = @type";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        if (category != "All")
                        {
                            cmd.Parameters.AddWithValue("@type", category);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string prodID = reader["prod_id"].ToString();
                                string prodName = reader["prod_name"].ToString();
                                string prodType = reader["prod_type"].ToString();
                                string prodPrice = reader["prod_price"].ToString();
                                string imagePath = reader["prod_image"].ToString();

                                Panel card = new Panel
                                {
                                    Width = 150,
                                    Height = 180,
                                    BackColor = Color.WhiteSmoke,
                                    BorderStyle = BorderStyle.FixedSingle,
                                    Margin = new Padding(10),
                                    Tag = prodID
                                };

                                PictureBox pic = new PictureBox
                                {
                                    Dock = DockStyle.Top,
                                    Height = 120,
                                    SizeMode = PictureBoxSizeMode.Zoom,
                                    Cursor = Cursors.Hand,
                                    Tag = prodID
                                };

                                try
                                {
                                    if (System.IO.File.Exists(imagePath))
                                        pic.Image = Image.FromFile(imagePath);
                                }
                                catch { }

                                Label lbl = new Label
                                {
                                    Dock = DockStyle.Fill,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                    Text = $"{prodName}\n₱{prodPrice}"
                                };

                                pic.Click += Product_Click;
                                card.Controls.Add(lbl);
                                card.Controls.Add(pic);
                                flowLayoutPanel1.Controls.Add(card);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void Product_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            string prodID = pic.Tag.ToString();

            try
            {
                using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                {
                    connect.Open();

                    // Get product info
                    string query = "SELECT * FROM products WHERE prod_id = @prodID";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@prodID", prodID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read()) return;

                            string name = reader["prod_name"].ToString();
                            string type = reader["prod_type"].ToString();
                            float price = Convert.ToSingle(reader["prod_price"]);
                            reader.Close();

                            IDGenerator();

                            // Check if product already exists in order
                            string checkOrder = "SELECT qty FROM orders WHERE transaction_id = @custId AND prod_id = @prodID";
                            using (MySqlCommand check = new MySqlCommand(checkOrder, connect))
                            {
                                check.Parameters.AddWithValue("@custId", idGen);
                                check.Parameters.AddWithValue("@prodID", prodID);
                                object result = check.ExecuteScalar();

                                bool removeMode = checkBox1.Checked;

                                if (!removeMode)
                                {
                                    // 🟩 Normal Mode: Add item or increase quantity
                                    if (result != null)
                                    {
                                        string update = "UPDATE orders SET qty = qty + 1 WHERE transaction_id = @custId AND prod_id = @prodID";
                                        using (MySqlCommand updateCmd = new MySqlCommand(update, connect))
                                        {
                                            updateCmd.Parameters.AddWithValue("@custId", idGen);
                                            updateCmd.Parameters.AddWithValue("@prodID", prodID);
                                            updateCmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        string insert = "INSERT INTO orders (transaction_id, prod_id, prod_name, prod_type, qty, prod_price, order_date) " +
                                                        "VALUES (@custId, @prodID, @name, @type, 1, @price, @date)";
                                        using (MySqlCommand insertCmd = new MySqlCommand(insert, connect))
                                        {
                                            insertCmd.Parameters.AddWithValue("@custId", idGen);
                                            insertCmd.Parameters.AddWithValue("@prodID", prodID);
                                            insertCmd.Parameters.AddWithValue("@name", name);
                                            insertCmd.Parameters.AddWithValue("@type", type);
                                            insertCmd.Parameters.AddWithValue("@price", price);
                                            insertCmd.Parameters.AddWithValue("@date", DateTime.Today);
                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    // 🟥 Remove Mode: Reduce quantity or delete if qty = 1
                                    if (result != null)
                                    {
                                        int currentQty = Convert.ToInt32(result);

                                        if (currentQty > 1)
                                        {
                                            string update = "UPDATE orders SET qty = qty - 1 WHERE transaction_id = @custId AND prod_id = @prodID";
                                            using (MySqlCommand updateCmd = new MySqlCommand(update, connect))
                                            {
                                                updateCmd.Parameters.AddWithValue("@custId", idGen);
                                                updateCmd.Parameters.AddWithValue("@prodID", prodID);
                                                updateCmd.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            string delete = "DELETE FROM orders WHERE transaction_id = @custId AND prod_id = @prodID";
                                            using (MySqlCommand deleteCmd = new MySqlCommand(delete, connect))
                                            {
                                                deleteCmd.Parameters.AddWithValue("@custId", idGen);
                                                deleteCmd.Parameters.AddWithValue("@prodID", prodID);
                                                deleteCmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("This product is not in the order list yet.", "Remove Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            // Refresh UI
                            displayAllOrders();
                            displayTotalPrice();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
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
            cashierOrderForm_orderList.Font = new Font("Consolas", 12);

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
                            cashierOrderForm_orderList.AppendText(String.Format("\t{0,-12}{1,-10}{2,-10}\n", "Product", "Qty", "Price"));
                            cashierOrderForm_orderList.AppendText(new string('-', 42) + "\n");

                            while (reader.Read())
                            {
                                string prodName = reader["prod_name"].ToString();
                                string qty = reader["qty"].ToString();
                                string price = reader["prod_price"].ToString();
                                float unit = float.Parse(price);
                                int q = int.Parse(qty);
                                float totalLine = unit * q;

                                cashierOrderForm_orderList.AppendText(String.Format("\t{0,-20}{1,-10}{2,-10}\n", prodName, qty, "₱" + totalLine.ToString("0.00")));

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
            if (string.IsNullOrWhiteSpace(cashierOrderForm_amount.Text))
            {
                MessageBox.Show("Please enter payment amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Confirm payment?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;"))
                    {
                        connect.Open();
                        IDGenerator();

                        // 1️⃣ Insert payment record
                        string insertCustomer = "INSERT INTO customers (transaction_id, total_price, amount, `change`, date) " +
                                                "VALUES(@custID, @totalprice, @amount, @change, @date)";
                        using (MySqlCommand cmd = new MySqlCommand(insertCustomer, connect))
                        {
                            cmd.Parameters.AddWithValue("@custID", idGen);
                            cmd.Parameters.AddWithValue("@totalprice", totalPrice);
                            cmd.Parameters.AddWithValue("@amount", cashierOrderForm_amount.Text);
                            cmd.Parameters.AddWithValue("@change", cashierOrderForm_change.Text);
                            cmd.Parameters.AddWithValue("@date", DateTime.Today);
                            cmd.ExecuteNonQuery();
                        }

                        // 2️⃣ Deduct stock + Capture data for receipt (ONLY ONCE)
                        string SelectOrders = "SELECT prod_id, prod_name, qty, prod_price FROM orders WHERE transaction_id = @custID";
                        printItems.Clear();

                        using (MySqlCommand cmdOrders = new MySqlCommand(SelectOrders, connect))
                        {
                            cmdOrders.Parameters.AddWithValue("@custID", idGen);
                            using (MySqlDataReader reader = cmdOrders.ExecuteReader())
                            {
                                List<(string prodId, int qty)> orderItems = new List<(string, int)>();

                                while (reader.Read())
                                {
                                    string prodId = reader["prod_id"].ToString();
                                    string prodName = reader["prod_name"].ToString();
                                    int qty = Convert.ToInt32(reader["qty"]);
                                    float price = Convert.ToSingle(reader["prod_price"]);
                                    float totalLine = price * qty;

                                    printItems.Add((prodName, qty, totalLine));
                                    orderItems.Add((prodId, qty));
                                }
                                reader.Close();

                                // Deduct stock
                                foreach (var item in orderItems)
                                {
                                    string deductQuery = "UPDATE products SET prod_stock = prod_stock - @qty WHERE prod_id = @id";
                                    using (MySqlCommand deductCmd = new MySqlCommand(deductQuery, connect))
                                    {
                                        deductCmd.Parameters.AddWithValue("@qty", item.qty);
                                        deductCmd.Parameters.AddWithValue("@id", item.prodId);
                                        deductCmd.ExecuteNonQuery();
                                    }

                                    // Check stock level
                                    string checkStockQuery = "SELECT prod_stock FROM products WHERE prod_id = @id";
                                    using (MySqlCommand checkCmd = new MySqlCommand(checkStockQuery, connect))
                                    {
                                        checkCmd.Parameters.AddWithValue("@id", item.prodId);
                                        object stockResult = checkCmd.ExecuteScalar();

                                        if (stockResult != null && Convert.ToInt32(stockResult) <= 0)
                                        {
                                            string updateStatusQuery = "UPDATE products SET prod_status = 'Unavailable' WHERE prod_id = @id";
                                            using (MySqlCommand statusCmd = new MySqlCommand(updateStatusQuery, connect))
                                            {
                                                statusCmd.Parameters.AddWithValue("@id", item.prodId);
                                                statusCmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        // 3️⃣ Save totals for receipt
                        printSubtotal = totalPrice;
                        float.TryParse(cashierOrderForm_amount.Text, out printCash);
                        float.TryParse(cashierOrderForm_change.Text, out printChange);

                        MessageBox.Show("Payment completed and stock updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 4️⃣ Refresh UI
                        cashierOrderForm_orderList.Clear();
                        cashierOrderForm_orderPrice.Text = "0.00";
                        cashierOrderForm_change.Text = "";
                        cashierOrderForm_amount.Text = "";
                        displayAvailableProds();
                        displayAllOrders();
                        displayTotalPrice();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 🧾 Print receipt
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
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
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 285, 600);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 🎨 Fonts & Styles
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 8, FontStyle.Regular);
            Font listFont = new Font("Consolas", 9, FontStyle.Regular);
            Font boldFont = new Font("Consolas", 9, FontStyle.Bold);
            Font footerFont = new Font("Arial", 8, FontStyle.Italic);
            Brush brush = Brushes.Black;

            // 🧾 Layout setup
            float y = 10;
            float pageWidth = e.PageBounds.Width;
            StringFormat center = new StringFormat() { Alignment = StringAlignment.Center };

            // 🏪 Header
            e.Graphics.DrawString("Chill Coffee", headerFont, brush, pageWidth / 2, y, center);
            y += 18;
            e.Graphics.DrawString("123 Arellano St., Tagum City", subHeaderFont, brush, pageWidth / 2, y, center);
            y += 12;
            e.Graphics.DrawString("Tel: (123) 456-7890", subHeaderFont, brush, pageWidth / 2, y, center);
            y += 18;
            e.Graphics.DrawString("----- RECEIPT -----", subHeaderFont, brush, pageWidth / 2, y, center);
            y += 25;

            // 🧍 Transaction Info
            e.Graphics.DrawString($"Transaction ID: {getCustID}", subHeaderFont, brush, 10, y);
            y += 12;
            e.Graphics.DrawString($"Date: {DateTime.Now:MM/dd/yyyy hh:mm tt}", subHeaderFont, brush, 10, y);
            y += 20;

            // 🛒 Product List Header
            e.Graphics.DrawString("------------------------------------------", listFont, brush, 10, y);
            y += 15;
            e.Graphics.DrawString(String.Format("{0,-14}{1,4}{2,10}", "Product", "Qty", "Price"), boldFont, brush, 10, y);
            y += 15;
            e.Graphics.DrawString("------------------------------------------", listFont, brush, 10, y);
            y += 20;

            // 📦 Product Items (from stored data)
            foreach (var item in printItems)
            {
                e.Graphics.DrawString(String.Format("{0,-14}{1,4}{2,10}", item.Product, item.Qty, "₱" + item.Total.ToString("0.00")), listFont, brush, 10, y);
                y += 15;
            }

            // 🧾 Footer (Totals)
            y += 10;
            e.Graphics.DrawString("------------------------------------------", listFont, brush, 10, y);
            y += 20;

            e.Graphics.DrawString($"Subtotal: ₱{printSubtotal:0.00}", listFont, brush, 10, y);
            y += 15;
            e.Graphics.DrawString($"Cash: ₱{printCash:0.00}", listFont, brush, 10, y);
            y += 15;
            e.Graphics.DrawString($"Change: ₱{printChange:0.00}", listFont, brush, 10, y);
            y += 20;
            e.Graphics.DrawString("------------------------------------------", listFont, brush, 10, y);
            y += 25;


            // 💬 Thank You Message
            e.Graphics.DrawString("Thank you for your purchase!", footerFont, brush, pageWidth / 2, y, center);
            y += 15;
            e.Graphics.DrawString("Please come again!", footerFont, brush, pageWidth / 2, y, center);
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            displayAvailableProds("All");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayAvailableProds("Drinks");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayAvailableProds("Meal");
        }
    }
    
}
