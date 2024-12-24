namespace Battleship
{
    partial class Form2
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
            this.form2_shoot_button = new System.Windows.Forms.Button();
            this.form2_send_fleet_position_button = new System.Windows.Forms.Button();
            this.OPONENTS__lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.user_dataGridView = new System.Windows.Forms.DataGridView();
            this.opponent_dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // form2_shoot_button
            // 
            this.form2_shoot_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.form2_shoot_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form2_shoot_button.Location = new System.Drawing.Point(1358, 999);
            this.form2_shoot_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.form2_shoot_button.Name = "form2_shoot_button";
            this.form2_shoot_button.Size = new System.Drawing.Size(334, 112);
            this.form2_shoot_button.TabIndex = 2;
            this.form2_shoot_button.Text = "Shoot";
            this.form2_shoot_button.UseVisualStyleBackColor = false;
            this.form2_shoot_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // form2_send_fleet_position_button
            // 
            this.form2_send_fleet_position_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.form2_send_fleet_position_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form2_send_fleet_position_button.Location = new System.Drawing.Point(919, 999);
            this.form2_send_fleet_position_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.form2_send_fleet_position_button.Name = "form2_send_fleet_position_button";
            this.form2_send_fleet_position_button.Size = new System.Drawing.Size(310, 112);
            this.form2_send_fleet_position_button.TabIndex = 3;
            this.form2_send_fleet_position_button.Text = "Send Fleet Position";
            this.form2_send_fleet_position_button.UseVisualStyleBackColor = false;
            this.form2_send_fleet_position_button.Click += new System.EventHandler(this.form2_send_fleet_position_button_Click);
            // 
            // OPONENTS__lbl
            // 
            this.OPONENTS__lbl.AutoSize = true;
            this.OPONENTS__lbl.BackColor = System.Drawing.Color.Transparent;
            this.OPONENTS__lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPONENTS__lbl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.OPONENTS__lbl.Location = new System.Drawing.Point(1589, 228);
            this.OPONENTS__lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OPONENTS__lbl.Name = "OPONENTS__lbl";
            this.OPONENTS__lbl.Size = new System.Drawing.Size(264, 29);
            this.OPONENTS__lbl.TabIndex = 15;
            this.OPONENTS__lbl.Text = "OPPONENT\'S SHIPS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(763, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "MY SHIPS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2238, 366);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 308);
            this.label4.TabIndex = 13;
            this.label4.Text = "Aircraft Carrier x1\r\nAAAAA\r\n\r\nBattleship x2\r\nBBBB\r\n\r\nCruiser x3\r\nCCC\r\n\r\nSubmarine" +
    " x4\r\nSS\r\n\r\nDestroyer x5\r\nD";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(144, 366);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 308);
            this.label3.TabIndex = 12;
            this.label3.Text = "Aircraft Carrier x1\r\nAAAAA\r\n\r\nBattleship x2\r\nBBBB\r\n\r\nCruiser x3\r\nCCC\r\n\r\nSubmarine" +
    " x4\r\nSS\r\n\r\nDestroyer x5\r\nD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // user_dataGridView
            // 
            this.user_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_dataGridView.Location = new System.Drawing.Point(425, 306);
            this.user_dataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.user_dataGridView.Name = "user_dataGridView";
            this.user_dataGridView.RowHeadersWidth = 51;
            this.user_dataGridView.RowTemplate.Height = 24;
            this.user_dataGridView.Size = new System.Drawing.Size(772, 620);
            this.user_dataGridView.TabIndex = 16;
            this.user_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.user_dataGridView_CellClick);
            // 
            // opponent_dataGridView
            // 
            this.opponent_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.opponent_dataGridView.Location = new System.Drawing.Point(1358, 306);
            this.opponent_dataGridView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.opponent_dataGridView.Name = "opponent_dataGridView";
            this.opponent_dataGridView.RowHeadersWidth = 51;
            this.opponent_dataGridView.RowTemplate.Height = 24;
            this.opponent_dataGridView.Size = new System.Drawing.Size(772, 620);
            this.opponent_dataGridView.TabIndex = 17;
            this.opponent_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.opponent_dataGridView_CellClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Battleship.Properties.Resources.sea_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2658, 1189);
            this.Controls.Add(this.opponent_dataGridView);
            this.Controls.Add(this.user_dataGridView);
            this.Controls.Add(this.OPONENTS__lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.form2_send_fleet_position_button);
            this.Controls.Add(this.form2_shoot_button);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button form2_shoot_button;
        private System.Windows.Forms.Button form2_send_fleet_position_button;
        private System.Windows.Forms.Label OPONENTS__lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView user_dataGridView;
        private System.Windows.Forms.DataGridView opponent_dataGridView;
    }
}