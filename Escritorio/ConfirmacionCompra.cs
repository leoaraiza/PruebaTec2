using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escritorio
{
    public partial class ConfirmacionCompra : Form
    {
        public ConfirmacionCompra()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConfirmacionCompra_Load(object sender, EventArgs e)
        {

            var list = Carrito.resulCompra;

            foreach (var item in list)
            {
                int rowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                row.Cells[0].Value = item.CantidadVendida;
                row.Cells[1].Value = item.Titulo;
                row.Cells[2].Value = item.Descripcion;
                row.Cells[3].Value = item.PrecioUnitario;
                row.Cells[4].Value = item.PrecioUnitario * item.CantidadVendida;
                row.Cells[5].Value = item.Fecha.ToString("yyyy/mm/dd");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Carrito.resulCompra.Clear();
        }
    }
}
