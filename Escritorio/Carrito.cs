using API.Models;
using Escritorio.ConsumoAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Escritorio
{
    public partial class Carrito : Form
    {
        public static List<ArticuloComprado> resulCompra = new List<ArticuloComprado>();
        public static List<ProductoCompra> list = PantallaPrincipal.compra;
        public Carrito()
        {
            InitializeComponent();
        }

        private void Carrito_Load(object sender, EventArgs e)
        {
            PantallaPrincipal pantallaPrincipal = new PantallaPrincipal();

            foreach (var item in list)
            {
                int rowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                row.Cells[0].Value = item.Titulo;
                row.Cells[1].Value = item.Descripcion;
                row.Cells[2].Value = item.PrecioUnitario;
                row.Cells[3].Value = item.Cantidad;
                row.Cells[4].Value = item.PrecioUnitario * item.Cantidad;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Servicios http = new Servicios();
                var index = 0;
                foreach (var item in list)
                {
                    RegistrarVenta obj = new RegistrarVenta
                    {
                        Idproductos = item.Idproductos,
                        Titulo = item.Titulo,
                        Descripcion = item.Descripcion,
                        PrecioUnitario = item.PrecioUnitario,
                        CantidadVendida = Convert.ToInt32(dataGridView1.Rows[index].Cells[3].Value),
                        Fecha = DateTime.Now,
                    };

                    var artComprado = await http.RegistrarCompra(obj);
                    Carrito.resulCompra.Add(artComprado);
                    index++;
                }
                this.Close();
                ConfirmacionCompra confirmacion = new ConfirmacionCompra();
                confirmacion.ShowDialog();
                PantallaPrincipal.compra.Clear();
            }
            else
            {
                MessageBox.Show("El carrito esta vacio", "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                var index = dataGridView1.CurrentRow.Index;
                list.RemoveAt(index);
                dataGridView1.Rows.Clear();
                foreach (var item in list)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    row.Cells[0].Value = item.Titulo;
                    row.Cells[1].Value = item.Descripcion;
                    row.Cells[2].Value = item.PrecioUnitario;
                    row.Cells[3].Value = item.Cantidad;
                    row.Cells[4].Value = item.PrecioUnitario * item.Cantidad;
                }
            }
            else
            {
                MessageBox.Show("No hay elementos por eliminar", "Error", MessageBoxButtons.OK);

            }
        }
    }
}
