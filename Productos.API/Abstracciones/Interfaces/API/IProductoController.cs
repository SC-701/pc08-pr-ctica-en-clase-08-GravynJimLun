using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IProductoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(ProductoRequest vehiculo);
        Task<IActionResult> Editar(Guid Id, ProductoRequest vehiculo);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
