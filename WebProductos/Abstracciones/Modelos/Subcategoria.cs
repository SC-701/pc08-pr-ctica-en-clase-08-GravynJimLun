using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Subcategoria
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }

    }

}