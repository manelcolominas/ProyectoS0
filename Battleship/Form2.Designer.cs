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
            this.user_dataGridView = new System.Windows.Forms.DataGridView();
            this.opponent_dataGridView = new System.Windows.Forms.DataGridView();
            this.form2_shoot_button = new System.Windows.Forms.Button();
            this.form2_send_fleet_position_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // user_dataGridView
            // 
            this.user_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_dataGridView.Location = new System.Drawing.Point(153, 854);
            this.user_dataGridView.Name = "user_dataGridView";
            this.user_dataGridView.RowHeadersWidth = 92;
            this.user_dataGridView.RowTemplate.Height = 37;
            this.user_dataGridView.Size = new System.Drawing.Size(874, 724);
            this.user_dataGridView.TabIndex = 0;
            this.user_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.user_dataGridView_CellClick);
            this.user_dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.user_dataGridView_CellContentClick);
            // 
            // opponent_dataGridView
            // 
            this.opponent_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.opponent_dataGridView.Location = new System.Drawing.Point(153, 37);
            this.opponent_dataGridView.Name = "opponent_dataGridView";
            this.opponent_dataGridView.RowHeadersWidth = 92;
            this.opponent_dataGridView.RowTemplate.Height = 37;
            this.opponent_dataGridView.Size = new System.Drawing.Size(874, 735);
            this.opponent_dataGridView.TabIndex = 1;
            this.opponent_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.opponent_dataGridView_CellClick);
            // 
            // form2_shoot_button
            // 
            this.form2_shoot_button.Location = new System.Drawing.Point(1357, 258);
            this.form2_shoot_button.Name = "form2_shoot_button";
            this.form2_shoot_button.Size = new System.Drawing.Size(335, 95);
            this.form2_shoot_button.TabIndex = 2;
            this.form2_shoot_button.Text = "Shoot";
            this.form2_shoot_button.UseVisualStyleBackColor = true;
            this.form2_shoot_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // form2_send_fleet_position_button
            // 
            this.form2_send_fleet_position_button.Location = new System.Drawing.Point(1419, 718);
            this.form2_send_fleet_position_button.Name = "form2_send_fleet_position_button";
            this.form2_send_fleet_position_button.Size = new System.Drawing.Size(309, 112);
            this.form2_send_fleet_position_button.TabIndex = 3;
            this.form2_send_fleet_position_button.Text = "Send Fleet Position";
            this.form2_send_fleet_position_button.UseVisualStyleBackColor = true;
            this.form2_send_fleet_position_button.Click += new System.EventHandler(this.form2_send_fleet_position_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1954, 1590);
            this.Controls.Add(this.form2_send_fleet_position_button);
            this.Controls.Add(this.form2_shoot_button);
            this.Controls.Add(this.opponent_dataGridView);
            this.Controls.Add(this.user_dataGridView);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView user_dataGridView;
        private System.Windows.Forms.DataGridView opponent_dataGridView;
        private System.Windows.Forms.Button form2_shoot_button;
        private System.Windows.Forms.Button form2_send_fleet_position_button;
    }
}