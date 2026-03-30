using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ISubcategoriaDA
    {
        Task<IEnumerable<Subcategoria>> Obtener(Guid IdCategoria);
    }
}
