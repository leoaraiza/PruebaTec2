namespace API.Models
{
    public class ArticuloComprado
    {
        public int? Idventas { get; set; }
        public int? Idproductos { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadVendida { get; set; }
        public decimal Subtotal => PrecioUnitario * CantidadVendida;
        public DateTime Fecha { get; set; }
    }
}
