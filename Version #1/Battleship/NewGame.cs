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

namespace Battleship
{
    public partial class NewGame : Form
    {
        private Battleship battleshipForm; // Referencia a la instancia de Battleship
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
        private void NewGame_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

  
}
