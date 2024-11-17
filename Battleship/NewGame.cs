using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Win32;

namespace Battleship
{
    public partial class NewGame : Form
    {
        private Battleship battleshipForm; // Referencia a la instancia de Battleship
        private Socket server;
        private Thread atender; 
        public NewGame(Battleship form)
        {
            InitializeComponent();
            battleshipForm = form; // Guardar la referencia
            show_Panel(seleccionarOponente_panel);  
            ProcessListConectados(battleshipForm.listaconectados); // Procesar la lista de conectados
            DataGrid_connectados.ReadOnly = true;
            CreateGameBoard(dataGridView_MiMar); // Llama al método para crear el tablero1
            CreateGameBoard(dataGridView_marOponente);// Llama al método para crear el tablero2
        }

        public void show_Panel(Panel panel)
        {
            seleccionarOponente_panel.Visible = false;
            game_panel.Visible = false;
         
            panel.Visible = true;
        }
        void ProcessListConectados(string resultado)
        {
            //dataGridView.Size = originalSize;
            // Vaciar el DataGridView antes de agregar nuevos resultados
            DataGrid_connectados.DataSource = null;

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
            DataGrid_connectados.DataSource = dataTable;

            // Configurar el modo de autoajuste de columnas y filas
            DataGrid_connectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Ajustar el ancho de las columnas
            DataGrid_connectados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;       // Ajustar el alto de las filas

            // (Opcional) Redimensionar el DataGridView para que se ajuste al contenido
            DataGrid_connectados.AutoResizeColumns();  // Redimensionar las columnas automáticamente
            DataGrid_connectados.AutoResizeRows();     // Redimensionar las filas automáticamente

            // Calcular el tamaño total del DataGridView basado en el contenido

            // 1. Ancho total: sumar el ancho de todas las columnas
            int totalWidth = DataGrid_connectados.RowHeadersWidth + 1; // Incluir el ancho de los encabezados de las filas
            foreach (DataGridViewColumn column in DataGrid_connectados.Columns)
            {
                totalWidth += column.Width;
            }

            // 2. Alto total: sumar el alto de todas las filas + encabezado de las columnas
            int totalHeight = DataGrid_connectados.ColumnHeadersHeight; // Incluir el alto del encabezado de las columnas
            foreach (DataGridViewRow row in DataGrid_connectados.Rows)
            {
                totalHeight += row.Height;
            }

            // Opcional: añadir espacio extra para las barras de desplazamiento si están presentes
            if (DataGrid_connectados.Rows.Count > DataGrid_connectados.DisplayedRowCount(false))
            {
                totalWidth += SystemInformation.VerticalScrollBarWidth; // Ancho adicional para la barra de desplazamiento vertical
            }
            if (DataGrid_connectados.Columns.Count > DataGrid_connectados.DisplayedColumnCount(false))
            {
                totalHeight += SystemInformation.HorizontalScrollBarHeight; // Alto adicional para la barra de desplazamiento horizontal
            }

            // Ajustar el tamaño del DataGridView para que coincida con el contenido
            DataGrid_connectados.RowHeadersVisible = false;

            DataGrid_connectados.Width = totalWidth;
            DataGrid_connectados.Height = totalHeight;
        }

        private void DataGrid_connectados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            battleshipForm.show_Panel(game_panel);
        }
        private void DataGrid_connectados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse de que se hizo clic en una fila válida
            if (e.RowIndex >= 0)
            {
                string selectedOpponent = DataGrid_connectados.Rows[e.RowIndex].Cells[0].Value.ToString();
                MessageBox.Show($"Has seleccionado a: {selectedOpponent}"); 

                // Cambiar al panel del juego
                show_Panel(game_panel);
                OPONENTS__lbl.Text = selectedOpponent + "'S SHIPS";
            }
        }

        private void CreateGameBoard(DataGridView midatagrid)
        {

            // Configurar propiedades del DataGridView
            midatagrid.ColumnCount = 10; // 10 columnas
            midatagrid.RowCount = 10; // 10 filas
            

            // Desactivar la edición de celdas (opcional)
            midatagrid.ReadOnly = true;

            // Establecer la apariencia del tablero
            midatagrid.AllowUserToAddRows = false; // Desactivar la opción de agregar filas
 

            string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            // Asumimos que midatagrid tiene 11 columnas ya creadas
            for (int i = 0; i < 9; i++) // Recorre las letras A-J (10 letras)
            {
                midatagrid.Columns[i].HeaderText = letras[i]; // Asignar las letras empezando desde la columna 0
            }

            // Agregar encabezados de fila 0-9
            for (int i = 0; i < 9; i++)
            {
                midatagrid.Rows[i].HeaderCell.Value = i.ToString(); // Agregar números
            }

            // Define el tamaño cuadrado de las celdas (50x50 en este caso)
            int cellSize = 20;

            // Configurar las columnas para que tengan un ancho fijo
            foreach (DataGridViewColumn column in midatagrid.Columns)
            {
                column.Width = cellSize;
            }

            // Configurar las filas para que tengan una altura fija
            foreach (DataGridViewRow row in midatagrid.Rows)
            {
                row.Height = cellSize;
            }

            // Ajustar el tamaño de los encabezados de las columnas (fijos)
            midatagrid.ColumnHeadersHeight = cellSize; // Alto de los encabezados de columna

            // Activar el ajuste automático de los encabezados de fila
            midatagrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            // Mantener el ajuste fijo para las celdas y columnas
            midatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            midatagrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Opcional: Desactivar las barras de desplazamiento si no se desean
            midatagrid.ScrollBars = ScrollBars.None;

            // Ajustar el tamaño total del DataGridView para que coincida con el número de filas y columnas
            midatagrid.Width = midatagrid.Columns.Count * cellSize + midatagrid.RowHeadersWidth;
            midatagrid.Height = midatagrid.Rows.Count * cellSize + midatagrid.ColumnHeadersHeight;
        }
        private void AtenderServidor()
        {
            while (true)
            {
                try
                {
                    // Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[1024]; // Aumentar el tamaño del buffer para recibir mensajes más largos
                    int bytesRec = server.Receive(msg2);
                    string response = Encoding.ASCII.GetString(msg2, 0, bytesRec);
                    string[] trozos = response.Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje = trozos[1].Split('\0')[0];
                    if (DataGrid_connectados.SelectedRows.Count > 0)
                    {
                        // Obtener la fila seleccionada (en el caso de que haya más de una, solo tomamos la primera)
                        DataGridViewRow row = DataGrid_connectados.SelectedRows[0];

                        // Obtener el valor de la celda en la columna "Username"
                        string nombreUsuario = row.Cells["Username"].Value.ToString();

                        switch (codigo)
                        {
                            case 1: // Registro
                                MessageBox.Show("Registro: " + mensaje);
                                break;

                            case 2: // Login
                                MessageBox.Show("Login: " + mensaje);
                                break;

                            case 3: // Listar juegos
                                MessageBox.Show("Lista de juegos: " + mensaje);
                                break;

                            case 4: // Consulta de oponente
                                MessageBox.Show("Consulta de oponente: " + mensaje);
                                break;

                            case 5: // Mostrar juegos
                                MessageBox.Show("Mostrar juegos: " + mensaje);
                                break;

                            case 6: // Mostrar rankings
                                MessageBox.Show("Mostrar rankings: " + mensaje);
                                break;

                            case 7: // Mostrar usuarios conectados
                                MessageBox.Show("Usuarios conectados: " + mensaje);
                                break;

                            default:
                                MessageBox.Show("Código de respuesta no reconocido: " + codigo);
                                break;
                        }
                    }
            catch (Exception ex)
                {
                    MessageBox.Show("Error al recibir datos: " + ex.Message);
                    break; // Salir del bucle en caso de error
                }
            }
            private void NewGame_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_MiMar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
    

        private void EnviarSolicitud_Click(object sender, EventArgs e)
        {
            {


            nombreUsuario = txtUsername.Text;

                // Verificamos qué opción ha seleccionado el usuario
                if (registro.Checked)
                {
                    // Quiere registrar
                    string mensaje = "1/" + nombreUsuario;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (login.Checked)
                {
                    // Quiere hacer login
                    string mensaje = "2/" + nombreUsuario; // Asumiendo que el nombre es el dato para login
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (listarJuegos.Checked)
                {
                    // Quiere listar juegos
                    string mensaje = "3/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (consultaOponente.Checked)
                {
                    // Quiere consultar oponente
                    string mensaje = "4/" + nombreUsuario; // Asumiendo que el nombre es el dato para consultar
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (mostrarJuegos.Checked)
                {
                    // Quiere mostrar juegos
                    string mensaje = "5/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (mostrarRankings.Checked)
                {
                    // Quiere mostrar rankings
                    string mensaje = "6/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (usuariosConectados.Checked)
                {
                    // Quiere mostrar usuarios conectados
                    string mensaje = "7/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona una opción.");
                }
            }
        }

        private void Conectar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = row.Cells["Username"].Value.ToString();
            // Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9080);

            // Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); // Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

                // Enviar mensaje al servidor para notificar que se ha conectado
                string mensaje = "CONNECT/" + nombreUsuario;
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            // Pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = row.Cells["Username"].Value.ToString();
            // Obtenemos el nombre de usuario desde el TextBox
            nombreUsuario = txtUsername.Text;

            // Mensaje de desconexión
            string mensaje = "DISCONNECT/" + nombreUsuario;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }
    }
