using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalControlador : ControllerBase

    {
        private ejemploContext _ejemploContext;

        public PortalControlador (ejemploContext ejemplo){
            _ejemploContext = ejemplo;
        }

        [HttpGet]
        [Route ("ListaProductos")]
        public async Task<IActionResult> GetListaProductos()
        {
            var result = _ejemploContext.Productos.ToList();   
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("TotalVentas")]
        public async Task<IActionResult> GetTotalVentas()
        {
            var result = _ejemploContext.Ventas.ToList();
            return new OkObjectResult(result);
        }

        [HttpGet("{Productoid}")]  
        public async Task<IActionResult> GetProducto(int Productoid)
        {
            var result = await _ejemploContext.Get(Productoid);
            return new OkObjectResult(result);
        }
      
  
        [HttpPost]
        [Route("RegistraVenta")]
        public async Task<IActionResult> RegistraVenta(RegistrarVenta prod)
        {
            var result = await _ejemploContext.RegistraVenta(prod);

            return new OkObjectResult(result);
        }

        [HttpPut]
        [Route("ActualizaExistencia")]
        public async Task<bool> ActualizarVenta(RegistrarVenta venta)
        {
            var result = await _ejemploContext.ActualizaVenta( venta );         
            return result;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
