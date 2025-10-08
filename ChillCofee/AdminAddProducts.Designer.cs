namespace ChillCofee
{
    partial class AdminAddProducts
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.adminAddProducts_importBtn = new System.Windows.Forms.Button();
            this.adminAddProducts_imageBox = new System.Windows.Forms.PictureBox();
            this.adminAddProducts_clearBtn = new System.Windows.Forms.Button();
            this.adminAddProducts_deleteBtn = new System.Windows.Forms.Button();
            this.adminAddProducts_updateBtn = new System.Windows.Forms.Button();
            this.adminAddProducts_addBtn = new System.Windows.Forms.Button();
            this.adminAddProducts_status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.adminAddProducts_price = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.adminAddProducts_stock = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.adminAddProducts_type = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.adminAddProducts_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.adminAddProducts_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminAddProducts_imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(15, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1574, 526);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data of Products";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(199)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(199)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(199)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(14, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1545, 426);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.adminAddProducts_importBtn);
            this.panel2.Controls.Add(this.adminAddProducts_imageBox);
            this.panel2.Controls.Add(this.adminAddProducts_clearBtn);
            this.panel2.Controls.Add(this.adminAddProducts_deleteBtn);
            this.panel2.Controls.Add(this.adminAddProducts_updateBtn);
            this.panel2.Controls.Add(this.adminAddProducts_addBtn);
            this.panel2.Controls.Add(this.adminAddProducts_status);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.adminAddProducts_price);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.adminAddProducts_stock);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.adminAddProducts_type);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.adminAddProducts_name);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.adminAddProducts_id);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(15, 593);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1574, 373);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // adminAddProducts_importBtn
            // 
            this.adminAddProducts_importBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(82)))));
            this.adminAddProducts_importBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminAddProducts_importBtn.FlatAppearance.BorderSize = 0;
            this.adminAddProducts_importBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.adminAddProducts_importBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.adminAddProducts_importBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminAddProducts_importBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_importBtn.ForeColor = System.Drawing.Color.White;
            this.adminAddProducts_importBtn.Location = new System.Drawing.Point(1238, 271);
            this.adminAddProducts_importBtn.Name = "adminAddProducts_importBtn";
            this.adminAddProducts_importBtn.Size = new System.Drawing.Size(175, 50);
            this.adminAddProducts_importBtn.TabIndex = 25;
            this.adminAddProducts_importBtn.Text = "Import Image";
            this.adminAddProducts_importBtn.UseVisualStyleBackColor = false;
            this.adminAddProducts_importBtn.Click += new System.EventHandler(this.adminAddProducts_importBtn_Click);
            // 
            // adminAddProducts_imageBox
            // 
            this.adminAddProducts_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adminAddProducts_imageBox.Location = new System.Drawing.Point(1227, 61);
            this.adminAddProducts_imageBox.Name = "adminAddProducts_imageBox";
            this.adminAddProducts_imageBox.Size = new System.Drawing.Size(195, 187);
            this.adminAddProducts_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.adminAddProducts_imageBox.TabIndex = 24;
            this.adminAddProducts_imageBox.TabStop = false;
            this.adminAddProducts_imageBox.Click += new System.EventHandler(this.adminAddProducts_imageBox_Click);
            // 
            // adminAddProducts_clearBtn
            // 
            this.adminAddProducts_clearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(82)))));
            this.adminAddProducts_clearBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminAddProducts_clearBtn.FlatAppearance.BorderSize = 0;
            this.adminAddProducts_clearBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.adminAddProducts_clearBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.adminAddProducts_clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminAddProducts_clearBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_clearBtn.ForeColor = System.Drawing.Color.White;
            this.adminAddProducts_clearBtn.Location = new System.Drawing.Point(726, 272);
            this.adminAddProducts_clearBtn.Name = "adminAddProducts_clearBtn";
            this.adminAddProducts_clearBtn.Size = new System.Drawing.Size(136, 50);
            this.adminAddProducts_clearBtn.TabIndex = 23;
            this.adminAddProducts_clearBtn.Text = "CLEAR";
            this.adminAddProducts_clearBtn.UseVisualStyleBackColor = false;
            this.adminAddProducts_clearBtn.Click += new System.EventHandler(this.adminAddProducts_clearBtn_Click);
            // 
            // adminAddProducts_deleteBtn
            // 
            this.adminAddProducts_deleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(82)))));
            this.adminAddProducts_deleteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminAddProducts_deleteBtn.FlatAppearance.BorderSize = 0;
            this.adminAddProducts_deleteBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.adminAddProducts_deleteBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.adminAddProducts_deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminAddProducts_deleteBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_deleteBtn.ForeColor = System.Drawing.Color.White;
            this.adminAddProducts_deleteBtn.Location = new System.Drawing.Point(570, 272);
            this.adminAddProducts_deleteBtn.Name = "adminAddProducts_deleteBtn";
            this.adminAddProducts_deleteBtn.Size = new System.Drawing.Size(136, 50);
            this.adminAddProducts_deleteBtn.TabIndex = 22;
            this.adminAddProducts_deleteBtn.Text = "DELETE";
            this.adminAddProducts_deleteBtn.UseVisualStyleBackColor = false;
            this.adminAddProducts_deleteBtn.Click += new System.EventHandler(this.adminAddProducts_deleteBtn_Click);
            // 
            // adminAddProducts_updateBtn
            // 
            this.adminAddProducts_updateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(82)))));
            this.adminAddProducts_updateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminAddProducts_updateBtn.FlatAppearance.BorderSize = 0;
            this.adminAddProducts_updateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.adminAddProducts_updateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.adminAddProducts_updateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminAddProducts_updateBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_updateBtn.ForeColor = System.Drawing.Color.White;
            this.adminAddProducts_updateBtn.Location = new System.Drawing.Point(406, 272);
            this.adminAddProducts_updateBtn.Name = "adminAddProducts_updateBtn";
            this.adminAddProducts_updateBtn.Size = new System.Drawing.Size(136, 50);
            this.adminAddProducts_updateBtn.TabIndex = 21;
            this.adminAddProducts_updateBtn.Text = "UPDATE";
            this.adminAddProducts_updateBtn.UseVisualStyleBackColor = false;
            this.adminAddProducts_updateBtn.Click += new System.EventHandler(this.adminAddProducts_updateBtn_Click);
            // 
            // adminAddProducts_addBtn
            // 
            this.adminAddProducts_addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(82)))));
            this.adminAddProducts_addBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminAddProducts_addBtn.FlatAppearance.BorderSize = 0;
            this.adminAddProducts_addBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.adminAddProducts_addBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(53)))));
            this.adminAddProducts_addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminAddProducts_addBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_addBtn.ForeColor = System.Drawing.Color.White;
            this.adminAddProducts_addBtn.Location = new System.Drawing.Point(234, 272);
            this.adminAddProducts_addBtn.Name = "adminAddProducts_addBtn";
            this.adminAddProducts_addBtn.Size = new System.Drawing.Size(136, 50);
            this.adminAddProducts_addBtn.TabIndex = 20;
            this.adminAddProducts_addBtn.Text = "ADD";
            this.adminAddProducts_addBtn.UseVisualStyleBackColor = false;
            this.adminAddProducts_addBtn.Click += new System.EventHandler(this.adminAddProducts_addBtn_Click);
            // 
            // adminAddProducts_status
            // 
            this.adminAddProducts_status.Font = new System.Drawing.Font("Segoe Fluent Icons", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_status.FormattingEnabled = true;
            this.adminAddProducts_status.Items.AddRange(new object[] {
            "Available",
            "Unavailable"});
            this.adminAddProducts_status.Location = new System.Drawing.Point(697, 170);
            this.adminAddProducts_status.Name = "adminAddProducts_status";
            this.adminAddProducts_status.Size = new System.Drawing.Size(206, 35);
            this.adminAddProducts_status.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(607, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 28);
            this.label4.TabIndex = 17;
            this.label4.Text = "Status:";
            // 
            // adminAddProducts_price
            // 
            this.adminAddProducts_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adminAddProducts_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_price.Location = new System.Drawing.Point(696, 123);
            this.adminAddProducts_price.Name = "adminAddProducts_price";
            this.adminAddProducts_price.Size = new System.Drawing.Size(206, 31);
            this.adminAddProducts_price.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(613, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 28);
            this.label6.TabIndex = 15;
            this.label6.Text = "Price:";
            // 
            // adminAddProducts_stock
            // 
            this.adminAddProducts_stock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adminAddProducts_stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_stock.Location = new System.Drawing.Point(696, 74);
            this.adminAddProducts_stock.Name = "adminAddProducts_stock";
            this.adminAddProducts_stock.Size = new System.Drawing.Size(206, 31);
            this.adminAddProducts_stock.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(611, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 28);
            this.label7.TabIndex = 13;
            this.label7.Text = "Stock:";
            // 
            // adminAddProducts_type
            // 
            this.adminAddProducts_type.Font = new System.Drawing.Font("Segoe Fluent Icons", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_type.FormattingEnabled = true;
            this.adminAddProducts_type.Items.AddRange(new object[] {
            "Meal",
            "Drinks"});
            this.adminAddProducts_type.Location = new System.Drawing.Point(321, 169);
            this.adminAddProducts_type.Name = "adminAddProducts_type";
            this.adminAddProducts_type.Size = new System.Drawing.Size(206, 35);
            this.adminAddProducts_type.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(246, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type:";
            // 
            // adminAddProducts_name
            // 
            this.adminAddProducts_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adminAddProducts_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_name.Location = new System.Drawing.Point(321, 121);
            this.adminAddProducts_name.Name = "adminAddProducts_name";
            this.adminAddProducts_name.Size = new System.Drawing.Size(206, 31);
            this.adminAddProducts_name.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Product Name:";
            // 
            // adminAddProducts_id
            // 
            this.adminAddProducts_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adminAddProducts_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminAddProducts_id.Location = new System.Drawing.Point(321, 72);
            this.adminAddProducts_id.Name = "adminAddProducts_id";
            this.adminAddProducts_id.Size = new System.Drawing.Size(206, 31);
            this.adminAddProducts_id.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(179, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Product ID:";
            // 
            // AdminAddProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(237)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AdminAddProducts";
            this.Size = new System.Drawing.Size(1610, 1014);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminAddProducts_imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox adminAddProducts_name;
        private System.Windows.Forms.TextBox adminAddProducts_id;
        private System.Windows.Forms.ComboBox adminAddProducts_status;
        private System.Windows.Forms.TextBox adminAddProducts_price;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox adminAddProducts_stock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox adminAddProducts_type;
        private System.Windows.Forms.Button adminAddProducts_clearBtn;
        private System.Windows.Forms.Button adminAddProducts_deleteBtn;
        private System.Windows.Forms.Button adminAddProducts_updateBtn;
        private System.Windows.Forms.Button adminAddProducts_addBtn;
        private System.Windows.Forms.PictureBox adminAddProducts_imageBox;
        private System.Windows.Forms.Button adminAddProducts_importBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
