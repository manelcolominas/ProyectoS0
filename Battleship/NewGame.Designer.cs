namespace Battleship
{
    partial class NewGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGame));
            this.seleccionarOponente_panel = new System.Windows.Forms.Panel();
            this.DataGrid_connectados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.game_panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OPONENTS__lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_MiMar = new System.Windows.Forms.DataGridView();
            this.dataGridView_marOponente = new System.Windows.Forms.DataGridView();
            this.EnviarSolicitud = new System.Windows.Forms.Button();
            this.Conectar = new System.Windows.Forms.Button();
            this.Desconectar = new System.Windows.Forms.Button();
            this.seleccionarOponente_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_connectados)).BeginInit();
            this.game_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MiMar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_marOponente)).BeginInit();
            this.SuspendLayout();
            // 
            // seleccionarOponente_panel
            // 
            this.seleccionarOponente_panel.Controls.Add(this.DataGrid_connectados);
            this.seleccionarOponente_panel.Controls.Add(this.label1);
            this.seleccionarOponente_panel.Location = new System.Drawing.Point(25, 25);
            this.seleccionarOponente_panel.Name = "seleccionarOponente_panel";
            this.seleccionarOponente_panel.Size = new System.Drawing.Size(417, 146);
            this.seleccionarOponente_panel.TabIndex = 0;
            // 
            // DataGrid_connectados
            // 
            this.DataGrid_connectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_connectados.Location = new System.Drawing.Point(19, 37);
            this.DataGrid_connectados.Name = "DataGrid_connectados";
            this.DataGrid_connectados.RowHeadersWidth = 62;
            this.DataGrid_connectados.RowTemplate.Height = 28;
            this.DataGrid_connectados.Size = new System.Drawing.Size(371, 81);
            this.DataGrid_connectados.TabIndex = 1;
            this.DataGrid_connectados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_connectados_CellClick);
            this.DataGrid_connectados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_connectados_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose your opponent from the connected users list:";
            // 
            // game_panel
            // 
            this.game_panel.AutoSize = true;
            this.game_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("game_panel.BackgroundImage")));
            this.game_panel.Controls.Add(this.label4);
            this.game_panel.Controls.Add(this.label3);
            this.game_panel.Controls.Add(this.OPONENTS__lbl);
            this.game_panel.Controls.Add(this.label2);
            this.game_panel.Controls.Add(this.dataGridView_MiMar);
            this.game_panel.Controls.Add(this.dataGridView_marOponente);
            this.game_panel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.game_panel.Location = new System.Drawing.Point(452, 12);
            this.game_panel.Name = "game_panel";
            this.game_panel.Size = new System.Drawing.Size(984, 1008);
            this.game_panel.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(44, 600);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 210);
            this.label4.TabIndex = 5;
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
            this.label3.Location = new System.Drawing.Point(44, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 210);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aircraft Carrier x1\r\nAAAAA\r\n\r\nBattleship x2\r\nBBBB\r\n\r\nCruiser x3\r\nCCC\r\n\r\nSubmarine" +
    " x4\r\nSS\r\n\r\nDestroyer x5\r\nD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OPONENTS__lbl
            // 
            this.OPONENTS__lbl.AutoSize = true;
            this.OPONENTS__lbl.BackColor = System.Drawing.Color.Transparent;
            this.OPONENTS__lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPONENTS__lbl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.OPONENTS__lbl.Location = new System.Drawing.Point(251, 514);
            this.OPONENTS__lbl.Name = "OPONENTS__lbl";
            this.OPONENTS__lbl.Size = new System.Drawing.Size(178, 20);
            this.OPONENTS__lbl.TabIndex = 3;
            this.OPONENTS__lbl.Text = "OPPONENT\'S SHIPS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(251, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "MY SHIPS";
            // 
            // dataGridView_MiMar
            // 
            this.dataGridView_MiMar.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_MiMar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_MiMar.Location = new System.Drawing.Point(241, 92);
            this.dataGridView_MiMar.Name = "dataGridView_MiMar";
            this.dataGridView_MiMar.RowHeadersWidth = 62;
            this.dataGridView_MiMar.RowTemplate.Height = 28;
            this.dataGridView_MiMar.Size = new System.Drawing.Size(290, 286);
            this.dataGridView_MiMar.TabIndex = 1;
            // 
            // dataGridView_marOponente
            // 
            this.dataGridView_marOponente.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_marOponente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_marOponente.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_marOponente.Location = new System.Drawing.Point(241, 556);
            this.dataGridView_marOponente.Name = "dataGridView_marOponente";
            this.dataGridView_marOponente.RowHeadersWidth = 62;
            this.dataGridView_marOponente.RowTemplate.Height = 28;
            this.dataGridView_marOponente.Size = new System.Drawing.Size(304, 286);
            this.dataGridView_marOponente.TabIndex = 0;
            // 
            // EnviarSolicitud
            // 
            this.EnviarSolicitud.Location = new System.Drawing.Point(44, 288);
            this.EnviarSolicitud.Name = "EnviarSolicitud";
            this.EnviarSolicitud.Size = new System.Drawing.Size(75, 23);
            this.EnviarSolicitud.TabIndex = 2;
            this.EnviarSolicitud.Text = "EnviarSolicitud";
            this.EnviarSolicitud.UseVisualStyleBackColor = true;
            this.EnviarSolicitud.Click += new System.EventHandler(this.EnviarSolicitud_Click);
            // 
            // Conectar
            // 
            this.Conectar.Location = new System.Drawing.Point(185, 288);
            this.Conectar.Name = "Conectar";
            this.Conectar.Size = new System.Drawing.Size(75, 23);
            this.Conectar.TabIndex = 3;
            this.Conectar.Text = "Conectar";
            this.Conectar.UseVisualStyleBackColor = true;
            this.Conectar.Click += new System.EventHandler(this.Conectar_Click);
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(340, 288);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(75, 23);
            this.Desconectar.TabIndex = 4;
            this.Desconectar.Text = "Desconectar";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // NewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 1047);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.Conectar);
            this.Controls.Add(this.EnviarSolicitud);
            this.Controls.Add(this.game_panel);
            this.Controls.Add(this.seleccionarOponente_panel);
            this.Name = "NewGame";
            this.Text = "New Game";
            this.seleccionarOponente_panel.ResumeLayout(false);
            this.seleccionarOponente_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_connectados)).EndInit();
            this.game_panel.ResumeLayout(false);
            this.game_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MiMar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_marOponente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel seleccionarOponente_panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataGrid_connectados;
        private System.Windows.Forms.Panel game_panel;
        private System.Windows.Forms.DataGridView dataGridView_marOponente;
        private System.Windows.Forms.DataGridView dataGridView_MiMar;
        private System.Windows.Forms.Label OPONENTS__lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button EnviarSolicitud;
        private System.Windows.Forms.Button Conectar;
        private System.Windows.Forms.Button Desconectar;
    }
}