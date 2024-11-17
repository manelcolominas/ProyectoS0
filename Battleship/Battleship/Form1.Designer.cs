namespace Battleship
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.login_panel = new System.Windows.Forms.Panel();
            this.login_panel_signup_button = new System.Windows.Forms.Button();
            this.login_panel_login_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.login_panel_password_textBox = new System.Windows.Forms.TextBox();
            this.login_panel_username_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.signup_panel_email_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel_signup_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.signup_panel_password_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel_username_textBox = new System.Windows.Forms.TextBox();
            this.user_panel = new System.Windows.Forms.Panel();
            this.ending_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.initial_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.query_Button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.opponent_textBox = new System.Windows.Forms.TextBox();
            this.ranking_radioButton = new System.Windows.Forms.RadioButton();
            this.games_sort_date_radioButton = new System.Windows.Forms.RadioButton();
            this.game_results_radioButton = new System.Windows.Forms.RadioButton();
            this.players_list_radioButton = new System.Windows.Forms.RadioButton();
            this.new_game_User_panel_button = new System.Windows.Forms.Button();
            this.resume_game_User_panel_button = new System.Windows.Forms.Button();
            this.nombre_usuario_label = new System.Windows.Forms.Label();
            this.desconnect_server__button = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.login_panel.SuspendLayout();
            this.signup_panel.SuspendLayout();
            this.user_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // login_panel
            // 
            this.login_panel.Controls.Add(this.login_panel_signup_button);
            this.login_panel.Controls.Add(this.login_panel_login_button);
            this.login_panel.Controls.Add(this.label2);
            this.login_panel.Controls.Add(this.label1);
            this.login_panel.Controls.Add(this.login_panel_password_textBox);
            this.login_panel.Controls.Add(this.login_panel_username_textBox);
            this.login_panel.Location = new System.Drawing.Point(1335, 103);
            this.login_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_panel.Name = "login_panel";
            this.login_panel.Size = new System.Drawing.Size(1046, 596);
            this.login_panel.TabIndex = 2;
            // 
            // login_panel_signup_button
            // 
            this.login_panel_signup_button.Location = new System.Drawing.Point(719, 379);
            this.login_panel_signup_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_panel_signup_button.Name = "login_panel_signup_button";
            this.login_panel_signup_button.Size = new System.Drawing.Size(318, 56);
            this.login_panel_signup_button.TabIndex = 5;
            this.login_panel_signup_button.Text = "SIgnUp";
            this.login_panel_signup_button.UseVisualStyleBackColor = true;
            this.login_panel_signup_button.Click += new System.EventHandler(this.login_panel_signup_button_Click);
            // 
            // login_panel_login_button
            // 
            this.login_panel_login_button.Location = new System.Drawing.Point(327, 379);
            this.login_panel_login_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_panel_login_button.Name = "login_panel_login_button";
            this.login_panel_login_button.Size = new System.Drawing.Size(341, 56);
            this.login_panel_login_button.TabIndex = 4;
            this.login_panel_login_button.Text = "Login";
            this.login_panel_login_button.UseVisualStyleBackColor = true;
            this.login_panel_login_button.Click += new System.EventHandler(this.login_panel_login_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 283);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // login_panel_password_textBox
            // 
            this.login_panel_password_textBox.Location = new System.Drawing.Point(485, 265);
            this.login_panel_password_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_panel_password_textBox.Name = "login_panel_password_textBox";
            this.login_panel_password_textBox.Size = new System.Drawing.Size(380, 35);
            this.login_panel_password_textBox.TabIndex = 1;
            // 
            // login_panel_username_textBox
            // 
            this.login_panel_username_textBox.Location = new System.Drawing.Point(478, 170);
            this.login_panel_username_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_panel_username_textBox.Name = "login_panel_username_textBox";
            this.login_panel_username_textBox.Size = new System.Drawing.Size(387, 35);
            this.login_panel_username_textBox.TabIndex = 0;
            // 
            // signup_panel
            // 
            this.signup_panel.Controls.Add(this.label5);
            this.signup_panel.Controls.Add(this.signup_panel_email_textBox);
            this.signup_panel.Controls.Add(this.signup_panel_signup_button);
            this.signup_panel.Controls.Add(this.label3);
            this.signup_panel.Controls.Add(this.label4);
            this.signup_panel.Controls.Add(this.signup_panel_password_textBox);
            this.signup_panel.Controls.Add(this.signup_panel_username_textBox);
            this.signup_panel.Location = new System.Drawing.Point(1356, 768);
            this.signup_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signup_panel.Name = "signup_panel";
            this.signup_panel.Size = new System.Drawing.Size(990, 482);
            this.signup_panel.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(243, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email";
            // 
            // signup_panel_email_textBox
            // 
            this.signup_panel_email_textBox.Location = new System.Drawing.Point(485, 165);
            this.signup_panel_email_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signup_panel_email_textBox.Name = "signup_panel_email_textBox";
            this.signup_panel_email_textBox.Size = new System.Drawing.Size(387, 35);
            this.signup_panel_email_textBox.TabIndex = 11;
            // 
            // signup_panel_signup_button
            // 
            this.signup_panel_signup_button.Location = new System.Drawing.Point(334, 317);
            this.signup_panel_signup_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signup_panel_signup_button.Name = "signup_panel_signup_button";
            this.signup_panel_signup_button.Size = new System.Drawing.Size(341, 56);
            this.signup_panel_signup_button.TabIndex = 10;
            this.signup_panel_signup_button.Text = "SignUp";
            this.signup_panel_signup_button.UseVisualStyleBackColor = true;
            this.signup_panel_signup_button.Click += new System.EventHandler(this.signup_panel_signup_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 237);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 29);
            this.label4.TabIndex = 8;
            this.label4.Text = "Username";
            // 
            // signup_panel_password_textBox
            // 
            this.signup_panel_password_textBox.Location = new System.Drawing.Point(485, 236);
            this.signup_panel_password_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signup_panel_password_textBox.Name = "signup_panel_password_textBox";
            this.signup_panel_password_textBox.Size = new System.Drawing.Size(387, 35);
            this.signup_panel_password_textBox.TabIndex = 7;
            // 
            // signup_panel_username_textBox
            // 
            this.signup_panel_username_textBox.Location = new System.Drawing.Point(485, 109);
            this.signup_panel_username_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signup_panel_username_textBox.Name = "signup_panel_username_textBox";
            this.signup_panel_username_textBox.Size = new System.Drawing.Size(387, 35);
            this.signup_panel_username_textBox.TabIndex = 6;
            // 
            // user_panel
            // 
            this.user_panel.Controls.Add(this.ending_dateTimePicker);
            this.user_panel.Controls.Add(this.initial_dateTimePicker);
            this.user_panel.Controls.Add(this.dataGridView);
            this.user_panel.Controls.Add(this.label6);
            this.user_panel.Controls.Add(this.query_Button);
            this.user_panel.Controls.Add(this.label7);
            this.user_panel.Controls.Add(this.label8);
            this.user_panel.Controls.Add(this.opponent_textBox);
            this.user_panel.Controls.Add(this.ranking_radioButton);
            this.user_panel.Controls.Add(this.games_sort_date_radioButton);
            this.user_panel.Controls.Add(this.game_results_radioButton);
            this.user_panel.Controls.Add(this.players_list_radioButton);
            this.user_panel.Controls.Add(this.new_game_User_panel_button);
            this.user_panel.Controls.Add(this.resume_game_User_panel_button);
            this.user_panel.Location = new System.Drawing.Point(54, 368);
            this.user_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.user_panel.Name = "user_panel";
            this.user_panel.Size = new System.Drawing.Size(1251, 908);
            this.user_panel.TabIndex = 4;
            // 
            // ending_dateTimePicker
            // 
            this.ending_dateTimePicker.Location = new System.Drawing.Point(826, 616);
            this.ending_dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ending_dateTimePicker.Name = "ending_dateTimePicker";
            this.ending_dateTimePicker.Size = new System.Drawing.Size(252, 35);
            this.ending_dateTimePicker.TabIndex = 44;
            // 
            // initial_dateTimePicker
            // 
            this.initial_dateTimePicker.Location = new System.Drawing.Point(480, 616);
            this.initial_dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.initial_dateTimePicker.Name = "initial_dateTimePicker";
            this.initial_dateTimePicker.Size = new System.Drawing.Size(240, 35);
            this.initial_dateTimePicker.TabIndex = 43;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(726, 112);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 92;
            this.dataGridView.RowTemplate.Height = 37;
            this.dataGridView.Size = new System.Drawing.Size(612, 346);
            this.dataGridView.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(752, 622);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 29);
            this.label6.TabIndex = 41;
            this.label6.Text = "to";
            // 
            // query_Button
            // 
            this.query_Button.Location = new System.Drawing.Point(68, 729);
            this.query_Button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.query_Button.Name = "query_Button";
            this.query_Button.Size = new System.Drawing.Size(226, 45);
            this.query_Button.TabIndex = 40;
            this.query_Button.Text = "Send Query";
            this.query_Button.UseVisualStyleBackColor = true;
            this.query_Button.Click += new System.EventHandler(this.query_Button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 460);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 29);
            this.label7.TabIndex = 39;
            this.label7.Text = "QUERIES:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(558, 571);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(475, 18);
            this.label8.TabIndex = 38;
            this.label8.Text = "write the username of the opponent you want to consult the games with";
            // 
            // opponent_textBox
            // 
            this.opponent_textBox.Location = new System.Drawing.Point(396, 560);
            this.opponent_textBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.opponent_textBox.Name = "opponent_textBox";
            this.opponent_textBox.Size = new System.Drawing.Size(153, 35);
            this.opponent_textBox.TabIndex = 35;
            // 
            // ranking_radioButton
            // 
            this.ranking_radioButton.AutoSize = true;
            this.ranking_radioButton.Location = new System.Drawing.Point(150, 660);
            this.ranking_radioButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.ranking_radioButton.Name = "ranking_radioButton";
            this.ranking_radioButton.Size = new System.Drawing.Size(132, 33);
            this.ranking_radioButton.TabIndex = 37;
            this.ranking_radioButton.TabStop = true;
            this.ranking_radioButton.Text = "Ranking";
            this.ranking_radioButton.UseVisualStyleBackColor = true;
            // 
            // games_sort_date_radioButton
            // 
            this.games_sort_date_radioButton.AutoSize = true;
            this.games_sort_date_radioButton.Location = new System.Drawing.Point(150, 614);
            this.games_sort_date_radioButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.games_sort_date_radioButton.Name = "games_sort_date_radioButton";
            this.games_sort_date_radioButton.Size = new System.Drawing.Size(339, 33);
            this.games_sort_date_radioButton.TabIndex = 36;
            this.games_sort_date_radioButton.TabStop = true;
            this.games_sort_date_radioButton.Text = "Games sorted by date from ";
            this.games_sort_date_radioButton.UseVisualStyleBackColor = true;
            // 
            // game_results_radioButton
            // 
            this.game_results_radioButton.AutoSize = true;
            this.game_results_radioButton.Location = new System.Drawing.Point(150, 566);
            this.game_results_radioButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.game_results_radioButton.Name = "game_results_radioButton";
            this.game_results_radioButton.Size = new System.Drawing.Size(235, 33);
            this.game_results_radioButton.TabIndex = 34;
            this.game_results_radioButton.TabStop = true;
            this.game_results_radioButton.Text = "Game results with";
            this.game_results_radioButton.UseVisualStyleBackColor = true;
            // 
            // players_list_radioButton
            // 
            this.players_list_radioButton.AutoSize = true;
            this.players_list_radioButton.Location = new System.Drawing.Point(150, 517);
            this.players_list_radioButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.players_list_radioButton.Name = "players_list_radioButton";
            this.players_list_radioButton.Size = new System.Drawing.Size(166, 33);
            this.players_list_radioButton.TabIndex = 33;
            this.players_list_radioButton.TabStop = true;
            this.players_list_radioButton.Text = "Players list ";
            this.players_list_radioButton.UseVisualStyleBackColor = true;
            // 
            // new_game_User_panel_button
            // 
            this.new_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.new_game_User_panel_button.Location = new System.Drawing.Point(18, 112);
            this.new_game_User_panel_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.new_game_User_panel_button.Name = "new_game_User_panel_button";
            this.new_game_User_panel_button.Size = new System.Drawing.Size(642, 160);
            this.new_game_User_panel_button.TabIndex = 31;
            this.new_game_User_panel_button.Text = "New Game";
            this.new_game_User_panel_button.UseVisualStyleBackColor = true;
            // 
            // resume_game_User_panel_button
            // 
            this.resume_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.resume_game_User_panel_button.Location = new System.Drawing.Point(18, 339);
            this.resume_game_User_panel_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resume_game_User_panel_button.Name = "resume_game_User_panel_button";
            this.resume_game_User_panel_button.Size = new System.Drawing.Size(642, 76);
            this.resume_game_User_panel_button.TabIndex = 32;
            this.resume_game_User_panel_button.Text = "Resume Game";
            this.resume_game_User_panel_button.UseVisualStyleBackColor = true;
            // 
            // nombre_usuario_label
            // 
            this.nombre_usuario_label.AutoSize = true;
            this.nombre_usuario_label.Location = new System.Drawing.Point(2140, 25);
            this.nombre_usuario_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nombre_usuario_label.Name = "nombre_usuario_label";
            this.nombre_usuario_label.Size = new System.Drawing.Size(0, 29);
            this.nombre_usuario_label.TabIndex = 5;
            // 
            // desconnect_server__button
            // 
            this.desconnect_server__button.Location = new System.Drawing.Point(111, 182);
            this.desconnect_server__button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.desconnect_server__button.Name = "desconnect_server__button";
            this.desconnect_server__button.Size = new System.Drawing.Size(254, 96);
            this.desconnect_server__button.TabIndex = 7;
            this.desconnect_server__button.Text = "Desconnect";
            this.desconnect_server__button.UseVisualStyleBackColor = true;
            this.desconnect_server__button.Click += new System.EventHandler(this.desconnect_server__button_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(111, 29);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(249, 98);
            this.Connect.TabIndex = 8;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2539, 1207);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.desconnect_server__button);
            this.Controls.Add(this.nombre_usuario_label);
            this.Controls.Add(this.user_panel);
            this.Controls.Add(this.signup_panel);
            this.Controls.Add(this.login_panel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.login_panel.ResumeLayout(false);
            this.login_panel.PerformLayout();
            this.signup_panel.ResumeLayout(false);
            this.signup_panel.PerformLayout();
            this.user_panel.ResumeLayout(false);
            this.user_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel login_panel;
        private System.Windows.Forms.Button login_panel_login_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox login_panel_password_textBox;
        private System.Windows.Forms.TextBox login_panel_username_textBox;
        private System.Windows.Forms.Button login_panel_signup_button;
        private System.Windows.Forms.Panel signup_panel;
        private System.Windows.Forms.Button signup_panel_signup_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox signup_panel_password_textBox;
        private System.Windows.Forms.TextBox signup_panel_username_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox signup_panel_email_textBox;
        private System.Windows.Forms.Panel user_panel;
        private System.Windows.Forms.DateTimePicker ending_dateTimePicker;
        private System.Windows.Forms.DateTimePicker initial_dateTimePicker;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button query_Button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox opponent_textBox;
        private System.Windows.Forms.RadioButton ranking_radioButton;
        private System.Windows.Forms.RadioButton games_sort_date_radioButton;
        private System.Windows.Forms.RadioButton game_results_radioButton;
        private System.Windows.Forms.RadioButton players_list_radioButton;
        private System.Windows.Forms.Button new_game_User_panel_button;
        private System.Windows.Forms.Button resume_game_User_panel_button;
        private System.Windows.Forms.Label nombre_usuario_label;
        private System.Windows.Forms.Button desconnect_server__button;
        private System.Windows.Forms.Button Connect;
    }
}

