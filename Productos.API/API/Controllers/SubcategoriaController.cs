using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriaController : ControllerBase, ISubcategoriaController
    {
        private ISubcategoriaFlujo _subcategoriaFlujo;
        private ILogger<SubcategoriaController> _logger;

        public SubcategoriaController(ISubcategoriaFlujo subcategoriaFLujo, ILogger<SubcategoriaController> logger)
        {
            _subcategoriaFlujo = subcategoriaFLujo;
            _logger = logger;
        }
        #region Operaciones
        [HttpGet("{IdCategoria}")]
        public async Task<IActionResult> Obtener(Guid IdCategoria)
        {
            var resultado = await _subcategoriaFlujo.Obtener(IdCategoria);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        #endregion Operaciones

    }
}
