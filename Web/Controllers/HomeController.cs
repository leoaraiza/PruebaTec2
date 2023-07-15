using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using Web.ConsumoAPI;


namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Servicios http = new Servicios();
        private static List<ProductoCompra> listaCarrito= new List<ProductoCompra>();
        private static List<ArticuloComprado> listaCompra = new List<ArticuloComprado>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var listaProductos = await http.GetListaProductos();        
            return View(listaProductos);
        }

        public async Task<IActionResult> Producto(int id)
        {
            var producto = await http.GetProducto(id);
            return View(producto);
        }

        public async Task<string> VTxProducto(string id)
        {
            var listaVentas = await http.GetVentas();
            var cantidadVentas = (from a in listaVentas where a.Idproductos == Int32.Parse(id) select a.CantidadVendida).Sum();
            return cantidadVentas.ToString();
        }

        public async Task<IActionResult> Compra(List<ProductoCompra> list)
        {
            listaCarrito.Clear();
            foreach(var item in list)
            {
                RegistrarVenta compra = new RegistrarVenta
                {
                    Idproductos = item.Idproductos,
                    Titulo = item.Titulo,
                    Descripcion = item.Descripcion,
                    PrecioUnitario = item.PrecioUnitario,
                    CantidadVendida = item.Cantidad,
                    Fecha = DateTime.Now,
                };
                var ventaRegistrada = await http.RegistrarCompra(compra);
                
                listaCompra.Add(ventaRegistrada);
            }
    
            return View(listaCompra);
        }

        public IActionResult RegresaIndexReset()
        {
            listaCompra.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult RegresaIndex()
        {
            return RedirectToAction("Index");
        }

        public IActionResult EliminarProducto(int index)
        {

            listaCarrito.RemoveAt(index);
            return RedirectToAction ("VerCarrito");
        }
        public async Task<IActionResult> ConfirmaProducto(int id)
        { 
            var producto = await http.GetProducto(id);
            return View(producto);
        }

        public async Task<IActionResult> AgregarACarrito(int id )
        {
            var productoResponse = await http.GetProducto(id);
            
            ProductoCompra producto = new ProductoCompra
            {
                Idproductos = productoResponse.Idproductos,
                Titulo = productoResponse.Titulo,
                Descripcion = productoResponse.Descripcion,
                PrecioUnitario = productoResponse.PrecioUnitario,
                Cantidad = 1
            };

            listaCarrito.Add(producto);

            return RedirectToAction("ConfirmaProducto", new {id=id});
        }

        public IActionResult VerCarrito(int id)
        {
            return View(listaCarrito);
        }

        public IActionResult modifCantCarrito(int productoId, int cantidad)
        {
           
                var item = listaCarrito.FirstOrDefault(item => item.Idproductos == productoId);
                if (item != null)
                {
                    item.Cantidad = cantidad;
                }

            return NoContent();
        }

        public async Task<IActionResult> Ventas()
        {
           
            List<SelectListItem> comboBox = new List<SelectListItem>();
            var productos = await http.GetListaProductos();
            string notificacion = "";
            List<string> nombrePro = new List<string>();

            foreach (var item in productos)
            {
                nombrePro.Add(item.Titulo);
            }
            comboBox = (from a in productos
                        select new SelectListItem
                        {
                            Value = a.Idproductos.ToString(),
                            Text = a.Titulo
                        }).ToList();


            var listaVentas = await http.GetVentas();

            ViewBag.TotalProd = productos.Count.ToString();
            ViewBag.TotalVentas = listaVentas.Sum(x => x.CantidadVendida).ToString();
            int? inventarioTotal = productos.Sum(x => x.Existencias);
            var not = inventarioTotal < 100 ? true : false;
            if (not == false)
            {
                notificacion = "No se requiere pedir a proveedor";
            }
            else
            {
                notificacion = "Solicitar pedido a proveedor";
            }
            ViewBag.Notificacion = notificacion;
            int? maxVenta = listaVentas.Max(x => x.CantidadVendida);
            var idProdMasVendido = (from a in listaVentas where a.CantidadVendida == maxVenta select a.Idproductos).FirstOrDefault();
            ViewBag.prodMasVendido = (from a in productos where a.Idproductos == idProdMasVendido select a.Titulo).FirstOrDefault();
            return View(comboBox);

        }
        
       
       
    }
}