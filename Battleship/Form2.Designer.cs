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
            this.opponent_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_dataGridView = new System.Windows.Forms.DataGridView();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // form2_shoot_button
            // 
            this.form2_shoot_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.form2_shoot_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form2_shoot_button.Location = new System.Drawing.Point(776, 551);
            this.form2_shoot_button.Margin = new System.Windows.Forms.Padding(2);
            this.form2_shoot_button.Name = "form2_shoot_button";
            this.form2_shoot_button.Size = new System.Drawing.Size(191, 62);
            this.form2_shoot_button.TabIndex = 2;
            this.form2_shoot_button.Text = "Shoot";
            this.form2_shoot_button.UseVisualStyleBackColor = false;
            this.form2_shoot_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // form2_send_fleet_position_button
            // 
            this.form2_send_fleet_position_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.form2_send_fleet_position_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form2_send_fleet_position_button.Location = new System.Drawing.Point(525, 551);
            this.form2_send_fleet_position_button.Margin = new System.Windows.Forms.Padding(2);
            this.form2_send_fleet_position_button.Name = "form2_send_fleet_position_button";
            this.form2_send_fleet_position_button.Size = new System.Drawing.Size(177, 62);
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
            this.OPONENTS__lbl.Location = new System.Drawing.Point(908, 126);
            this.OPONENTS__lbl.Name = "OPONENTS__lbl";
            this.OPONENTS__lbl.Size = new System.Drawing.Size(158, 17);
            this.OPONENTS__lbl.TabIndex = 15;
            this.OPONENTS__lbl.Text = "OPPONENT\'S SHIPS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(436, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "MY SHIPS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1279, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 182);
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
            this.label3.Location = new System.Drawing.Point(82, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 182);
            this.label3.TabIndex = 12;
            this.label3.Text = "Aircraft Carrier x1\r\nAAAAA\r\n\r\nBattleship x2\r\nBBBB\r\n\r\nCruiser x3\r\nCCC\r\n\r\nSubmarine" +
    " x4\r\nSS\r\n\r\nDestroyer x5\r\nD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // opponent_dataGridView
            // 
            this.opponent_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.opponent_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.opponent_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.opponent_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.opponent_dataGridView.Location = new System.Drawing.Point(754, 171);
            this.opponent_dataGridView.Name = "opponent_dataGridView";
            this.opponent_dataGridView.RowHeadersWidth = 51;
            this.opponent_dataGridView.RowTemplate.Height = 24;
            this.opponent_dataGridView.Size = new System.Drawing.Size(495, 358);
            this.opponent_dataGridView.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "A";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 45;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "B";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 45;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "C";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 45;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "D";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 46;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "E";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 45;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "F";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 44;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "G";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 46;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "H";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 46;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "I";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 39;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "J";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 43;
            // 
            // user_dataGridView
            // 
            this.user_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.user_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.user_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.A,
            this.B,
            this.C,
            this.D,
            this.E,
            this.F,
            this.G,
            this.H,
            this.I,
            this.J});
            this.user_dataGridView.Location = new System.Drawing.Point(227, 171);
            this.user_dataGridView.Name = "user_dataGridView";
            this.user_dataGridView.RowHeadersWidth = 51;
            this.user_dataGridView.RowTemplate.Height = 24;
            this.user_dataGridView.Size = new System.Drawing.Size(495, 358);
            this.user_dataGridView.TabIndex = 10;
            // 
            // A
            // 
            this.A.HeaderText = "A";
            this.A.MinimumWidth = 6;
            this.A.Name = "A";
            this.A.Width = 45;
            // 
            // B
            // 
            this.B.HeaderText = "B";
            this.B.MinimumWidth = 6;
            this.B.Name = "B";
            this.B.Width = 45;
            // 
            // C
            // 
            this.C.HeaderText = "C";
            this.C.MinimumWidth = 6;
            this.C.Name = "C";
            this.C.Width = 45;
            // 
            // D
            // 
            this.D.HeaderText = "D";
            this.D.MinimumWidth = 6;
            this.D.Name = "D";
            this.D.Width = 46;
            // 
            // E
            // 
            this.E.HeaderText = "E";
            this.E.MinimumWidth = 6;
            this.E.Name = "E";
            this.E.Width = 45;
            // 
            // F
            // 
            this.F.HeaderText = "F";
            this.F.MinimumWidth = 6;
            this.F.Name = "F";
            this.F.Width = 44;
            // 
            // G
            // 
            this.G.HeaderText = "G";
            this.G.MinimumWidth = 6;
            this.G.Name = "G";
            this.G.Width = 46;
            // 
            // H
            // 
            this.H.HeaderText = "H";
            this.H.MinimumWidth = 6;
            this.H.Name = "H";
            this.H.Width = 46;
            // 
            // I
            // 
            this.I.HeaderText = "I";
            this.I.MinimumWidth = 6;
            this.I.Name = "I";
            this.I.Width = 39;
            // 
            // J
            // 
            this.J.HeaderText = "J";
            this.J.MinimumWidth = 6;
            this.J.Name = "J";
            this.J.Width = 43;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Battleship.Properties.Resources.sea_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1519, 656);
            this.Controls.Add(this.OPONENTS__lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.opponent_dataGridView);
            this.Controls.Add(this.user_dataGridView);
            this.Controls.Add(this.form2_send_fleet_position_button);
            this.Controls.Add(this.form2_shoot_button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.opponent_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_dataGridView)).EndInit();
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
        private System.Windows.Forms.DataGridView opponent_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridView user_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn C;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn E;
        private System.Windows.Forms.DataGridViewTextBoxColumn F;
        private System.Windows.Forms.DataGridViewTextBoxColumn G;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn I;
        private System.Windows.Forms.DataGridViewTextBoxColumn J;
    }
}