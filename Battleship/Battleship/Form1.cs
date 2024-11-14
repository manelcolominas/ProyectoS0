using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        public Form1()
        {
            InitializeComponent();
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;
            show_Panel(login_panel);

            //IPAddress direc = IPAddress.Parse("192.168.56.102");
            //IPEndPoint ipep = new IPEndPoint(direc, 8020);

            //server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //// Intentar conectar al servidor
            //try
            //{
            //    server.Connect(ipep);
            //    MessageBox.Show("Connection successful", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (SocketException ex)
            //{
            //    MessageBox.Show("Connection failed: " + ex.Message, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        private void show_Panel(Panel panel)
        {
            login_panel.Visible = false;
            signup_panel.Visible = false;
            user_panel.Visible = false;

            panel.Visible = true;
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
            Console.WriteLine("Signup data sent: " + signupData);

            // Read the response from the server
            byte[] responseData = new byte[256];
            int bytes = server.Receive(responseData);
            string response = Encoding.ASCII.GetString(responseData, 0, bytes);
            Console.WriteLine("Server response: " + response);
            if (response.Contains("6/0"))
            {
                MessageBox.Show("Sign Up successful!");
                show_Panel(login_panel);
    
            }
            else if(response.Contains("6/1"))
            {
                MessageBox.Show("Username already taken.");
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string username = nombre_usuario_label.Text;

            // Verifica si el evento se dispara y si el usuario está conectado
            Console.WriteLine("Cerrando el formulario...");

            if (!string.IsNullOrEmpty(username))
            {
                string exitdata = $"8/{username}";
                byte[] data = Encoding.ASCII.GetBytes(exitdata);
                server.Send(data);  // Enviar al servidor
                Console.WriteLine("Mensaje enviado: " + exitdata);  // Verifica el mensaje enviado
            }

            // Cerrar la conexión con el servidor
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }



        private void desconnect_server__button_Click(object sender, EventArgs e)
        {
            string username = nombre_usuario_label.Text;

            // Verifica si el evento se dispara y si el usuario está conectado
            Console.WriteLine("Desconectando...");

            // Si el usuario está conectado, enviar el mensaje de desconexión al servidor
            if (!string.IsNullOrEmpty(username))
            {
                string exitdata = $"8/{username}";
                byte[] data = Encoding.ASCII.GetBytes(exitdata);
                server.Send(data);  // Enviar al servidor
                Console.WriteLine("Mensaje enviado: " + exitdata);  // Verifica el mensaje enviado
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
            Console.WriteLine("Login data sent: " + loginData);

            // Read the response from the server
            byte[] responseData = new byte[256];
            int bytesReceived = server.Receive(responseData);
            string response = Encoding.ASCII.GetString(responseData, 0, bytesReceived);
            Console.WriteLine("Server response: " + response);

            // Check the server response for login success
            if (response.Contains("1/1"))
            {
                MessageBox.Show("Login successful!");
                show_Panel(user_panel);
            }
            else if (response.Contains("1/3"))
            {
                MessageBox.Show("User already connected");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
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
            // netejem el datagrid x cada consulta
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            //creem la taula nova x la consulta
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
                //divdim pel caracter / xq es com ho envia el server 
                string[] parts = line.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 7) // comprovem q server ens ha tornat 7 columnes
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
                Console.WriteLine("data sent: " + mensaje);
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                Console.WriteLine("Server response: " + mensaje);
                ProcessOpponentList(mensaje);
                
                // HE CREAT UN ALTRE DATAGRIDVIEW
            }

            else if (game_results_radioButton.Checked)
            {
                //quiere consultar los resultados de las partidas contra un oponente que se ingresa por el textbox

                string username = nombre_usuario_label.Text;
                string opponent = opponent_textBox.Text;
                string mensaje = $"4/{username}/{opponent}";
                Console.WriteLine("data sent: " + mensaje);
                // Enviar al servidor el mensaje

                // Recibir la respuesta del servidor
                byte[] msg2 = new byte[512];  // Cambié el tamaño para permitir más datos
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                Console.WriteLine("Server response: " + mensaje);
                // Procesar la respuesta para llenar el DataGridView
                ProcessGameResults(mensaje);
                //es pot veure q no fem servir la mateixa funcio q pels oponents xq la resposta ara te 7 columnes
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 4000);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Intentar conectar al servidor
            try
            {
                server.Connect(ipep);
                MessageBox.Show("Connection successful", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BackColor = Color.Green;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
