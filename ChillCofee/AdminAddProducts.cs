using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ChillCofee
{
    public partial class AdminAddProducts : UserControl
    {
        MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;");
        public AdminAddProducts()
        {
            InitializeComponent();

            displayData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayData();
        }

        public bool emptyFields()
        {
            if (adminAddProducts_id.Text == "" || adminAddProducts_name.Text == ""
                || adminAddProducts_type.SelectedIndex == -1 || adminAddProducts_stock.Text == ""
                || adminAddProducts_price.Text == "" || adminAddProducts_status.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void displayData()
        {
            AdminAddProductsData prodData = new AdminAddProductsData();
            List<AdminAddProductsData> listData = prodData.productsListData();

            dataGridView1.DataSource = listData;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }


        private void adminAddProducts_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                        // CHECKING IF THE PRODUCT ID IS EXISTING ALREADY
                        string selectProdID = "SELECT * FROM products WHERE prod_id = @prodID";

                        using (MySqlCommand selectPID = new MySqlCommand(selectProdID, connect))
                        {
                            selectPID.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(selectPID);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Product ID: " + adminAddProducts_id.Text.Trim() + " is taken already", "Error Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO products (prod_id, prod_name, prod_type, " +
                                                    "prod_stock, prod_price, prod_status, prod_image, date_insert) " +
                                                    "VALUES(@prodID, @prodName, @prodType, @prodStock, @prodPrice, @prodStatus, @prodImage, @dateInsert)";



                                DateTime today = DateTime.Today;

                                

                                

                                using (MySqlCommand cmd = new MySqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodImage", imagePath);
                                    cmd.Parameters.AddWithValue("@dateInsert", today);

                                    cmd.ExecuteNonQuery();

                                    clearFields();

                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayData();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed connection: " + ex, "Error Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }

            }
        }
        private void adminAddUsers_importBtn_Click(object sender, EventArgs e)
        {
            
        }

        public void clearFields()
        {
            adminAddProducts_id.Text = "";
            adminAddProducts_name.Text = "";
            adminAddProducts_type.SelectedIndex = -1;
            adminAddProducts_stock.Text = "";
            adminAddProducts_price.Text = "";
            adminAddProducts_status.SelectedIndex = -1;
            adminAddProducts_imageBox.Image = null;
            imagePath = "";


        }

        private void adminAddProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                adminAddProducts_id.Text = row.Cells["ProductID"].Value.ToString();
                adminAddProducts_name.Text = row.Cells["ProductName"].Value.ToString();
                adminAddProducts_type.Text = row.Cells["Type"].Value.ToString();
                adminAddProducts_stock.Text = row.Cells["Stock"].Value.ToString();
                adminAddProducts_price.Text = row.Cells["Price"].Value.ToString();
                adminAddProducts_status.Text = row.Cells["Status"].Value.ToString();

                // ✅ FIX: assign the actual image path to the global variable
                imagePath = row.Cells["ImagePath"].Value?.ToString();

                // ✅ FIX: check if file exists before loading it
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    // Dispose the old image to avoid memory leaks
                    if (adminAddProducts_imageBox.Image != null)
                        adminAddProducts_imageBox.Image.Dispose();

                    adminAddProducts_imageBox.Image = Image.FromFile(imagePath);
                }
                else
                {
                    adminAddProducts_imageBox.Image = null;
                }
            }
        }


        private void adminAddProducts_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are requried to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Update Product ID: " + adminAddProducts_id.Text.Trim() + "?"
                    , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();

                            string updateData = "UPDATE products SET prod_name = @prodName, prod_type = @prodType, prod_stock = @prodStock, prod_price = @prodPrice, prod_status = @prodStatus, prod_image = @prodImage, date_update = @dateUpdate WHERE prod_id = @prodID";


                            DateTime today = DateTime.Today;

                            using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                updateD.Parameters.AddWithValue("@dateUpdate", today);
                                updateD.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodImage", imagePath);
                                updateD.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayData();

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
        }

        private void adminAddProducts_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are requried to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Delete Product ID: " + adminAddProducts_id.Text.Trim() + "?"
                    , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();

                            string updateData = "UPDATE products SET date_delete = @dateDelete WHERE prod_id = @prodID";
                            DateTime today = DateTime.Today;

                            using (MySqlCommand updateD = new MySqlCommand(updateData, connect))
                            {

                                updateD.Parameters.AddWithValue("@dateDelete", today);
                                updateD.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                                updateD.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Removed successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayData();

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
        }
        string imagePath = "";

        private void adminAddProducts_importBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagePath = ofd.FileName;
                    adminAddProducts_imageBox.Image = Image.FromFile(imagePath);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
