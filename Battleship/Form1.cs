using System;
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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Battleship
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
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
                    case 3: //list of games
                        string[] trozos = response.Split('/');
                        if (trozos[0] == "3")
                        {
                            MessageBox.Show("No se encontraron oponentes")
                            ;
                        }
                        else {
                            MessageBox.Show("Server response:" + response);
                            ProcessOpponentList(response);
                            // HE CREAT UN ALTRE DATAGRIDVIEW
                        }
                        break;
                    case 4: //consultar oponente
                        MessageBox.Show("Server response: " + response);
                        // Procesar la respuesta para llenar el DataGridView
                        ProcessGameResults(response);
                        //es pot veure q no fem servir la mateixa funcio q pels oponents xq la resposta ara te 7 columnes
                        break;
                    case 5: //show games 
                        // Process the response         
                        MessageBox.Show("Server response: " + response);
                        ProcessGameResults(response);
                        // Close the connection
                        server.Close();
                        // HE CREAT UN ALTRE DATAGRIDVIEW
                        break;
                    case 6: //show ranking
                        // Process the response to fill the DataGridView
                        MessageBox.Show("Server response: " + response);
                        ProcessRankingResults(response);
                        break;
                    case 7:
                        string[] opponent = trozos_respuesta[1].Split('/');
                        if (opponent[1] == "1") // Invitacions
                        { 
                                DialogResult respuesta = MessageBox.Show("Do you accept ?",
                                    $"User {opponent[2]} invites you to play a game.",
                                        MessageBoxButtons.YesNo // Icono
                                    );
                                string username = nombre_usuario_label.Text;
                                string serviceCode = "7";  //
                                string type_of_message = "2"; // Respostes

                                if (respuesta == DialogResult.Yes)
                                {
                                    string bool_respuesta = "1"; // Afirmatiu
                                    string data_to_send = $"{serviceCode}/{type_of_message}/{bool_respuesta}/{username}/{opponent[2]}";
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
                        else if (opponent[1] == "2") {
                            if (opponent[2] == "1")
                            {
                                MessageBox.Show($"{opponent[3]} has accept your game invitation.");
                                // Aquí s'hauria d'obrir la interficie de joc i començara jugar
                            }
                            else if (opponent[2] == "0") { 
                            }
                            MessageBox.Show($"{opponent[3]} has not accept your game invitation.");
                            }
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
                        break;


                }
                if (codigo == 8)
                    break;
            }
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
            string username = nombre_usuario_label.Text;

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
            string username = nombre_usuario_label.Text;

            // Verifica si el evento se dispara y si el usuario está conectado
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

            // Restablecer la interfaz a su estado inicial
            login_panel_username_textBox.Text = "";
            login_panel_password_textBox.Text = "";
            nombre_usuario_label.Text = "";
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;
            show_Panel(login_panel);
            this.BackColor = Color.Gray;
        }

        private void login_panel_login_button_Click(object sender, EventArgs e)
        {
            //Get the username and password from the TextBoxes
            string username = login_panel_username_textBox.Text;
            nombre_usuario_label.Text = username;
            string password = login_panel_password_textBox.Text;

            // Prepare the login data using ASCII encoding
            string serviceCode = "2";  // 2 for login
            string loginData = $"{serviceCode}/{username}/{password}";
            byte[] data = Encoding.ASCII.GetBytes(loginData);

            // Send the login data to the server
            server.Send(data);
            MessageBox.Show("Login data sent: " + loginData);
        }


        private DataGridView playersDataGridView;

        //inicializamos el datagrid para mostrar las diferentes partidas qcon un mismo oponente
        private void InitializeDataGridView()
        {
            // Agregar columnas al DataGridView
            playersDataGridView.Columns.Add("IdPartida", "ID Partida");
            playersDataGridView.Columns.Add("Jugador1", "Jugador 1");
            playersDataGridView.Columns.Add("Jugador2", "Jugador 2");
            playersDataGridView.Columns.Add("PuntosJugador1", "Puntos Jugador 1");
            playersDataGridView.Columns.Add("PuntosJugador2", "Puntos Jugador 2");
            playersDataGridView.Columns.Add("Inicio", "Inicio");
            playersDataGridView.Columns.Add("Final", "Final");

            playersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        //función que recibe el mensaje del servidor, lo procesa y lo muestra en el datagridview
        private void ProcessGameResults(string response)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            if (response == "No games found.\n" || response == "No games found between the players.\n")
            {
                MessageBox.Show(response.Trim(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView.Columns.Add("GameID", "Game ID");
            dataGridView.Columns.Add("Player1", "Player 1");
            dataGridView.Columns.Add("Player2", "Player 2");
            dataGridView.Columns.Add("Player1Points", "Player 1 Points");
            dataGridView.Columns.Add("Player2Points", "Player 2 Points");
            dataGridView.Columns.Add("StartTime", "Start Time");
            dataGridView.Columns.Add("EndTime", "End Time");

            string[] lines = response.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 7)
                {
                    string idPartida = parts[0].Trim();
                    string jugador1 = parts[1].Trim();
                    string jugador2 = parts[2].Trim();
                    string puntosJugador1 = parts[3].Trim();
                    string puntosJugador2 = parts[4].Trim();
                    string inicio = parts[5].Trim();
                    string final = parts[6].Trim();

                    dataGridView.Rows.Add(idPartida, jugador1, jugador2, puntosJugador1, puntosJugador2, inicio, final);
                }
            }
        }

        private void ProcessRankingResults(string response)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            if (response == "No rankings available.\n")
            {
                MessageBox.Show(response.Trim(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView.Columns.Add("Username", "Username");
            dataGridView.Columns.Add("TotalPoints", "Total Points");

            string[] lines = response.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    string username = parts[0].Trim();
                    string totalPoints = parts[1].Trim();

                    dataGridView.Rows.Add(username, totalPoints);
                }
            }
        }

        //per la llista d'oponents fem una funcio nova xq nomes te una columna la resposta del servidor

        private void ProcessOpponentList(string response)
        {
            // Limpiar las filas y columnas del DataGridView antes de agregar nuevos datos
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Agregar una columna para mostrar los nombres de los oponentes
            dataGridView.Columns.Add("Oponente", "Oponente");

            // Dividir la respuesta en líneas (cada línea es un nombre de oponente)
            string[] opponents = response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Agregar cada oponente al DataGridView como una fila
            foreach (string opponent in opponents)
            {
                dataGridView.Rows.Add(opponent.Trim());
            }
        }

        private void query_Button_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear(); //limpiamos el data grid donde se mostraran las partidas contra un oponente


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
                MessageBox.Show("data sent:" + mensaje);
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

                // Prepare the message to send
                string mensaje = $"5/{username}/{formattedInitialDate}/{formattedEndingDate}";
                MessageBox.Show("data sent:" + mensaje);
                // Send the message to the server
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg); // Sending the message using server.Client.Send

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

                string serviceCode = "7";  // 1 for signup
                string type_of_message = "1"; // Invitacions
                string data_to_send = $"{serviceCode}/{type_of_message}/{username}/{opponent}";
                // Send the message to the server
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data_to_send);
                server.Send(msg);
            }
        }

        

        private void user_panel_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
