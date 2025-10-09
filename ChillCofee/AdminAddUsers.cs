﻿using System;
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
    public partial class AdminAddUsers : UserControl
    {
        MySqlConnection connect = new MySqlConnection("Server=localhost;Database=chillcoffee;Uid=root;Pwd=;");
        public AdminAddUsers()
        {
            InitializeComponent();

            displayAddUsersData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayAddUsersData();
        }

        public void displayAddUsersData()
        {
            AdminAddUsersData usersData = new AdminAddUsersData();
            List<AdminAddUsersData> listData = usersData.usersListData();

            dataGridView1.DataSource = listData;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Columns.Contains("id"))
            {
                dataGridView1.Columns["id"].Visible = false;
            }
            
        }
        public bool emptyFields()
        {
            if (adminAddUsers_username.Text == "" || adminAddUsers_password.Text == ""
                || adminAddUsers_role.Text == "" || adminAddUsers_status.Text == "" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void adminAddUsers_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                       
                        string selectUsern = "SELECT * FROM users WHERE username = @usern";

                        using (MySqlCommand checkUsern = new MySqlCommand(selectUsern, connect))
                        {
                            checkUsern.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(checkUsern);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string usern = adminAddUsers_username.Text.Substring(0, 1).ToUpper() + adminAddUsers_username.Text.Substring(1);
                                MessageBox.Show(usern + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                string insertData = "INSERT INTO users (username, password, role, status, date_rag) " +
                                        "VALUES(@usern, @pass, @role, @status, @date)";

                                DateTime today = DateTime.Now;

                                

                                using (MySqlCommand cmd = new MySqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                    
                                    cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                    cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", today);

                                    cmd.ExecuteNonQuery();
                                    clearFields();
                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    displayAddUsersData();
                                }
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
        }
 
        private void adminAddUsers_importBtn_Click(object sender, EventArgs e)
        {
            
        }

        private int id = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            id = (int)row.Cells[0].Value;
            adminAddUsers_username.Text = row.Cells[1].Value.ToString();
            adminAddUsers_password.Text = row.Cells[2].Value.ToString();
            adminAddUsers_role.Text = row.Cells[3].Value.ToString();
            adminAddUsers_status.Text = row.Cells[4].Value.ToString();

            

        }

        private void adminAddUsers_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Update Username: " + adminAddUsers_username.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();

                            

                            string updateData = "UPDATE users SET username = @usern, password = @pass, role = @role, status = @status WHERE id = @id";


                            using (MySqlCommand cmd = new MySqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                cmd.Parameters.AddWithValue("@id", id);

                                

                                cmd.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayAddUsersData();
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

        public void clearFields()
        {
            adminAddUsers_username.Text = "";
            adminAddUsers_password.Text = "";
            adminAddUsers_role.SelectedIndex = -1;
            adminAddUsers_status.SelectedIndex = -1;
            
        }

        private void adminAddUsers_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void adminAddUsers_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Delete Username: " + adminAddUsers_username.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();

                            string deleteData = "DELETE FROM users WHERE id = @id";

                            using (MySqlCommand cmd = new MySqlCommand(deleteData, connect))
                            {

                                cmd.Parameters.AddWithValue("@id", id);

                                cmd.ExecuteNonQuery();

                                clearFields();

                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayAddUsersData();
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

        private void AdminAddUsers_Load(object sender, EventArgs e)
        {

        }
    }
}
