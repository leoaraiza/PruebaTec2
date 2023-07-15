using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.ConsumoAPI

{
    public class Servicios
    {
        private string _url = "https://localhost:7240/api/PortalControlador/";

        public async Task<List<Producto>> GetListaProductos()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_url);
            var respuesta = await cliente.GetAsync("ListaProductos");
            var json_respuesta = await respuesta.Content.ReadAsStringAsync();
            var productos = JsonConvert.DeserializeObject<List<Producto>>(json_respuesta);
            return productos;
        }
        public async Task<Producto> GetProducto(int id)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_url);
            var respuesta = await cliente.GetAsync($"{id}");
            var json_respuesta = await respuesta.Content.ReadAsStringAsync();
            var producto = JsonConvert.DeserializeObject<Producto>(json_respuesta);
            return producto;
        }

        public async Task<List<Venta>> GetVentas()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_url);
            var respuesta = await cliente.GetAsync("TotalVentas");
            var json_respuesta = await respuesta.Content.ReadAsStringAsync();
            var listaVentas = JsonConvert.DeserializeObject<List<Venta>>(json_respuesta);
            return listaVentas;
        }


        public async Task<ArticuloComprado> RegistrarCompra(RegistrarVenta obj)
        {

            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_url);
            StringContent contenido = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            string json_respuesta = "";
            var regExistencia = await cliente.PutAsync("ActualizaExistencia", contenido);
            var ap = await regExistencia.Content.ReadAsStringAsync();
            var aprobacion = JsonConvert.DeserializeObject<bool>(ap);
            if (aprobacion == true)
            {
                var respuesta = await cliente.PostAsync("RegistraVenta", contenido);
                json_respuesta = await respuesta.Content.ReadAsStringAsync();
            }

            var articuloComprado = JsonConvert.DeserializeObject<ArticuloComprado>(json_respuesta);

            return articuloComprado;
        }
    }
}
