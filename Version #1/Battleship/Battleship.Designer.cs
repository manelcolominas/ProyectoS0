namespace Battleship
{
    partial class Battleship
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
            this.components = new System.ComponentModel.Container();
            this.username_Login_panel_textBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.password_Login_panel_textBox = new System.Windows.Forms.TextBox();
            this.username_Login_panel_label = new System.Windows.Forms.Label();
            this.password_Login_panel_label = new System.Windows.Forms.Label();
            this.login_Login_panel_button = new System.Windows.Forms.Button();
            this.donthaveanaacountyet_Login_Form_label = new System.Windows.Forms.Label();
            this.email_Sign_Up_panel_textBox = new System.Windows.Forms.TextBox();
            this.username_Sign_Up_panel_textBox = new System.Windows.Forms.TextBox();
            this.password_Sign_Up_panel_textBox = new System.Windows.Forms.TextBox();
            this.email_Sign_Up_Form_label = new System.Windows.Forms.Label();
            this.username_Sign_Up_Form_label = new System.Windows.Forms.Label();
            this.password_Sign_Up_Form_label = new System.Windows.Forms.Label();
            this.new_game_User_panel_button = new System.Windows.Forms.Button();
            this.resume_game_User_panel_button = new System.Windows.Forms.Button();
            this.sign_up_Sign_Up_Form_button = new System.Windows.Forms.Button();
            this.sign_up_Login_panel_button = new System.Windows.Forms.Button();
            this.Login_panel = new System.Windows.Forms.Panel();
            this.User_panel = new System.Windows.Forms.Panel();
            this.connected_users_radioButton = new System.Windows.Forms.RadioButton();
            this.ending_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.initial_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.nombre_usuario_label = new System.Windows.Forms.Label();
            this.query_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.opponent_textBox = new System.Windows.Forms.TextBox();
            this.ranking_radioButton = new System.Windows.Forms.RadioButton();
            this.games_sort_date_radioButton = new System.Windows.Forms.RadioButton();
            this.game_results_radioButton = new System.Windows.Forms.RadioButton();
            this.players_list_radioButton = new System.Windows.Forms.RadioButton();
            this.Sign_Up_panel = new System.Windows.Forms.Panel();
            this.Login_panel.SuspendLayout();
            this.User_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.Sign_Up_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // username_Login_panel_textBox
            // 
            this.username_Login_panel_textBox.Location = new System.Drawing.Point(125, 52);
            this.username_Login_panel_textBox.Name = "username_Login_panel_textBox";
            this.username_Login_panel_textBox.Size = new System.Drawing.Size(131, 26);
            this.username_Login_panel_textBox.TabIndex = 0;
            this.username_Login_panel_textBox.TextChanged += new System.EventHandler(this.username_Login_panel_textBox_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 37);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // password_Login_panel_textBox
            // 
            this.password_Login_panel_textBox.Location = new System.Drawing.Point(125, 97);
            this.password_Login_panel_textBox.Name = "password_Login_panel_textBox";
            this.password_Login_panel_textBox.PasswordChar = '*';
            this.password_Login_panel_textBox.Size = new System.Drawing.Size(131, 26);
            this.password_Login_panel_textBox.TabIndex = 2;
            // 
            // username_Login_panel_label
            // 
            this.username_Login_panel_label.AutoSize = true;
            this.username_Login_panel_label.Location = new System.Drawing.Point(24, 52);
            this.username_Login_panel_label.Name = "username_Login_panel_label";
            this.username_Login_panel_label.Size = new System.Drawing.Size(83, 20);
            this.username_Login_panel_label.TabIndex = 3;
            this.username_Login_panel_label.Text = "Username";
            // 
            // password_Login_panel_label
            // 
            this.password_Login_panel_label.AutoSize = true;
            this.password_Login_panel_label.Location = new System.Drawing.Point(24, 97);
            this.password_Login_panel_label.Name = "password_Login_panel_label";
            this.password_Login_panel_label.Size = new System.Drawing.Size(78, 20);
            this.password_Login_panel_label.TabIndex = 4;
            this.password_Login_panel_label.Text = "Password";
            // 
            // login_Login_panel_button
            // 
            this.login_Login_panel_button.Location = new System.Drawing.Point(145, 131);
            this.login_Login_panel_button.Name = "login_Login_panel_button";
            this.login_Login_panel_button.Size = new System.Drawing.Size(84, 35);
            this.login_Login_panel_button.TabIndex = 5;
            this.login_Login_panel_button.Text = "Login";
            this.login_Login_panel_button.UseVisualStyleBackColor = true;
            this.login_Login_panel_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // donthaveanaacountyet_Login_Form_label
            // 
            this.donthaveanaacountyet_Login_Form_label.AutoSize = true;
            this.donthaveanaacountyet_Login_Form_label.Location = new System.Drawing.Point(267, 39);
            this.donthaveanaacountyet_Login_Form_label.Name = "donthaveanaacountyet_Login_Form_label";
            this.donthaveanaacountyet_Login_Form_label.Size = new System.Drawing.Size(206, 20);
            this.donthaveanaacountyet_Login_Form_label.TabIndex = 7;
            this.donthaveanaacountyet_Login_Form_label.Text = "Don\'t have an account yet ?";
            // 
            // email_Sign_Up_panel_textBox
            // 
            this.email_Sign_Up_panel_textBox.Location = new System.Drawing.Point(208, 77);
            this.email_Sign_Up_panel_textBox.Name = "email_Sign_Up_panel_textBox";
            this.email_Sign_Up_panel_textBox.Size = new System.Drawing.Size(131, 26);
            this.email_Sign_Up_panel_textBox.TabIndex = 8;
            // 
            // username_Sign_Up_panel_textBox
            // 
            this.username_Sign_Up_panel_textBox.Location = new System.Drawing.Point(208, 119);
            this.username_Sign_Up_panel_textBox.Name = "username_Sign_Up_panel_textBox";
            this.username_Sign_Up_panel_textBox.Size = new System.Drawing.Size(131, 26);
            this.username_Sign_Up_panel_textBox.TabIndex = 9;
            // 
            // password_Sign_Up_panel_textBox
            // 
            this.password_Sign_Up_panel_textBox.Location = new System.Drawing.Point(208, 163);
            this.password_Sign_Up_panel_textBox.Name = "password_Sign_Up_panel_textBox";
            this.password_Sign_Up_panel_textBox.Size = new System.Drawing.Size(133, 26);
            this.password_Sign_Up_panel_textBox.TabIndex = 10;
            // 
            // email_Sign_Up_Form_label
            // 
            this.email_Sign_Up_Form_label.AutoSize = true;
            this.email_Sign_Up_Form_label.Location = new System.Drawing.Point(108, 77);
            this.email_Sign_Up_Form_label.Name = "email_Sign_Up_Form_label";
            this.email_Sign_Up_Form_label.Size = new System.Drawing.Size(53, 20);
            this.email_Sign_Up_Form_label.TabIndex = 11;
            this.email_Sign_Up_Form_label.Text = "E-mail";
            // 
            // username_Sign_Up_Form_label
            // 
            this.username_Sign_Up_Form_label.AutoSize = true;
            this.username_Sign_Up_Form_label.Location = new System.Drawing.Point(108, 123);
            this.username_Sign_Up_Form_label.Name = "username_Sign_Up_Form_label";
            this.username_Sign_Up_Form_label.Size = new System.Drawing.Size(83, 20);
            this.username_Sign_Up_Form_label.TabIndex = 12;
            this.username_Sign_Up_Form_label.Text = "Username";
            // 
            // password_Sign_Up_Form_label
            // 
            this.password_Sign_Up_Form_label.AutoSize = true;
            this.password_Sign_Up_Form_label.Location = new System.Drawing.Point(108, 163);
            this.password_Sign_Up_Form_label.Name = "password_Sign_Up_Form_label";
            this.password_Sign_Up_Form_label.Size = new System.Drawing.Size(78, 20);
            this.password_Sign_Up_Form_label.TabIndex = 13;
            this.password_Sign_Up_Form_label.Text = "Password";
            // 
            // new_game_User_panel_button
            // 
            this.new_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.new_game_User_panel_button.Location = new System.Drawing.Point(27, 31);
            this.new_game_User_panel_button.Name = "new_game_User_panel_button";
            this.new_game_User_panel_button.Size = new System.Drawing.Size(413, 110);
            this.new_game_User_panel_button.TabIndex = 15;
            this.new_game_User_panel_button.Text = "New Game";
            this.new_game_User_panel_button.UseVisualStyleBackColor = true;
            this.new_game_User_panel_button.Click += new System.EventHandler(this.new_game_User_Form_button_Click);
            // 
            // resume_game_User_panel_button
            // 
            this.resume_game_User_panel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.resume_game_User_panel_button.Location = new System.Drawing.Point(27, 189);
            this.resume_game_User_panel_button.Name = "resume_game_User_panel_button";
            this.resume_game_User_panel_button.Size = new System.Drawing.Size(413, 52);
            this.resume_game_User_panel_button.TabIndex = 16;
            this.resume_game_User_panel_button.Text = "Resume Game";
            this.resume_game_User_panel_button.UseVisualStyleBackColor = true;
            this.resume_game_User_panel_button.Click += new System.EventHandler(this.resume_game_User_Form_button_Click);
            // 
            // sign_up_Sign_Up_Form_button
            // 
            this.sign_up_Sign_Up_Form_button.Location = new System.Drawing.Point(216, 217);
            this.sign_up_Sign_Up_Form_button.Name = "sign_up_Sign_Up_Form_button";
            this.sign_up_Sign_Up_Form_button.Size = new System.Drawing.Size(100, 34);
            this.sign_up_Sign_Up_Form_button.TabIndex = 18;
            this.sign_up_Sign_Up_Form_button.Text = "Sign Up";
            this.sign_up_Sign_Up_Form_button.UseVisualStyleBackColor = true;
            this.sign_up_Sign_Up_Form_button.Click += new System.EventHandler(this.sign_up_Sign_Up_Form_button_Click_1);
            // 
            // sign_up_Login_panel_button
            // 
            this.sign_up_Login_panel_button.Location = new System.Drawing.Point(278, 71);
            this.sign_up_Login_panel_button.Name = "sign_up_Login_panel_button";
            this.sign_up_Login_panel_button.Size = new System.Drawing.Size(150, 45);
            this.sign_up_Login_panel_button.TabIndex = 19;
            this.sign_up_Login_panel_button.Text = "Sign Up";
            this.sign_up_Login_panel_button.UseVisualStyleBackColor = true;
            this.sign_up_Login_panel_button.Click += new System.EventHandler(this.sign_up_Login_Form_button_Click);
            // 
            // Login_panel
            // 
            this.Login_panel.Controls.Add(this.sign_up_Login_panel_button);
            this.Login_panel.Controls.Add(this.username_Login_panel_textBox);
            this.Login_panel.Controls.Add(this.password_Login_panel_textBox);
            this.Login_panel.Controls.Add(this.username_Login_panel_label);
            this.Login_panel.Controls.Add(this.password_Login_panel_label);
            this.Login_panel.Controls.Add(this.login_Login_panel_button);
            this.Login_panel.Controls.Add(this.donthaveanaacountyet_Login_Form_label);
            this.Login_panel.Location = new System.Drawing.Point(1012, 421);
            this.Login_panel.Name = "Login_panel";
            this.Login_panel.Size = new System.Drawing.Size(474, 199);
            this.Login_panel.TabIndex = 20;
            // 
            // User_panel
            // 
            this.User_panel.Controls.Add(this.connected_users_radioButton);
            this.User_panel.Controls.Add(this.ending_dateTimePicker);
            this.User_panel.Controls.Add(this.initial_dateTimePicker);
            this.User_panel.Controls.Add(this.dataGridView);
            this.User_panel.Controls.Add(this.label3);
            this.User_panel.Controls.Add(this.nombre_usuario_label);
            this.User_panel.Controls.Add(this.query_Button);
            this.User_panel.Controls.Add(this.label2);
            this.User_panel.Controls.Add(this.label1);
            this.User_panel.Controls.Add(this.opponent_textBox);
            this.User_panel.Controls.Add(this.ranking_radioButton);
            this.User_panel.Controls.Add(this.games_sort_date_radioButton);
            this.User_panel.Controls.Add(this.game_results_radioButton);
            this.User_panel.Controls.Add(this.players_list_radioButton);
            this.User_panel.Controls.Add(this.new_game_User_panel_button);
            this.User_panel.Controls.Add(this.resume_game_User_panel_button);
            this.User_panel.Location = new System.Drawing.Point(33, 14);
            this.User_panel.Name = "User_panel";
            this.User_panel.Size = new System.Drawing.Size(1513, 803);
            this.User_panel.TabIndex = 21;
            this.User_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.User_panel_Paint);
            // 
            // connected_users_radioButton
            // 
            this.connected_users_radioButton.AutoSize = true;
            this.connected_users_radioButton.Location = new System.Drawing.Point(111, 539);
            this.connected_users_radioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.connected_users_radioButton.Name = "connected_users_radioButton";
            this.connected_users_radioButton.Size = new System.Drawing.Size(158, 24);
            this.connected_users_radioButton.TabIndex = 31;
            this.connected_users_radioButton.TabStop = true;
            this.connected_users_radioButton.Text = "Connected Users";
            this.connected_users_radioButton.UseVisualStyleBackColor = true;
            // 
            // ending_dateTimePicker
            // 
            this.ending_dateTimePicker.Location = new System.Drawing.Point(546, 472);
            this.ending_dateTimePicker.Name = "ending_dateTimePicker";
            this.ending_dateTimePicker.Size = new System.Drawing.Size(130, 26);
            this.ending_dateTimePicker.TabIndex = 30;
            // 
            // initial_dateTimePicker
            // 
            this.initial_dateTimePicker.Location = new System.Drawing.Point(348, 472);
            this.initial_dateTimePicker.Name = "initial_dateTimePicker";
            this.initial_dateTimePicker.Size = new System.Drawing.Size(133, 26);
            this.initial_dateTimePicker.TabIndex = 29;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(492, 31);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 92;
            this.dataGridView.RowTemplate.Height = 37;
            this.dataGridView.Size = new System.Drawing.Size(559, 371);
            this.dataGridView.TabIndex = 28;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(500, 477);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "to";
            // 
            // nombre_usuario_label
            // 
            this.nombre_usuario_label.AutoSize = true;
            this.nombre_usuario_label.Location = new System.Drawing.Point(675, 483);
            this.nombre_usuario_label.Name = "nombre_usuario_label";
            this.nombre_usuario_label.Size = new System.Drawing.Size(0, 20);
            this.nombre_usuario_label.TabIndex = 24;
            // 
            // query_Button
            // 
            this.query_Button.Location = new System.Drawing.Point(64, 608);
            this.query_Button.Name = "query_Button";
            this.query_Button.Size = new System.Drawing.Size(145, 31);
            this.query_Button.TabIndex = 23;
            this.query_Button.Text = "Send Query";
            this.query_Button.UseVisualStyleBackColor = true;
            this.query_Button.Click += new System.EventHandler(this.query_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "QUERIES:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(389, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(622, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "write the username of the opponent you want to consult the games with";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // opponent_textBox
            // 
            this.opponent_textBox.Location = new System.Drawing.Point(270, 435);
            this.opponent_textBox.Name = "opponent_textBox";
            this.opponent_textBox.Size = new System.Drawing.Size(100, 26);
            this.opponent_textBox.TabIndex = 19;
            // 
            // ranking_radioButton
            // 
            this.ranking_radioButton.AutoSize = true;
            this.ranking_radioButton.Location = new System.Drawing.Point(111, 503);
            this.ranking_radioButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ranking_radioButton.Name = "ranking_radioButton";
            this.ranking_radioButton.Size = new System.Drawing.Size(93, 24);
            this.ranking_radioButton.TabIndex = 20;
            this.ranking_radioButton.TabStop = true;
            this.ranking_radioButton.Text = "Ranking";
            this.ranking_radioButton.UseVisualStyleBackColor = true;
            // 
            // games_sort_date_radioButton
            // 
            this.games_sort_date_radioButton.AutoSize = true;
            this.games_sort_date_radioButton.Location = new System.Drawing.Point(111, 471);
            this.games_sort_date_radioButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.games_sort_date_radioButton.Name = "games_sort_date_radioButton";
            this.games_sort_date_radioButton.Size = new System.Drawing.Size(231, 24);
            this.games_sort_date_radioButton.TabIndex = 19;
            this.games_sort_date_radioButton.TabStop = true;
            this.games_sort_date_radioButton.Text = "Games sorted by date from ";
            this.games_sort_date_radioButton.UseVisualStyleBackColor = true;
            // 
            // game_results_radioButton
            // 
            this.game_results_radioButton.AutoSize = true;
            this.game_results_radioButton.Location = new System.Drawing.Point(111, 437);
            this.game_results_radioButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.game_results_radioButton.Name = "game_results_radioButton";
            this.game_results_radioButton.Size = new System.Drawing.Size(161, 24);
            this.game_results_radioButton.TabIndex = 18;
            this.game_results_radioButton.TabStop = true;
            this.game_results_radioButton.Text = "Game results with";
            this.game_results_radioButton.UseVisualStyleBackColor = true;
            // 
            // players_list_radioButton
            // 
            this.players_list_radioButton.AutoSize = true;
            this.players_list_radioButton.Location = new System.Drawing.Point(111, 405);
            this.players_list_radioButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.players_list_radioButton.Name = "players_list_radioButton";
            this.players_list_radioButton.Size = new System.Drawing.Size(112, 24);
            this.players_list_radioButton.TabIndex = 17;
            this.players_list_radioButton.TabStop = true;
            this.players_list_radioButton.Text = "Players list ";
            this.players_list_radioButton.UseVisualStyleBackColor = true;
            // 
            // Sign_Up_panel
            // 
            this.Sign_Up_panel.Controls.Add(this.sign_up_Sign_Up_Form_button);
            this.Sign_Up_panel.Controls.Add(this.password_Sign_Up_Form_label);
            this.Sign_Up_panel.Controls.Add(this.username_Sign_Up_Form_label);
            this.Sign_Up_panel.Controls.Add(this.email_Sign_Up_Form_label);
            this.Sign_Up_panel.Controls.Add(this.password_Sign_Up_panel_textBox);
            this.Sign_Up_panel.Controls.Add(this.username_Sign_Up_panel_textBox);
            this.Sign_Up_panel.Controls.Add(this.email_Sign_Up_panel_textBox);
            this.Sign_Up_panel.Location = new System.Drawing.Point(1012, 92);
            this.Sign_Up_panel.Name = "Sign_Up_panel";
            this.Sign_Up_panel.Size = new System.Drawing.Size(474, 310);
            this.Sign_Up_panel.TabIndex = 22;
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 724);
            this.Controls.Add(this.User_panel);
            this.Controls.Add(this.Sign_Up_panel);
            this.Controls.Add(this.Login_panel);
            this.Name = "Battleship";
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.Battleship_Load);
            this.Login_panel.ResumeLayout(false);
            this.Login_panel.PerformLayout();
            this.User_panel.ResumeLayout(false);
            this.User_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.Sign_Up_panel.ResumeLayout(false);
            this.Sign_Up_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox username_Login_panel_textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox password_Login_panel_textBox;
        private System.Windows.Forms.Label username_Login_panel_label;
        private System.Windows.Forms.Label password_Login_panel_label;
        private System.Windows.Forms.Button login_Login_panel_button;
        private System.Windows.Forms.Label donthaveanaacountyet_Login_Form_label;
        private System.Windows.Forms.TextBox email_Sign_Up_panel_textBox;
        private System.Windows.Forms.TextBox username_Sign_Up_panel_textBox;
        private System.Windows.Forms.TextBox password_Sign_Up_panel_textBox;
        private System.Windows.Forms.Label email_Sign_Up_Form_label;
        private System.Windows.Forms.Label username_Sign_Up_Form_label;
        private System.Windows.Forms.Label password_Sign_Up_Form_label;
        private System.Windows.Forms.Button new_game_User_panel_button;
        private System.Windows.Forms.Button resume_game_User_panel_button;
        private System.Windows.Forms.Button sign_up_Sign_Up_Form_button;
        private System.Windows.Forms.Button sign_up_Login_panel_button;
        private System.Windows.Forms.Panel Login_panel;
        private System.Windows.Forms.Panel User_panel;
        private System.Windows.Forms.Panel Sign_Up_panel;
        private System.Windows.Forms.TextBox opponent_textBox;
        private System.Windows.Forms.RadioButton ranking_radioButton;
        private System.Windows.Forms.RadioButton games_sort_date_radioButton;
        private System.Windows.Forms.RadioButton game_results_radioButton;
        private System.Windows.Forms.RadioButton players_list_radioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button query_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label nombre_usuario_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DateTimePicker initial_dateTimePicker;
        private System.Windows.Forms.DateTimePicker ending_dateTimePicker;
        private System.Windows.Forms.RadioButton connected_users_radioButton;
    }
}

