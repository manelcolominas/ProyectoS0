using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Battleship : Form
    {
        String username, password, reply;
        Socket server;
        public Battleship()
        {
            InitializeComponent();
            show_Panel(Login_panel);
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc,9080);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the server
            server.Connect(ipep);
        }

        private void show_Panel(Panel panel)
        {
            Login_panel.Visible = false;
            Sign_Up_panel.Visible = false;
            User_panel.Visible = false;

            panel.Visible = true;
        }

        // COM EXTRUCTURAREM ELS NOMS DELS BUTONS ?
        // el text que conté el butó (en MINÚSCULAS)  +_+ Nom del formulari al qual pertanyen (User/Sign_Up/Login) +_+Form+_+button/label
        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button

        private void login_button_Click(object sender, EventArgs e)
        {
            // Define the server address and port
            //IPAddress direc = IPAddress.Parse("192.168.56.102");
            //IPEndPoint ipep = new IPEndPoint(direc, 9080);

            //server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //// Connect to the server
            //server.Connect(ipep);
            this.BackColor = Color.Green; // Change the background color if the connection is successful

            // Get the username and password from the TextBoxes
            string username = username_Login_panel_textBox.Text;
            nombre_usuario_label.Text = username;
            string password = password_Login_panel_textBox.Text;

            // Prepare the login data using ASCII encoding
            string serviceCode = "1";  // 1 for login
            string loginData = $"{serviceCode}/{username}/{password}";
            byte[] data = Encoding.ASCII.GetBytes(loginData);

            // Send the login data to the server
            server.Send(data);
            Console.WriteLine("Login data sent: " + loginData);

            // Read the response from the server
            byte[] responseData = new byte[256];
            int bytesReceived = server.Receive(responseData);
            string response = Encoding.ASCII.GetString(responseData, 0, bytesReceived);
            Console.WriteLine("Server response: " + response);

            // Check the server response for login success
            if (response.Contains("SUCCESS"))
            {
                MessageBox.Show("Login successful!");
                show_Panel(User_panel);
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        // el text que conté el butó (en MINÚSCULAS)+_+ Nom del formulari al qual pertanyen (User/Sign_Up/Login) +_+Form+_+button/label
        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button
        private void Battleship_Load(object sender, EventArgs e)
        {

        }
        // el text que conté el butó (en MINÚSCULAS)+_+ Nom del formulari al qual pertanyen (User/Sign_Up/Login/Game) +_+Form+_+button/label
        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button
        private void sign_up_Login_Form_button_Click(object sender, EventArgs e)
        {
            show_Panel(Sign_Up_panel);
        }

        // el text que conté el butó (en MINÚSCULAS)+_+ Nom del formulari al qual pertanyen (User/Sign_Up/Login) +_+Form+_+button/label
        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button
        private void new_game_User_Form_button_Click(object sender, EventArgs e)
        {
            // Començar una partida nova
        }

        private void players_list_checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
       
            // Dividir la respuesta en líneas
            string[] lines = response.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                // Suponiendo que cada línea está bien formateada
                string[] parts = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 7) // Verifica que haya 7 partes
                {
                    // Extraer los valores
                    string idPartida = parts[0].Split(':')[1].Trim();
                    string jugador1 = parts[1].Split(':')[1].Trim();
                    string jugador2 = parts[2].Split(':')[1].Trim();
                    string puntosJugador1 = parts[3].Split(':')[1].Trim();
                    string puntosJugador2 = parts[4].Split(':')[1].Trim();
                    string inicio = parts[5].Split(':')[1].Trim();
                    string final = parts[6].Split(':')[1].Trim();

                    // Agregar una nueva fila al DataGridView
                    playersDataGridView.Rows.Add(idPartida, jugador1, jugador2, puntosJugador1, puntosJugador2, inicio, final);
                }
            }
            // HE CREAT UN ALTRE DATAGRIDVIEW
        }


        private void query_Button_Click(object sender, EventArgs e)
        {
            playersDataGridView.Rows.Clear(); //limpiamos el data grid donde se mostraran las partidas contra un oponente


            if (players_list_radioButton.Checked)
            {
                //quiere consultar la lista de jugadores con los que ha jugado una partida al menos una vez
                
                string username = nombre_usuario_label.Text;
                string mensaje = $" 3/{username}";

                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                ProcessGameResults(mensaje);
                // HE CREAT UN ALTRE DATAGRIDVIEW
            }

            else if (game_results_radioButton.Checked)
            {
               //quiere consultar los resultados de las partidas contra un oponente que se ingresa por el textbox
                
                string username = nombre_usuario_label.Text;
                string opponent = opponent_textBox.Text;
                string mensaje = $"4/{username}/{opponent}";

                // Enviar al servidor el mensaje

                // Recibir la respuesta del servidor
                byte[] msg2 = new byte[512];  // Cambié el tamaño para permitir más datos
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                // Procesar la respuesta para llenar el DataGridView
                ProcessGameResults(mensaje);
                // HE CREAT UN ALTRE DATAGRIDVIEW
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

                // Send the message to the server
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg); // Sending the message using server.Client.Send

                // Receive the response from the server
                byte[] msg2 = new byte[512];  // Size for receiving data
                int bytesRead = server.Receive(msg2); // Receiving the response

                // Process the response
                string response = Encoding.ASCII.GetString(msg2, 0, bytesRead);
                ProcessGameResults(response);

                // Close the connection
                server.Close();

                // HE CREAT UN ALTRE DATAGRIDVIEW
            }
            else if (ranking_radioButton.Checked)
            {
                // Sort ranking code to ask the query
                string mensaje = "6"; // Operation code for showing rankings

                // Send the message to the server
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Prepare to receive the response from the server
                byte[] msg2 = new byte[512];  // Increased size for more data
                int bytesReceived = server.Receive(msg2);

                // Convert the received bytes to a string
                mensaje = Encoding.ASCII.GetString(msg2, 0, bytesReceived).TrimEnd('\0');

                // Process the response to fill the DataGridView
                ProcessGameResults(mensaje);
                // HE CREAT UN ALTRE DATAGRIDVIEW
            }
        }


        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button
        private void resume_game_User_Form_button_Click(object sender, EventArgs e)
        {
            // reanudar una partida
        }
        // el text que conté el butó (en MINÚSCULAS)+_+ Nom del formulari al qual pertanyen (User/Sign_Up/Login) +_+Form+_+button/label
        // login_Login_Form_button
        // sign_up_Login_Form_button
        // resume_game_User_Form_button
        // new_game_User_Form_button
        // sign_up_Sign_Up_Form_button
        private void sign_up_Sign_Up_Form_button_Click_1(object sender, EventArgs e)
        {
            // Define the server address and port
            //IPAddress direc = IPAddress.Parse("192.168.56.102");
            //IPEndPoint ipep = new IPEndPoint(direc, 9080);

            //server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //// Connect to the server
            //server.Connect(ipep);
            this.BackColor = Color.Green; // Change the background color if the connection is successful

            // Step 1: Choose a service, e.g., "2" for signup
            string serviceCode = "2";  // 1 for login, 2 for signup

            // Get the username from the TextBox
            string username = username_Sign_Up_panel_textBox.Text;

            // Get the email from the TextBox
            string email = email_Sign_Up_panel_textBox.Text;

            // Get the password from the TextBox
            string password = password_Sign_Up_panel_textBox.Text;

            // Prepare the signup data using ASCII encoding
            string signupData = $"{serviceCode}/{username}/{email}/{password}";
            byte[] data = Encoding.ASCII.GetBytes(signupData);

            // Send the signup data to the server
            server.Send(data);
            Console.WriteLine("Signup data sent: " + signupData);

            // Read the response from the server
            byte[] responseData = new byte[256];
            int bytes = server.Receive(responseData);
            string response = Encoding.ASCII.GetString(responseData, 0, bytes);
            Console.WriteLine("Server response: " + response);

            // Check the server response for username availability
            if (response.Contains("ERROR: Username already taken"))
            {
                MessageBox.Show("This username is not available.");
            }
            else
            {
                // Show the success response
                MessageBox.Show("Server response: " + response);
                show_Panel(Login_panel);

            }

            //// Close the socket connection
            //server.Shutdown(SocketShutdown.Both);
            //server.Close();
            //show_Panel(Login_panel);
        }
    }
}
