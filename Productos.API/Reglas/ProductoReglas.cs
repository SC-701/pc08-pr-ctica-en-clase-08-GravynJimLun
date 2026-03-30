using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambio _tipoCambio;

        public ProductoReglas(ITipoCambio tipoCambio)
        {
            _tipoCambio = tipoCambio;
        }

        public async Task<decimal> CalcularPrecioUSD(decimal precioCRC)
        {
            var tipoCambio = await _tipoCambio.ObtenerTipoCambioVenta();
            return precioCRC * tipoCambio;
        }
    }
}
