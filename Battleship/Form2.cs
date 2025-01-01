using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form2 : Form
    {
        private List<(int Row, int Column)> userselectedCells = new List<(int, int)>();
        private List<(int Row, int Column)> opponentselectedCells = new List<(int, int)>();
        private string username;
        private string opponent;
        private int ID_game;
        Socket server;
        private int num_shoots;

        public Form2(int ID_game, Socket server, string username, string opponent)
        {
            InitializeComponent();
            form2_shoot_button.Visible = false;
            form2_send_fleet_position_button.Visible = true;
            opponent_dataGridView.CellClick -= opponent_dataGridView_CellClick;
            this.username = username;
            this.opponent = opponent;
            this.ID_game = ID_game;
            this.server = server;
            user_game_form_label.Text = username;
            opponent_game_form_label.Text= opponent;
            this.num_shoots = 0;

        }
        public void handle_game(string mensaje)
        {
            // Split the message to extract the necessary parts
            string[] parts = mensaje.Split('/');

            // Determine if the message is for the user or opponent
            int player = int.Parse(parts[1]); // 2 for user, 1 for opponent

            // Declare the target DataGridView
            DataGridView targetGrid = null;

            // Based on the player, choose the appropriate DataGridView
            if (player == 2)
            {
                targetGrid = user_dataGridView;
            }
            else if (player == 1)
            {
                targetGrid = opponent_dataGridView;
            }

            // Extract all row-column positions from the last part
            string positions = parts[3]; // Example: "7:6-8:3-9:2-"
            string[] pairs = positions.Split('-'); // Split by '-'

            foreach (string pair in pairs)
            {
                if (string.IsNullOrWhiteSpace(pair)) continue; // Skip empty entries

                string[] coordinates = pair.Split(':');
                if (coordinates.Length != 2) continue; // Invalid format, skip

                // Parse row and column
                int row = int.Parse(coordinates[0]); // Adjust to 0-based index
                int column = int.Parse(coordinates[1]); // Adjust to 0-based index

                // Change the row's background color to red
                targetGrid.Rows[row].Cells[column].Style.BackColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (opponentselectedCells.Count == 2)
            {
                string message = $"";
                for (int i = 0; i < opponentselectedCells.Count - 1; i++)
                {
                    var (Row, Column) = opponentselectedCells[i];
                    message += $"{Row:D2}:{Column:D2}*";
                }

                string serviceCode = "13";
                string type_of_message = "1"; // shoot
                int ID_game = get_ID_game();
                string data_to_send = $"{serviceCode}/{ID_game}/{type_of_message}/{username}/{num_shoots}/{message}";
                byte[] data = Encoding.ASCII.GetBytes(data_to_send);
                server.Send(data);

                num_shoots++;

                MessageBox.Show(data_to_send, "Celdas Seleccionadas");

            }
            else if (opponentselectedCells.Count > 2)
            {
                MessageBox.Show($"Faltan por seleccionar {2 - userselectedCells.Count} celdas.");

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ConfigureTable(user_dataGridView);
            ConfigureTable(opponent_dataGridView);
        }
        private void form2_send_fleet_position_button_Click(object sender, EventArgs e)
        {
            if (userselectedCells.Count == 5)
            {
                form2_send_fleet_position_button.Visible = false;

                form2_shoot_button.Visible = true;
                string message = $"";
                foreach (var (Row, Column) in userselectedCells)
                {
                    message += $"{Row:D2}:{Column:D2}*";
                }

                string serviceCode = "13";
                string type_of_message = "0"; // fleet position
                int ID_game = get_ID_game();
                string data_to_send = $"{serviceCode}/{ID_game}/{type_of_message}/{username}/{message}";
                byte[] data = Encoding.ASCII.GetBytes(data_to_send);
                server.Send(data);

                user_dataGridView.CellClick -= user_dataGridView_CellClick;
                opponent_dataGridView.CellClick += opponent_dataGridView_CellClick;
                // Muestra las coordenadas en un MessageBox
                MessageBox.Show(data_to_send, "Celdas Seleccionadas");

            }
            else
            {
                MessageBox.Show($"Faltan por seleccionar {5 - userselectedCells.Count} celdas.");

            }
        }

        private void opponent_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var cellPosition = (Row: e.RowIndex, Column: e.ColumnIndex);
            dgv.InvalidateCell(cell);
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (opponentselectedCells.Contains(cellPosition))
                {
                    cell.Style.BackColor = Color.White;
                    opponentselectedCells.Remove(cellPosition);
                }
                else
                {
                    if (opponentselectedCells.Count > 2 && !opponentselectedCells.Contains(cellPosition))
                    {
                        MessageBox.Show("Solo puedes seleccionar hasta 3 celda.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        cell.Style.BackColor = Color.Green;
                        opponentselectedCells.Add(cellPosition);
                    }
                }
                dgv.InvalidateCell(cell);
            }
        }

        private void user_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // no eliminar la funció
        }
        private void ConfigureTable(DataGridView dgv)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowHeadersVisible = true;  // Ocultar encabezados de fila si no se necesitan

            // Configura 10 columnas
            dgv.ColumnCount = 10;
            for (int i = 0; i < 10; i++)
            {
                dgv.Columns[i].Name = $"{i + 1}";
                dgv.Columns[i].Width = 30;  // Ancho de las columnas
            }

            // Configura 10 filas con letras de A a J
            string[] rowNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            dgv.RowCount = 10;
            for (int i = 0; i < 10; i++)
            {
                dgv.Rows[i].HeaderCell.Value = rowNames[i];  // Asigna la letra a cada fila
                dgv.RowHeadersWidth = 50;
                dgv.Rows[i].Height = 30;  // Altura de las filas
            }
        }

        private void user_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var cellPosition = (Row: e.RowIndex, Column: e.ColumnIndex);
            dgv.InvalidateCell(cell);
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (userselectedCells.Contains(cellPosition))
                {
                    cell.Style.BackColor = Color.White;
                    userselectedCells.Remove(cellPosition);
                }
                else
                {
                    if (userselectedCells.Count > 5-1 && !userselectedCells.Contains(cellPosition))
                    {
                        MessageBox.Show("No puedes seleccionar más celdas.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        cell.Style.BackColor = Color.Black;
                        userselectedCells.Add(cellPosition);
                    }
                }
                dgv.InvalidateCell(cell);
            }
        }

        private void set_username(string username)
        {
            this.username = username;
        }
        private string get_username()
        {
            return this.username;
        }
        private void set_opponent(string opponent)
        {
            this.opponent = opponent;
        }
        private string get_opponent()
        {
            return this.opponent;
        }

        private void set_ID_game(int ID_game)
        {
            this.ID_game = ID_game;
        }
        private int get_ID_game()
        {
            return this.ID_game;
        }
    }
}
