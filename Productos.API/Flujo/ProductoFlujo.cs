using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Reglas;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private IProductoDA _ProductoDA;
        private IProductoReglas _productoReglas;


        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _ProductoDA = productoDA;
            _productoReglas = productoReglas;
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _ProductoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _ProductoDA.Editar(Id, producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _ProductoDA.Eliminar(Id);
        }

        public Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return _ProductoDA.Obtener();
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            var producto = await _ProductoDA.Obtener(Id);

            if (producto == null)
                return null;

            var detalle = new ProductoDetalle
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
                SubCategoria = producto.SubCategoria,
                Categoria = producto.Categoria
            };

            detalle.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);

            return detalle;
        }
    }
}
