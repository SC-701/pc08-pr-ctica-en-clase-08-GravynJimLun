using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Reglas;
using System.Reflection;

namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private ICategoriaDA _CategoriaDA;

        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _CategoriaDA = categoriaDA;
        }

        public async Task<IEnumerable<Categoria>> Obtener()
        {
            return await _CategoriaDA.Obtener();
        }
    }
}
