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
    public partial class Articulo : Form
    {
        public string titulo;
        public string descripcion;
        public decimal precio;
        public int? existencias;
        public Articulo()
        {
            InitializeComponent();
        }
        public Articulo(string titulo, string descripcion, decimal precio, int? existencias)
        {
            InitializeComponent();
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.precio = precio;
            this.existencias = existencias;
        }

        private void Articulo_Load(object sender, EventArgs e)
        {
            label1.Text = this.titulo;
            label3.Text = this.descripcion;
            label5.Text = this.precio.ToString();
            label7.Text = this.existencias.ToString();

        }
    }
}
