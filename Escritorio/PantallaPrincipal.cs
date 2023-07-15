using API.Models;
using System.Net;
using Newtonsoft.Json;
using System.Text.Unicode;
using System.Text;
using Escritorio.ConsumoAPI;

namespace Escritorio
{
    public partial class PantallaPrincipal : Form
    {

        public static List<ProductoCompra> compra = new List<ProductoCompra>();
        private List<Producto> lista = new List<Producto>();
        private int? existencias;
        public PantallaPrincipal()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Servicios http = new Servicios();
            var productos = await http.GetListaProductos();
            lista = productos;
            foreach (var item in lista)
            {
                int rowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                row.Cells[0].Value = item.Titulo;
                row.Cells[1].Value = item.Descripcion;
                row.Cells[2].Value = item.PrecioUnitario;
                row.Cells[3].Value = item.Idproductos;
                this.existencias = item.Existencias;
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
            string titulo = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            string descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            decimal precio = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[2].Value);
            //int existencias = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);

            ProductoCompra producto = new ProductoCompra
            {
                Idproductos = id,
                Titulo = titulo,
                Descripcion = descripcion,
                PrecioUnitario = precio,
                Cantidad = 1

            };
            compra.Add(producto);

            ConfirmacionCarrito obj = new ConfirmacionCarrito(titulo, descripcion);
            obj.ShowDialog();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            ResumenVentas obj = new ResumenVentas();
            obj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Carrito obj = new Carrito();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
            string titulo = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            string descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            decimal precio = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[2].Value);
            var existencias = (from a in lista where a.Idproductos == id select a.Existencias).FirstOrDefault();

            Articulo obj = new Articulo(titulo, descripcion, precio, existencias);
            obj.ShowDialog(this);
        }
    }
}