using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form2 : Form
    {
        private List<(int Row, int Column)> userselectedCells = new List<(int, int)>();
        private List<(int Row, int Column)> opponentselectedCells = new List<(int, int)>();
        private string username;

        public Form2(string username)
        {
            InitializeComponent();
            form2_shoot_button.Visible = false;
            form2_send_fleet_position_button.Visible = true;
            opponent_dataGridView.CellClick -= opponent_dataGridView_CellClick;
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ConfigureTable(user_dataGridView);
            ConfigureTable(opponent_dataGridView);
        }
        private void form2_send_fleet_position_button_Click(object sender, EventArgs e)
        {
            user_dataGridView.CellClick -= user_dataGridView_CellClick;
            opponent_dataGridView.CellClick += opponent_dataGridView_CellClick;
            if (userselectedCells.Count == 5)
            {
                form2_send_fleet_position_button.Visible = false;

                form2_shoot_button.Visible = true;
                // Genera un mensaje con las coordenadas seleccionadas
                string message = $"10/{username}/";
                foreach (var (Row, Column) in userselectedCells)
                {
                    message += $"{Row:D2}/{Column:D2}/";
                }
                // Muestra las coordenadas en un MessageBox
                MessageBox.Show(message, "Celdas Seleccionadas");
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
                        cell.Style.BackColor = Color.Red;
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
    }
}
