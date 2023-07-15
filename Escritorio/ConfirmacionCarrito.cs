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
    public partial class ConfirmacionCarrito : Form
    {
        string nombreProducto;
        string descripcion;
        public ConfirmacionCarrito(string nombreProducto, string descripcion)
        {
            this.nombreProducto = nombreProducto;
            this.descripcion = descripcion;
            InitializeComponent();
        }


        private void ConfirmacionCarrito_Load(object sender, EventArgs e)
        {
            label2.Text = this.nombreProducto + " " + this.descripcion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
