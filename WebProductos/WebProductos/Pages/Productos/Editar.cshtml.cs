using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace WebProductos.Pages.Productos
{
    [Authorize]
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse producto { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaseleccionada { get; set; }
        public Guid subcategoriaseleccionada { get; set; }


        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategoria();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                if (producto != null)
                {

                    var categoriaItem = categorias?
                        .FirstOrDefault(c => c.Text.Equals(producto.Categoria, StringComparison.OrdinalIgnoreCase));

                    if (categoriaItem == null)
                    {

                        ModelState.AddModelError("", $"No se encontró la categoría: {producto.Categoria}");
                        return Page();
                    }

                    categoriaseleccionada = Guid.Parse(categoriaItem.Value);

                    subcategorias = (await ObtenerSubcategorias(categoriaseleccionada))
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Nombre,
                            Selected = c.Nombre == producto.Subcategoria
                        }).ToList();

                    var subcategoriaItem = subcategorias?
                        .FirstOrDefault(s => s.Text.Equals(producto.Subcategoria, StringComparison.OrdinalIgnoreCase));

                    if (subcategoriaItem != null)
                        subcategoriaseleccionada = Guid.Parse(subcategoriaItem.Value);
                }
            }

            return Page();
        }
        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, producto.Id),new ProductoRequest {
            IdSubCategoria = subcategoriaseleccionada,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CodigoBarras = producto.CodigoBarras 
            } );

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        public async Task ObtenerCategoria()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoria");
            var cliente = ObtenerClienteConToken();
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
            var cliente = ObtenerClienteConToken(); 
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

        // ★ Helper — extrae el JWT de los claims y configura el HttpClient
        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
    }
}
