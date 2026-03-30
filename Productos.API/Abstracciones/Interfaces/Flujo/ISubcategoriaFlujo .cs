using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISubcategoriaFlujo
    {
        Task<IEnumerable<Subcategoria>> Obtener(Guid IdCategoria);

    }
}
