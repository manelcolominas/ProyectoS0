﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Battleship
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        string username_of_the_connection;
        private static Dictionary<int, Form2> instanciasForm2 = new Dictionary<int, Form2>();

        public Form1()
        {
            InitializeComponent();
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;
            connect_server_button.Visible= true;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //función que escucha constantemente al servidor y procesa sus mensajes
        private void AtenderServidor()
        {
            while (true) 
            {
                // Receive the message from the server
                byte[] responseData = new byte[256];
                int bytesReceived = server.Receive(responseData);
                string [] trozos_respuesta = Encoding.ASCII.GetString(responseData, 0, bytesReceived).Split('*');
                int codigo = Convert.ToInt32(trozos_respuesta[0]);
                string response = trozos_respuesta[1].Split('\0')[0];

                switch (codigo)
                {
                    case 1: //sign up
                        MessageBox.Show("Server response: " + response);
                        if (response.Contains("6/1"))
                        {
                            MessageBox.Show("Sign Up successful!");
                            show_Panel(login_panel);

                        }
                        else if (response.Contains("6/0"))
                        {
                            MessageBox.Show("Username already taken.");
                        }
                        break;
                    case 2: //log in
                        MessageBox.Show("Server response: " + response);
                        // Check the server response for login success
                        if (response.Contains("1/1"))
                        {
                            MessageBox.Show("Login successful!");
                            string username = login_panel_username_textBox.Text;
                            set_username(username);
                            nombre_usuario_label.Text = username;
                            this.Invoke(new Action(() => { mostrar_user_panel(response); }));
                        }
                        else if (response.Contains("1/2"))
                        {
                            MessageBox.Show("User already connected");
                        }
                        else if (response.Contains("1/3"))
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                        break;
                    case 3: //list of games // eseña a lista de partidas realizadas del usuaio con todos sus oponentes
                        string[] trozos = response.Split('/');
                        if (trozos[0] == "3")
                        {
                            MessageBox.Show("No se encontraron oponentes");
                        }
                        else 
                        {
                            MessageBox.Show("Server response:" + response);
                            this.Invoke(new Action(() => { ProcessOpponentList(response); }));
                        }
                        break;
                    case 4: //consultar partidas hechas con un cierto oponente
                        MessageBox.Show("Server response: " + response);
                        this.Invoke(new Action(() => { ShowGamesWithOpponent(response); }));
                        break;
                    case 5: //show games   
                        MessageBox.Show("Server response: " + response);
                        this.Invoke(new Action(() => { ShowGamesWithOpponent(response); }));
                        break;
                    case 6: //show ranking
                        this.Invoke(new Action(() => { ProcessRankingResults(response); }));
                        break;
                    case 7:
                        this.Invoke(new Action(() => {handle_game_invitation(trozos_respuesta); }));                
                        break;

                    case 9: //notificacion usuarios conectados
                        string[] cachos = response.Split('/');
                        connected_users_label.Text = "";
                        foreach (string s in cachos)
                        {
                            connected_users_label.Text += s + Environment.NewLine; // Añade el texto y un salto de línea
                        }
                        break;
                    case 8:
                        MessageBox.Show("Disconnected from server");
                        set_username(string.Empty);
                        break;
                    case 10:
                        this.Invoke(new Action(() => {create_game(response); }));
                        break;
                    case 14:
                        string[] elementos_respuesta = trozos_respuesta[1].Split('/');
                        int ID_game = Convert.ToInt32(elementos_respuesta[2]);
                        if (instanciasForm2.TryGetValue(ID_game, out Form2 instancia))
                        {
                            this.Invoke(new Action(() => { instancia.handle_game(trozos_respuesta[1]); }));
                            //instancia.handle_game(trozos_respuesta[1]);
                        }
                        break;
                }
                //if (codigo == 8)
                //    break;
            }
        }
        private void set_username(string username)
        {
            this.username_of_the_connection = username;
        }
        private string get_username()
        {
            return this.username_of_the_connection;
        }
        private void show_Panel(Panel panel)
        {
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;

            panel.Visible = true;
        }
        private void mostrar_user_panel(string response)
        {
            show_Panel(user_panel);
        }

        private void handle_game_invitation(string[] trozos_respuesta)
        {
            string username = get_username();
            string[] opponent = trozos_respuesta[1].Split('/');
            if (opponent[1] == "1") // Invitacions
            {
                DialogResult respuesta = MessageBox.Show("Do you accept ?",
                    $"User {opponent[2]} invites you to play a game.",
                        MessageBoxButtons.YesNo // Icono
                    );
                string serviceCode = "7";  //
                string type_of_message = "2"; // Respostes

                if (respuesta == DialogResult.Yes)
                {
                    string bool_respuesta = "1"; // Afirmatiu
                    DateTime date = DateTime.Now;

                    string formatdate = date.ToString("yyyy-MM-dd");
                    string data_to_send = $"{serviceCode}/{type_of_message}/{bool_respuesta}/{username}/{opponent[2]}/{formatdate}";
                    byte[] data = Encoding.ASCII.GetBytes(data_to_send);
                    server.Send(data);

                }
                else if (respuesta == DialogResult.No)
                {
                    string bool_respuesta = "0"; // negatiu
                    string data_to_send = $"{serviceCode}/{type_of_message}/{bool_respuesta}/{username}/{opponent[2]}";
                    byte[] data = Encoding.ASCII.GetBytes(data_to_send);
                    server.Send(data);
                }
            }
            else if (opponent[1] == "2")
            {
                if (opponent[2] == "1")
                {
                    MessageBox.Show($"{opponent[3]} has accept your game invitation.");
                }
                else if (opponent[2] == "0")
                {
                    MessageBox.Show($"{opponent[3]} has not accept your game invitation.");
                }
                else if (opponent[2] == "2")
                {
                    MessageBox.Show($"{opponent[3]} is not connected.");
                }
            }
        }

        private void login_panel_signup_button_Click(object sender, EventArgs e)
        {
            show_Panel(signup_panel);
        }

        private void signup_panel_signup_button_Click(object sender, EventArgs e)
        {
            //Get the username, password and email from the TextBoxes
            string username = signup_panel_username_textBox.Text;
            string password = signup_panel_password_textBox.Text;
            string email = signup_panel_email_textBox.Text;
            string serviceCode = "1";  // 1 for signup

            // Prepare the signup data using ASCII encoding
            string signupData = $"{serviceCode}/{username}/{email}/{password}";
            byte[] data = Encoding.ASCII.GetBytes(signupData);

            // Send the signup data to the server
            server.Send(data);
            MessageBox.Show("Signup data sent: " + signupData);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string username = nombre_usuario_label.Text;
            string username = get_username();
            // Verifica si el evento se dispara y si el usuario está conectado
            MessageBox.Show("Cerrando el formulario...");

            if (!string.IsNullOrEmpty(username))
            {
                string exitdata = $"8/{username}";
                byte[] data = Encoding.ASCII.GetBytes(exitdata);
                server.Send(data);  // Enviar al servidor
                MessageBox.Show("Mensaje enviado: " + exitdata);  // Verifica el mensaje enviado
            }

            // Cerrar la conexión con el servidor
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void desconnect_server__button_Click(object sender, EventArgs e)
        {
            string username = get_username();
            MessageBox.Show("Desconectando...");

            // Si el usuario está conectado, enviar el mensaje de desconexión al servidor
            if (!string.IsNullOrEmpty(username))
            {
                string exitdata = $"8/{username}";
                byte[] data = Encoding.ASCII.GetBytes(exitdata);
                server.Send(data);  // Enviar al servidor
                MessageBox.Show("Mensaje enviado: " + exitdata);  // Verifica el mensaje enviado
            }

            //aqui no cerramos la conexion con el servidor, pues queremos aprovechar que ya esta la conexion hecha y solo cambiar de user
            if (Application.OpenForms["Form2"] != null)
            {
                // Obtener una referencia a Form2
                Form formulario2 = Application.OpenForms["Form2"];

                // Cerrar Form2
                formulario2.Close();
            }

            // Restablecer la interfaz a su estado inicial
            login_panel_username_textBox.Text = "";
            login_panel_password_textBox.Text = "";
            nombre_usuario_label.Text = "";
            set_username(string.Empty);
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;
            show_Panel(login_panel);
            this.BackColor = Color.Gray;
        }

        private void login_panel_login_button_Click(object sender, EventArgs e)
        {
            string username = login_panel_username_textBox.Text;
            //set_username(username);
            //nombre_usuario_label.Text = username;
            string password = login_panel_password_textBox.Text;

            // Prepare the login data using ASCII encoding
            string serviceCode = "2";  // 2 for login
            string loginData = $"{serviceCode}/{username}/{password}";
            byte[] data = Encoding.ASCII.GetBytes(loginData);

            // Send the login data to the server
            server.Send(data);
            MessageBox.Show("Login data sent: " + loginData);
        }

        private void ShowGamesWithOpponent(string data)
        {
            // Clear previous data
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Set up the columns
            dataGridView.Columns.Add("ID Game", "ID Game");
            dataGridView.Columns.Add("Player1", "Player 1");
            dataGridView.Columns.Add("Player2", "Player 2");
            dataGridView.Columns.Add("PointsPlayer1", "Points Player 1");
            dataGridView.Columns.Add("PointsPlayer2", "Points Player 2");
            dataGridView.Columns.Add("Start Time", "Start Time");
            dataGridView.Columns.Add("End Time", "End Time");

            // Split the data into rows based on newlines
            var rows = data.Trim().Split('\n');

            // Parse each row and add to the DataGridView
            foreach (var row in rows)
            {
                var columns = row.Split('/'); // Split fields based on '/'
                if (columns.Length == 7)
                {
                    dataGridView.Rows.Add(columns[0],columns[1], columns[2], columns[3], columns[4], columns[5], columns[6]);
                }
            }
        }

        private void ProcessRankingResults(string response)
        {
            // Clear previous data
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Set up the columns
            dataGridView.Columns.Add("Player", "Players");
            dataGridView.Columns.Add("TotalPoints", "Total Points");

            // Split the data into rows based on newlines
            var rows = response.Trim().Split('\n');

            // Parse each row and add to the DataGridView
            foreach (var row in rows)
            {
                var columns = row.Split('/'); // Split fields based on '/'
                if (columns.Length == 2)
                {
                    dataGridView.Rows.Add(columns[0], columns[1]);
                }
            }
        }
        private void ProcessOpponentList(string response)
        {
            // Clear previous data
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Set up a single column for opponents
            dataGridView.Columns.Add("Opponent", "Opponents");

            // Split the data into opponents based on '/'
            var opponents = response.Trim().Split('/');

            // Add each opponent to the DataGridView
            foreach (var opponent in opponents)
            {
                if (!string.IsNullOrWhiteSpace(opponent))  // Ensure the value is not empty
                {
                    dataGridView.Rows.Add(opponent);
                }
            }
        }

        private void query_Button_Click(object sender, EventArgs e)
        {
            if (players_list_radioButton.Checked)
            {
                //quiere consultar la lista de jugadores con los que ha jugado una partida al menos una vez

                string username = nombre_usuario_label.Text;
                string mensaje = $" 3/{username}";
                MessageBox.Show("data sent:" + mensaje);
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);              
            }

            else if (game_results_radioButton.Checked)
            {
                //quiere consultar los resultados de las partidas contra un oponente que se ingresa por el textbox

                string username = nombre_usuario_label.Text;
                string opponent = opponent_textBox.Text;
                string mensaje = $"4/{username}/{opponent}";
                // Send the message to the server
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                MessageBox.Show("data sent:" + mensaje);
                server.Send(msg); // Sending the message using server.Client.Send

                // Enviar al servidor el mensaje
            }

            else if (games_sort_date_radioButton.Checked)
            {
                // User input
                string username = nombre_usuario_label.Text;

                // Assuming you have DateTimePicker controls named 'initialDatePicker' and 'endingDatePicker'
                DateTime initial_date = initial_dateTimePicker.Value; // Get the selected initial date
                DateTime ending_date = ending_dateTimePicker.Value;    // Get the selected ending date

                // Format the dates to a string suitable for your server (e.g., YYYY-MM-DD)
                string formattedInitialDate = initial_date.ToString("yyyy-MM-dd");
                string formattedEndingDate = ending_date.ToString("yyyy-MM-dd");

                string mensaje = $"5/{username}/{formattedInitialDate}/{formattedEndingDate}";
                MessageBox.Show("data sent:" + mensaje);
                
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else if (ranking_radioButton.Checked)
            {
                // Sort ranking code to ask the query
                string mensaje = "6"; // Operation code for showing rankings

                // Send the message to the server
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                
                MessageBox.Show("data sent: " + mensaje);   
            }
            else if (game_invitation_radioButton.Checked)
            {
                string username = nombre_usuario_label.Text;
                string opponent = game_invitation_textBox.Text;

                string serviceCode = "7"; 
                string type_of_message = "1"; // Invitacions 
                string data_to_send = $"{serviceCode}/{type_of_message}/{username}/{opponent}";
                // Send the message to the server
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data_to_send);
                server.Send(msg);
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            show_Panel(login_panel);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void connect_server_button_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 8050);
            //IPAddress direc = IPAddress.Parse("10.4.119.5");
            //IPEndPoint ipep = new IPEndPoint(direc, 50035);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Intentar conectar al servidor
            try
            {
                server.Connect(ipep);
                MessageBox.Show("Connection successful", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                show_Panel(login_panel);
                connect_server_button.Visible= false;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //pongo en marcha el thread que atenderá los mensajes del servidor 
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

            CheckForIllegalCrossThreadCalls = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void new_game_User_panel_button_Click(object sender, EventArgs e)
        {
            //int ID_game = 18;
            //string username = "manel007";
            //string opponent = "esther789";
            //Form2 gameform = new Form2(ID_game,server, username, opponent);
            //instanciasForm2.Add(ID_game, gameform);
            //gameform.Show();
        }

        //private void create_game(string response)
        private void create_game(string response)
        {
            //    "/17/manel007/esther789"
            string[] trozos_respuesta = response.Split('/');

            int ID_game = Convert.ToInt32(trozos_respuesta[1]);
            string username = trozos_respuesta[2];
            string opponent = trozos_respuesta[3];
            Form2 gameform = new Form2(ID_game,server,username,opponent);
            instanciasForm2.Add(ID_game, gameform);
            gameform.Show();
        }

    }
}
