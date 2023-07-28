using API.Models;
using Escritorio.ConsumoAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Escritorio
{
    public partial class ResumenVentas : Form
    {
        string valor = "";
        int? CantidadVentas;
        public List<Producto> productos;
        public List<Venta> ventas;
        public ResumenVentas()
        {
            InitializeComponent();
        }

        private async void ResumenVentas_Load(object sender, EventArgs e)
        {
            Servicios http = new Servicios();
            productos = await http.GetListaProductos();
            ventas = await http.GetVentas();

            List<string> nombrePro = new List<string>();
            foreach (var item in productos)
            {
                nombrePro.Add(item.Titulo);
            }
            string totalProd = productos.Count.ToString();
            string totalVentas = ventas.Sum(x => x.CantidadVendida).ToString();
            int? InventarioTotal = productos.Sum(x => x.Existencias);
            var not = InventarioTotal < 100 ? true : false;
            string notificacionPro;
            if (not)
            {
                notificacionPro = "Realizar pedido a provedor";
            }
            else { notificacionPro = "No pedir a proveedor"; }
            int? maxVenta = ventas.Max(x => x.CantidadVendida);
            var idProdMasVendido = (from a in ventas where a.CantidadVendida == maxVenta select a.Idproductos).FirstOrDefault();
            var prodMasVendido = (from a in productos where a.Idproductos == idProdMasVendido select a.Titulo).FirstOrDefault();
            
            label6.Text = totalVentas;
            label7.Text = prodMasVendido.ToString();
            label8.Text = totalProd;
            label9.Text = notificacionPro;
            comboBox1.DataSource = nombrePro;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.valor = comboBox1.SelectedItem.ToString();
            var idProdSeleccionado =(from a in productos where a.Titulo == this.valor select a.Idproductos).FirstOrDefault();
            this.CantidadVentas = (from a in ventas where a.Idproductos == idProdSeleccionado select a.CantidadVendida).Sum();
            if (this.CantidadVentas == null)
            { this.CantidadVentas = 0; }
            label10.Text = this.CantidadVentas.ToString() + " piezas";
        }
    }
}
