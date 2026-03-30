using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DA
{
    public class SubcategoriaDA : ISubcategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;


        public SubcategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones


        public async Task<IEnumerable<Subcategoria>> Obtener(Guid IdCategoria)
        {
            string query = @"ObtenerSubcategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Subcategoria>(query,
                new { IdCategoria = IdCategoria });
            return resultadoConsulta;
        }


        #endregion
    }
}
