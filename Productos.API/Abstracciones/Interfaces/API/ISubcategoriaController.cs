using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ISubcategoriaController
    {
        Task<IActionResult> Obtener(Guid IdCategoria);
    }
}
