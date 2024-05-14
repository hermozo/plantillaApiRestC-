using apirest.Helpers;
using apirest.Model;
using Microsoft.AspNetCore.Mvc;

namespace apirest.Interfaces
{
    public interface IProductoSearch
    {
        Task<ActionResult<Serializer<Producto>>> Search(int page = 1, string nombre = "");
        Meta GetMeta(Paginacion pagination);
        Links GetLinks(int currentPage, Paginacion pagination, string nombre);
    }
}
