using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChillCofee
{
    public partial class CashierMainForm : Form
    {
        // Remove duplicate field declarations for adminDashboardForm1, adminAddProducts1, cashierCustomersForm1
        // Only keep the ones auto-generated in Designer.cs
        // If you need to reference cashierOrderForm1, declare it here:
        private CashierOrderForm cashierOrderForm1 = new CashierOrderForm();
        private string loggedInUser;
        public CashierMainForm(string usernamelbl)
        {
            InitializeComponent();
            // Add the cashierOrderForm1 to panel2 and set its initial visibility
            cashierOrderForm1.Location = new Point(0, 0);
            cashierOrderForm1.Size = new Size(1240, 739);
            cashierOrderForm1.Visible = false;
            panel2.Controls.Add(cashierOrderForm1);
            loggedInUser = usernamelbl;
            label4.Text = loggedInUser;
            this.WindowState = FormWindowState.Maximized;
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(check == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to sign out?" , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = true;
           
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AdminDashboardForm adForm = adminDashboardForm1 as AdminDashboardForm;
            

            if (adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
           
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;

            if (coForm != null)
            {
                coForm.refreshData();
                coForm.displayAvailableProds("All");
            }
        }

        private void order_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;

            if (coForm != null)
            {
                coForm.refreshData();
                coForm.displayAvailableProds("Meal");
            }
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
           
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;

            if (coForm != null)
            {
                coForm.refreshData();
                coForm.displayAvailableProds("Drinks");
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminDashboardForm1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = true;

            
            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;

            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }

        private void username_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
