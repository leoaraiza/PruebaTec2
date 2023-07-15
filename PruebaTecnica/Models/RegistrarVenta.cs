namespace API.Models
{
    public class RegistrarVenta
    {
        public int? Idproductos { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadVendida { get; set; }

        public decimal SubTotal => PrecioUnitario * CantidadVendida;
        public DateTime Fecha { get; set; }

    }
}
