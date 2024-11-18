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
using System.Security.Cryptography.X509Certificates;
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
        //Size originalSize;
        public string listaconectados;


        public Battleship()
        {
            InitializeComponent();
            show_Panel(Login_panel);
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc,8020);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the server
            server.Connect(ipep);
        }
        
      
        public void show_Panel(Panel panel)
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
            string serviceCode = "2";  // 1 for login
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
            //originalSize = dataGridView.Size;

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
            NewGame newGameForm = new NewGame(this);

            // Mostrar el formulario
            //newGameForm.Show();
            newGameForm.ShowDialog();

     

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
            
        }


        //función que recibe el mensaje del servidor, lo procesa y lo muestra en el datagridview
        private void ProcessGameResults(string resultado)
        {
            // Vaciar el DataGridView antes de agregar nuevos resultados
            dataGridView.DataSource = null; // Limpia el DataGridView actual

            // Separar los resultados usando el salto de línea como delimitador
            string[] filas = resultado.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Crear una tabla de datos (DataTable) para almacenar los resultados
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Username");    // Columna para el nombre de usuario
            dataTable.Columns.Add("Total Points"); // Columna para los puntos totales

            // Iterar a través de cada fila de resultados
            foreach (var fila in filas)
            {
                // Separar el nombre de usuario y los puntos totales
                string[] datos = fila.Split('/');
                if (datos.Length == 2) // Verificamos que tenemos dos partes
                {
                    // Agregar una nueva fila a la DataTable
                    dataTable.Rows.Add(datos[0], int.Parse(datos[1])); // Nombre de usuario y puntos totales
                }
            }

            // Asignar la DataTable al DataGridView
            dataGridView.DataSource = dataTable;

            // Configurar el modo de autoajuste de columnas y filas
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Ajustar el ancho de las columnas
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;       // Ajustar el alto de las filas

            // (Opcional) Redimensionar el DataGridView para que se ajuste al contenido
            dataGridView.AutoResizeColumns();  // Redimensionar las columnas automáticamente
            dataGridView.AutoResizeRows();     // Redimensionar las filas automáticamente

            // Calcular el tamaño total del DataGridView basado en el contenido

            // 1. Ancho total: sumar el ancho de todas las columnas
            int totalWidth = dataGridView.RowHeadersWidth; // Incluir el ancho de los encabezados de las filas
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                totalWidth += column.Width;
            }

            // 2. Alto total: sumar el alto de todas las filas + encabezado de las columnas
            int totalHeight = dataGridView.ColumnHeadersHeight; // Incluir el alto del encabezado de las columnas
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                totalHeight += row.Height;
            }

            // Opcional: añadir espacio extra para las barras de desplazamiento si están presentes
            if (dataGridView.Rows.Count > dataGridView.DisplayedRowCount(false))
            {
                totalWidth += SystemInformation.VerticalScrollBarWidth; // Ancho adicional para la barra de desplazamiento vertical
            }
            if (dataGridView.Columns.Count > dataGridView.DisplayedColumnCount(false))
            {
                totalHeight += SystemInformation.HorizontalScrollBarHeight; // Alto adicional para la barra de desplazamiento horizontal

            }
            dataGridView.RowHeadersVisible = false;

            dataGridView.Width = totalWidth;
            dataGridView.Height = totalHeight;
        }

        void ProcessShowGamesResult(string resultado)
        {
            // Vaciar el DataGridView antes de agregar nuevos resultados
            dataGridView.DataSource = null;

            // Separar los resultados usando '/' como delimitador
            string[] filas = resultado.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Crear un DataTable para almacenar los resultados
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID Game");
            dataTable.Columns.Add("Player 1 ID");
            dataTable.Columns.Add("Player 2 ID");
            dataTable.Columns.Add("Points Player 1");
            dataTable.Columns.Add("Points Player 2");
            dataTable.Columns.Add("Start Time");
            dataTable.Columns.Add("End Time");

            // Iterar a través de cada fila de resultados
            foreach (var fila in filas)
            {
                // Separar la información de cada partida
                string[] datos = fila.Split('/');
                if (datos.Length == 7) // Asegurarnos que hay siete partes
                {
                    // Agregar una nueva fila al DataTable
                    dataTable.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5], datos[6]);
                }
            }

            // Asignar el DataTable al DataGridView
            dataGridView.DataSource = dataTable;

            // Configurar el modo de autoajuste de columnas y filas
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Ajustar el ancho de las columnas
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;       // Ajustar el alto de las filas

            // (Opcional) Redimensionar el DataGridView para que se ajuste al contenido
            dataGridView.AutoResizeColumns();  // Redimensionar las columnas automáticamente
            dataGridView.AutoResizeRows();     // Redimensionar las filas automáticamente

            // Calcular el tamaño total del DataGridView basado en el contenido

            // 1. Ancho total: sumar el ancho de todas las columnas
            int totalWidth = dataGridView.RowHeadersWidth; // Incluir el ancho de los encabezados de las filas
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                totalWidth += column.Width;
            }

            // 2. Alto total: sumar el alto de todas las filas + encabezado de las columnas
            int totalHeight = dataGridView.ColumnHeadersHeight; // Incluir el alto del encabezado de las columnas
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                totalHeight += row.Height;
            }

            // Opcional: añadir espacio extra para las barras de desplazamiento si están presentes
            if (dataGridView.Rows.Count > dataGridView.DisplayedRowCount(false))
            {
                totalWidth += SystemInformation.VerticalScrollBarWidth; // Ancho adicional para la barra de desplazamiento vertical
            }
            if (dataGridView.Columns.Count > dataGridView.DisplayedColumnCount(false))
            {
                totalHeight += SystemInformation.HorizontalScrollBarHeight; // Alto adicional para la barra de desplazamiento horizontal

            }
            dataGridView.RowHeadersVisible = false;

            dataGridView.Width = totalWidth;
            dataGridView.Height = totalHeight;
        }

        void ProcessListResults(string resultado)
        {
            //dataGridView.Size = originalSize;
            // Vaciar el DataGridView antes de agregar nuevos resultados
            dataGridView.DataSource = null;

            // Crear un DataTable para almacenar los resultados
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Opponent Username");

            // Separar los nombres de oponentes usando '\n' como delimitador
            string[] oponentes = resultado.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Iterar a través de cada oponente y añadirlos al DataTable
            foreach (var oponente in oponentes)
            {
                dataTable.Rows.Add(oponente); // Agregar cada oponente como una fila
            }

            // Asignar el DataTable al DataGridView
            dataGridView.DataSource = dataTable;

            // Configurar el modo de autoajuste de columnas y filas
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Ajustar el ancho de las columnas
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;       // Ajustar el alto de las filas

            // (Opcional) Redimensionar el DataGridView para que se ajuste al contenido
            dataGridView.AutoResizeColumns();  // Redimensionar las columnas automáticamente
            dataGridView.AutoResizeRows();     // Redimensionar las filas automáticamente

            // Calcular el tamaño total del DataGridView basado en el contenido

            // 1. Ancho total: sumar el ancho de todas las columnas
            int totalWidth = dataGridView.RowHeadersWidth +1 ; // Incluir el ancho de los encabezados de las filas
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                totalWidth += column.Width;
            }

            // 2. Alto total: sumar el alto de todas las filas + encabezado de las columnas
            int totalHeight = dataGridView.ColumnHeadersHeight; // Incluir el alto del encabezado de las columnas
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                totalHeight += row.Height;
            }

            // Opcional: añadir espacio extra para las barras de desplazamiento si están presentes
            if (dataGridView.Rows.Count > dataGridView.DisplayedRowCount(false))
            {
                totalWidth += SystemInformation.VerticalScrollBarWidth; // Ancho adicional para la barra de desplazamiento vertical
            }
            if (dataGridView.Columns.Count > dataGridView.DisplayedColumnCount(false))
            {
                totalHeight += SystemInformation.HorizontalScrollBarHeight; // Alto adicional para la barra de desplazamiento horizontal
            }

            // Ajustar el tamaño del DataGridView para que coincida con el contenido
            dataGridView.RowHeadersVisible = false;

            dataGridView.Width = totalWidth;
            dataGridView.Height = totalHeight;
        }
        
        
        public void query_Button_Click(object sender, EventArgs e)
        {
            if (players_list_radioButton.Checked)
            {
                //quiere consultar la lista de jugadores con los que ha jugado una partida al menos una vez
                string username = nombre_usuario_label.Text;
                string mensaje = $"3/{username}";

                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[4096];  // Aumentar el tamaño para permitir más datos
                int bytesRead = server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2, 0, bytesRead).Split('\0')[0];

                ProcessListResults(mensaje);
            }

            else if (game_results_radioButton.Checked)
            {
                //quiere consultar los resultados de las partidas contra un oponente que se ingresa por el textbox

                string username = nombre_usuario_label.Text;
                string opponent = opponent_textBox.Text;
                string mensaje = $"4/{username}/{opponent}";

                // Enviar el mensaje al servidor
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Recibir la respuesta del servidor
                byte[] msg2 = new byte[4096];  // Aumentamos el tamaño para permitir más datos
                int bytesRead = server.Receive(msg2);
                string response = Encoding.ASCII.GetString(msg2, 0, bytesRead).Split('\0')[0];

                // Procesar la respuesta para llenar el DataGridView
                ProcessShowGamesResult(response);
            }

            else if (games_sort_date_radioButton.Checked)
            {
                // User input
                string username = nombre_usuario_label.Text;

                // Obtener fechas desde los DateTimePicker
                DateTime initial_date = initial_dateTimePicker.Value;
                DateTime ending_date = ending_dateTimePicker.Value;

                // Formatear las fechas
                string formattedInitialDate = initial_date.ToString("yyyy-MM-dd HH:mm:ss");
                string formattedEndingDate = ending_date.ToString("yyyy-MM-dd HH:mm:ss");

                // Preparar el mensaje
                string mensaje = $"5/{username}/{formattedInitialDate}/{formattedEndingDate}";

                // Enviar el mensaje al servidor
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Recibir la respuesta del servidor
                byte[] msg2 = new byte[2048];  // Ajustar el tamaño según lo esperado
                int bytesRead = server.Receive(msg2);

                // Procesar la respuesta
                string response = Encoding.ASCII.GetString(msg2, 0, bytesRead);
                ProcessShowGamesResult(response);

                // Cerrar la conexión
                //server.Close();
            }
            else if (ranking_radioButton.Checked)
            {
                string mensaje = "6"; // Operation code for showing rankings

                // Send the message to the server
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Prepare to receive the response from the server
                byte[] msg2 = new byte[512];  // Increased size for more data
                StringBuilder sb = new StringBuilder(); // Use a StringBuilder to concatenate the incoming messages
                int bytesReceived;

                // Keep receiving data until there's no more data
                do
                {
                    bytesReceived = server.Receive(msg2);
                    sb.Append(Encoding.ASCII.GetString(msg2, 0, bytesReceived));
                } while (bytesReceived > 0);

                // Convert the received bytes to a string
                mensaje = sb.ToString().TrimEnd('\0');

                // Clear the DataGridView before processing the new results
                ProcessGameResults(mensaje);
            }
            else if (connected_users_radioButton.Checked)
            {
                //quiere consultar la lista de jugadores con conectados
                string username = nombre_usuario_label.Text;
                string mensaje = $"7/{username}";

                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[4096];  // Aumentar el tamaño para permitir más datos
                int bytesRead = server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2, 0, bytesRead).Split('\0')[0];
                listaconectados = mensaje;
                ProcessListResults(mensaje);
            }
           
        }

        private void username_Login_panel_textBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void User_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void EnviarSolicitud_Click(object sender, EventArgs e)
        {

        }

        private void Conectar_Click(object sender, EventArgs e)
        {

        }

        private void Desconectar_Click(object sender, EventArgs e)
        {

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
            string serviceCode = "1";  // 2 for login, 1 for signup

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
