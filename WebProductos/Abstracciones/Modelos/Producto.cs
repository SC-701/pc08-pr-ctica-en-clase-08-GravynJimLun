using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida.")]
        [StringLength(20, ErrorMessage = "El nombre debe de tener entre 3 y 20 caracteres.",MinimumLength =3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad Descripcion es requerida.")]
        [StringLength(100, ErrorMessage = "La descripcion no debe tener mas de 100 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La propiedad Precio es requerida.")]
        [Range(0.01,100000, ErrorMessage ="El numero debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La propiedad Stock es requerida.")]
        [Range(0, 100000, ErrorMessage = "El numero debe ser mayor a 0 y no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La propiedad CodigoBarras es requerida.")]
        [StringLength(12,ErrorMessage ="El codigo de barras debe ser de 12 caracteres.")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid? IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string? Subcategoria { get; set; }
        public  string? Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse {
    
        public decimal PrecioUSD { get; set; }

    }

}
