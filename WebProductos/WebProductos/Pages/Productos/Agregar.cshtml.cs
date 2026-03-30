using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace WebProductos.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid? categoriaseleccionada { get; set; }


        public async Task<ActionResult> OnGet()
        {
            await ObtenerCategoria();
            return Page();
        }
        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            System.Diagnostics.Debug.WriteLine($"IdSubCategoria: {producto.IdSubCategoria}");
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");
            var cliente = new HttpClient();

            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Console.WriteLine(contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception($"Error API: {contenido}");
            }
            return RedirectToPage("./Index");
        }

        public async Task ObtenerCategoria()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoria");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
                categorias = resultadoDeserializado.Select(c =>
                                  new SelectListItem
                                  {
                                      Value = c.Id.ToString(),
                                      Text = c.Nombre.ToString()
                                  }).ToList();
            }
        }

        public async Task<List<Subcategoria>> ObtenerSubcategorias(Guid categoriaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubcategoria");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Subcategoria>>(resultado, opciones);
            }
            return new List<Subcategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubcategorias(Guid categoriaId)
        {
            var Subcategoria = await ObtenerSubcategorias(categoriaId);
            return new JsonResult(Subcategoria);
        }
    }
}
