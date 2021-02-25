namespace kursach_1._1
{
    partial class AddForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.Drivers_listview = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_add_driver = new System.Windows.Forms.Button();
            this.tb_driverName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Drivers_listview
            // 
            this.Drivers_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Drivers_listview.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Drivers_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.Drivers_listview.FullRowSelect = true;
            this.Drivers_listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Drivers_listview.HideSelection = false;
            this.Drivers_listview.LargeImageList = this.imageList1;
            this.Drivers_listview.Location = new System.Drawing.Point(12, 25);
            this.Drivers_listview.MultiSelect = false;
            this.Drivers_listview.Name = "Drivers_listview";
            this.Drivers_listview.Size = new System.Drawing.Size(459, 203);
            this.Drivers_listview.SmallImageList = this.imageList1;
            this.Drivers_listview.TabIndex = 1;
            this.Drivers_listview.UseCompatibleStateImageBehavior = false;
            this.Drivers_listview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 5;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Устройство";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Емкость";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Свободно";
            this.columnHeader4.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "green.png");
            this.imageList1.Images.SetKeyName(1, "red.png");
            // 
            // btn_add_driver
            // 
            this.btn_add_driver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add_driver.Location = new System.Drawing.Point(396, 265);
            this.btn_add_driver.Name = "btn_add_driver";
            this.btn_add_driver.Size = new System.Drawing.Size(75, 23);
            this.btn_add_driver.TabIndex = 2;
            this.btn_add_driver.Text = "Создать";
            this.btn_add_driver.UseVisualStyleBackColor = true;
            this.btn_add_driver.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_driverName
            // 
            this.tb_driverName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_driverName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tb_driverName.Location = new System.Drawing.Point(12, 265);
            this.tb_driverName.Name = "tb_driverName";
            this.tb_driverName.Size = new System.Drawing.Size(378, 20);
            this.tb_driverName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Название устройства";
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 305);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_driverName);
            this.Controls.Add(this.btn_add_driver);
            this.Controls.Add(this.Drivers_listview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddForm";
            this.Text = "Добавленые новых устройств для синхронизации";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Drivers_listview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btn_add_driver;
        private System.Windows.Forms.TextBox tb_driverName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList imageList1;

    }
}