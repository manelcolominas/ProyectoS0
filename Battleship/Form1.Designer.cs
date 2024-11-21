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
            this.label9 = new System.Windows.Forms.Label();
            this.login_panel_signup_button = new System.Windows.Forms.Button();
            this.login_panel_login_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.login_panel_password_textBox = new System.Windows.Forms.TextBox();
            this.login_panel_username_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel = new System.Windows.Forms.Panel();
            this.back_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.signup_panel_email_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel_signup_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.signup_panel_password_textBox = new System.Windows.Forms.TextBox();
            this.signup_panel_username_textBox = new System.Windows.Forms.TextBox();
            this.user_panel = new System.Windows.Forms.Panel();
            this.connected_users_label = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nombre_usuario_label = new System.Windows.Forms.Label();
            this.desconnect_server__button = new System.Windows.Forms.Button();
            this.connect_server_button = new System.Windows.Forms.Button();
            this.login_panel.SuspendLayout();
            this.signup_panel.SuspendLayout();
            this.user_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // login_panel
            // 
            this.login_panel.BackColor = System.Drawing.Color.Transparent;
            this.login_panel.Controls.Add(this.connect_server_button);
            this.login_panel.Controls.Add(this.label9);
            this.login_panel.Controls.Add(this.login_panel_signup_button);
            this.login_panel.Controls.Add(this.login_panel_login_button);
            this.login_panel.Controls.Add(this.label2);
            this.login_panel.Controls.Add(this.label1);
            this.login_panel.Controls.Add(this.login_panel_password_textBox);
            this.login_panel.Controls.Add(this.login_panel_username_textBox);
            this.login_panel.Location = new System.Drawing.Point(214, 11);
            this.login_panel.Margin = new System.Windows.Forms.Padding(2);
            this.login_panel.Name = "login_panel";
            this.login_panel.Size = new System.Drawing.Size(947, 329);
            this.login_panel.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(591, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 32);
            this.label9.TabIndex = 6;
            this.label9.Text = "¿Don\'t you have \r\nan account yet?";
            // 
            // login_panel_signup_button
            // 
            this.login_panel_signup_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_panel_signup_button.Location = new System.Drawing.Point(579, 122);
            this.login_panel_signup_button.Margin = new System.Windows.Forms.Padding(2);
            this.login_panel_signup_button.Name = "login_panel_signup_button";
            this.login_panel_signup_button.Size = new System.Drawing.Size(128, 42);
            this.login_panel_signup_button.TabIndex = 5;
            this.login_panel_signup_button.Text = "Sign Up";
            this.login_panel_signup_button.UseVisualStyleBackColor = true;
            this.login_panel_signup_button.Click += new System.EventHandler(this.login_panel_signup_button_Click);
            // 
            // login_panel_login_button
            // 
            this.login_panel_login_button.BackColor = System.Drawing.Color.SkyBlue;
            this.login_panel_login_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_panel_login_button.Location = new System.Drawing.Point(434, 122);
            this.login_panel_login_button.Margin = new System.Windows.Forms.Padding(2);
            this.login_panel_login_button.Name = "login_panel_login_button";
            this.login_panel_login_button.Size = new System.Drawing.Size(114, 42);
            this.login_panel_login_button.TabIndex = 4;
            this.login_panel_login_button.Text = "Login";
            this.login_panel_login_button.UseVisualStyleBackColor = false;
            this.login_panel_login_button.Click += new System.EventHandler(this.login_panel_login_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(135, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // login_panel_password_textBox
            // 
            this.login_panel_password_textBox.Location = new System.Drawing.Point(250, 156);
            this.login_panel_password_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.login_panel_password_textBox.Name = "login_panel_password_textBox";
            this.login_panel_password_textBox.Size = new System.Drawing.Size(145, 22);
            this.login_panel_password_textBox.TabIndex = 1;
            // 
            // login_panel_username_textBox
            // 
            this.login_panel_username_textBox.Location = new System.Drawing.Point(250, 122);
            this.login_panel_username_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.login_panel_username_textBox.Name = "login_panel_username_textBox";
            this.login_panel_username_textBox.Size = new System.Drawing.Size(145, 22);
            this.login_panel_username_textBox.TabIndex = 0;
            // 
            // signup_panel
            // 
            this.signup_panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.signup_panel.Controls.Add(this.back_button);
            this.signup_panel.Controls.Add(this.label5);
            this.signup_panel.Controls.Add(this.signup_panel_email_textBox);
            this.signup_panel.Controls.Add(this.signup_panel_signup_button);
            this.signup_panel.Controls.Add(this.label3);
            this.signup_panel.Controls.Add(this.label4);
            this.signup_panel.Controls.Add(this.signup_panel_password_textBox);
            this.signup_panel.Controls.Add(this.signup_panel_username_textBox);
            this.signup_panel.Location = new System.Drawing.Point(915, 424);
            this.signup_panel.Margin = new System.Windows.Forms.Padding(2);
            this.signup_panel.Name = "signup_panel";
            this.signup_panel.Size = new System.Drawing.Size(422, 266);
            this.signup_panel.TabIndex = 3;
            // 
            // back_button
            // 
            this.back_button.BackColor = System.Drawing.SystemColors.GrayText;
            this.back_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_button.Location = new System.Drawing.Point(346, 203);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(59, 48);
            this.back_button.TabIndex = 13;
            this.back_button.Text = " ↶";
            this.back_button.UseVisualStyleBackColor = false;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 92);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email";
            // 
            // signup_panel_email_textBox
            // 
            this.signup_panel_email_textBox.Location = new System.Drawing.Point(173, 89);
            this.signup_panel_email_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.signup_panel_email_textBox.Name = "signup_panel_email_textBox";
            this.signup_panel_email_textBox.Size = new System.Drawing.Size(223, 22);
            this.signup_panel_email_textBox.TabIndex = 11;
            // 
            // signup_panel_signup_button
            // 
            this.signup_panel_signup_button.BackColor = System.Drawing.Color.LightGreen;
            this.signup_panel_signup_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_panel_signup_button.Location = new System.Drawing.Point(77, 173);
            this.signup_panel_signup_button.Margin = new System.Windows.Forms.Padding(2);
            this.signup_panel_signup_button.Name = "signup_panel_signup_button";
            this.signup_panel_signup_button.Size = new System.Drawing.Size(135, 50);
            this.signup_panel_signup_button.TabIndex = 10;
            this.signup_panel_signup_button.Text = "SignUp";
            this.signup_panel_signup_button.UseVisualStyleBackColor = false;
            this.signup_panel_signup_button.Click += new System.EventHandler(this.signup_panel_signup_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Username";
            // 
            // signup_panel_password_textBox
            // 
            this.signup_panel_password_textBox.Location = new System.Drawing.Point(173, 128);
            this.signup_panel_password_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.signup_panel_password_textBox.Name = "signup_panel_password_textBox";
            this.signup_panel_password_textBox.Size = new System.Drawing.Size(223, 22);
            this.signup_panel_password_textBox.TabIndex = 7;
            // 
            // signup_panel_username_textBox
            // 
            this.signup_panel_username_textBox.Location = new System.Drawing.Point(173, 58);
            this.signup_panel_username_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.signup_panel_username_textBox.Name = "signup_panel_username_textBox";
            this.signup_panel_username_textBox.Size = new System.Drawing.Size(223, 22);
            this.signup_panel_username_textBox.TabIndex = 6;
            // 
            // user_panel
            // 
            this.user_panel.AutoSize = true;
            this.user_panel.BackColor = System.Drawing.Color.White;
            this.user_panel.Controls.Add(this.desconnect_server__button);
            this.user_panel.Controls.Add(this.connected_users_label);
            this.user_panel.Controls.Add(this.label10);
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
            this.user_panel.Controls.Add(this.pictureBox1);
            this.user_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.user_panel.Location = new System.Drawing.Point(0, 0);
            this.user_panel.Margin = new System.Windows.Forms.Padding(2);
            this.user_panel.Name = "user_panel";
            this.user_panel.Size = new System.Drawing.Size(1456, 732);
            this.user_panel.TabIndex = 4;
            this.user_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.user_panel_Paint);
            // 
            // connected_users_label
            // 
            this.connected_users_label.AutoSize = true;
            this.connected_users_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.connected_users_label.Location = new System.Drawing.Point(1097, 64);
            this.connected_users_label.Name = "connected_users_label";
            this.connected_users_label.Size = new System.Drawing.Size(2, 18);
            this.connected_users_label.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1094, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(174, 16);
            this.label10.TabIndex = 46;
            this.label10.Text = "USUARIOS CONECTADOS";
            // 
            // ending_dateTimePicker
            // 
            this.ending_dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ending_dateTimePicker.Location = new System.Drawing.Point(604, 446);
            this.ending_dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.ending_dateTimePicker.Name = "ending_dateTimePicker";
            this.ending_dateTimePicker.Size = new System.Drawing.Size(146, 30);
            this.ending_dateTimePicker.TabIndex = 44;
            // 
            // initial_dateTimePicker
            // 
            this.initial_dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initial_dateTimePicker.Location = new System.Drawing.Point(429, 446);
            this.initial_dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.initial_dateTimePicker.Name = "initial_dateTimePicker";
            this.initial_dateTimePicker.Size = new System.Drawing.Size(139, 30);
            this.initial_dateTimePicker.TabIndex = 43;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(494, 167);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView.RowTemplate.Height = 37;
            this.dataGridView.Size = new System.Drawing.Size(417, 191);
            this.dataGridView.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(572, 448);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 25);
            this.label6.TabIndex = 41;
            this.label6.Text = "to";
            // 
            // query_Button
            // 
            this.query_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.query_Button.Location = new System.Drawing.Point(118, 507);
            this.query_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.query_Button.Name = "query_Button";
            this.query_Button.Size = new System.Drawing.Size(141, 43);
            this.query_Button.TabIndex = 40;
            this.query_Button.Text = "Send Query";
            this.query_Button.UseVisualStyleBackColor = true;
            this.query_Button.Click += new System.EventHandler(this.query_Button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(108, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 25);
            this.label7.TabIndex = 39;
            this.label7.Text = "QUERIES:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(462, 422);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(418, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "write the username of the opponent you want to consult the games with";
            // 
            // opponent_textBox
            // 
            this.opponent_textBox.Location = new System.Drawing.Point(354, 419);
            this.opponent_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.opponent_textBox.Name = "opponent_textBox";
            this.opponent_textBox.Size = new System.Drawing.Size(102, 22);
            this.opponent_textBox.TabIndex = 35;
            // 
            // ranking_radioButton
            // 
            this.ranking_radioButton.AutoSize = true;
            this.ranking_radioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ranking_radioButton.Location = new System.Drawing.Point(165, 469);
            this.ranking_radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ranking_radioButton.Name = "ranking_radioButton";
            this.ranking_radioButton.Size = new System.Drawing.Size(104, 29);
            this.ranking_radioButton.TabIndex = 37;
            this.ranking_radioButton.TabStop = true;
            this.ranking_radioButton.Text = "Ranking";
            this.ranking_radioButton.UseVisualStyleBackColor = true;
            // 
            // games_sort_date_radioButton
            // 
            this.games_sort_date_radioButton.AutoSize = true;
            this.games_sort_date_radioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.games_sort_date_radioButton.Location = new System.Drawing.Point(165, 444);
            this.games_sort_date_radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.games_sort_date_radioButton.Name = "games_sort_date_radioButton";
            this.games_sort_date_radioButton.Size = new System.Drawing.Size(272, 29);
            this.games_sort_date_radioButton.TabIndex = 36;
            this.games_sort_date_radioButton.TabStop = true;
            this.games_sort_date_radioButton.Text = "Games sorted by date from ";
            this.games_sort_date_radioButton.UseVisualStyleBackColor = true;
            // 
            // game_results_radioButton
            // 
            this.game_results_radioButton.AutoSize = true;
            this.game_results_radioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.game_results_radioButton.Location = new System.Drawing.Point(165, 417);
            this.game_results_radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.game_results_radioButton.Name = "game_results_radioButton";
            this.game_results_radioButton.Size = new System.Drawing.Size(187, 29);
            this.game_results_radioButton.TabIndex = 34;
            this.game_results_radioButton.TabStop = true;
            this.game_results_radioButton.Text = "Game results with";
            this.game_results_radioButton.UseVisualStyleBackColor = true;
            // 
            // players_list_radioButton
            // 
            this.players_list_radioButton.AutoSize = true;
            this.players_list_radioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players_list_radioButton.Location = new System.Drawing.Point(165, 390);
            this.players_list_radioButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.players_list_radioButton.Name = "players_list_radioButton";
            this.players_list_radioButton.Size = new System.Drawing.Size(131, 29);
            this.players_list_radioButton.TabIndex = 33;
            this.players_list_radioButton.TabStop = true;
            this.players_list_radioButton.Text = "Players list ";
            this.players_list_radioButton.UseVisualStyleBackColor = true;
            // 
            // new_game_User_panel_button
            // 
            this.new_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.new_game_User_panel_button.Location = new System.Drawing.Point(89, 167);
            this.new_game_User_panel_button.Margin = new System.Windows.Forms.Padding(2);
            this.new_game_User_panel_button.Name = "new_game_User_panel_button";
            this.new_game_User_panel_button.Size = new System.Drawing.Size(367, 88);
            this.new_game_User_panel_button.TabIndex = 31;
            this.new_game_User_panel_button.Text = "New Game";
            this.new_game_User_panel_button.UseVisualStyleBackColor = true;
            // 
            // resume_game_User_panel_button
            // 
            this.resume_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.resume_game_User_panel_button.Location = new System.Drawing.Point(89, 292);
            this.resume_game_User_panel_button.Margin = new System.Windows.Forms.Padding(2);
            this.resume_game_User_panel_button.Name = "resume_game_User_panel_button";
            this.resume_game_User_panel_button.Size = new System.Drawing.Size(367, 42);
            this.resume_game_User_panel_button.TabIndex = 32;
            this.resume_game_User_panel_button.Text = "Resume Game";
            this.resume_game_User_panel_button.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Battleship.Properties.Resources.fondo_inici;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(675, 223);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(956, 576);
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // nombre_usuario_label
            // 
            this.nombre_usuario_label.AutoSize = true;
            this.nombre_usuario_label.Location = new System.Drawing.Point(1223, 14);
            this.nombre_usuario_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nombre_usuario_label.Name = "nombre_usuario_label";
            this.nombre_usuario_label.Size = new System.Drawing.Size(0, 16);
            this.nombre_usuario_label.TabIndex = 5;
            // 
            // desconnect_server__button
            // 
            this.desconnect_server__button.BackColor = System.Drawing.Color.Firebrick;
            this.desconnect_server__button.Font = new System.Drawing.Font("Trebuchet MS", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desconnect_server__button.ForeColor = System.Drawing.SystemColors.Control;
            this.desconnect_server__button.Location = new System.Drawing.Point(31, 29);
            this.desconnect_server__button.Margin = new System.Windows.Forms.Padding(2);
            this.desconnect_server__button.Name = "desconnect_server__button";
            this.desconnect_server__button.Size = new System.Drawing.Size(145, 53);
            this.desconnect_server__button.TabIndex = 7;
            this.desconnect_server__button.Text = "Desconnect";
            this.desconnect_server__button.UseVisualStyleBackColor = false;
            this.desconnect_server__button.Click += new System.EventHandler(this.desconnect_server__button_Click);
            // 
            // connect_server_button
            // 
            this.connect_server_button.BackColor = System.Drawing.Color.LightGreen;
            this.connect_server_button.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connect_server_button.Location = new System.Drawing.Point(36, 27);
            this.connect_server_button.Name = "connect_server_button";
            this.connect_server_button.Size = new System.Drawing.Size(177, 44);
            this.connect_server_button.TabIndex = 7;
            this.connect_server_button.Text = "connect to server";
            this.connect_server_button.UseVisualStyleBackColor = false;
            this.connect_server_button.Click += new System.EventHandler(this.connect_server_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Battleship.Properties.Resources.fondo_inici;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1456, 732);
            this.Controls.Add(this.nombre_usuario_label);
            this.Controls.Add(this.login_panel);
            this.Controls.Add(this.signup_panel);
            this.Controls.Add(this.user_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label connected_users_label;
        private System.Windows.Forms.Button connect_server_button;
    }
}

