using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Reglas;
using System.Reflection;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubcategoriaFlujo
    {
        private ISubcategoriaDA _SubcategoriaDA;

        public SubCategoriaFlujo(ISubcategoriaDA SubcategoriaDA)
        {
            _SubcategoriaDA = SubcategoriaDA;
        }

        async Task<IEnumerable<Subcategoria>> ISubcategoriaFlujo.Obtener(Guid IdCategoria)
        {
            return (IEnumerable<Subcategoria>)await _SubcategoriaDA.Obtener(IdCategoria);
        }
    }


}
