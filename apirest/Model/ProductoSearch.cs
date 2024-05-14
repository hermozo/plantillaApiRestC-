using apirest.Conexion;
using apirest.Helpers;
using apirest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apirest.Model
{
    public class ProductoSearch : IProductoSearch
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AplicationContext _context;

        public ProductoSearch(IHttpContextAccessor httpContextAccessor, AplicationContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ActionResult<Serializer<Producto>>> Search(int page = 1, string nombre = "")
        {
            IQueryable<Producto> query = _context.Producto;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(filter => filter.Name.Contains(nombre));
            }
            int total = await query.CountAsync();
            Paginacion pagination = new Paginacion(page, total);
            List<Producto> model = await query.Skip(pagination.StartIndex).Take(pagination.ItemsPerPage).ToListAsync();
            return new Serializer<Producto>
            {
                items = model,
                _meta = GetMeta(pagination),
                _links = GetLinks(page, pagination, nombre)
            };
        }

        public Meta GetMeta(Paginacion pagination)
        {
            return new Meta
            {
                totalCount = pagination.TotalItems,
                pageCount = pagination.TotalPages,
                currentPage = pagination.CurrentPage,
                perPage = pagination.ItemsPerPage
            };
        }

        public Links GetLinks(int currentPage, Paginacion pagination, string nombre)
        {
            string currentUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}";
            return new Links
            {
                self = new Href { href = $"{currentUrl}?nombre={nombre}&page={currentPage}" },
                first = new Href { href = $"{currentUrl}?nombre={nombre}&page=1" },
                last = new Href { href = $"{currentUrl}?nombre={nombre}&page={pagination.TotalPages}" },
                next = new Href { href = $"{currentUrl}?nombre={nombre}&page={(currentPage < pagination.TotalPages ? currentPage + 1 : currentPage)}" }
            };
        }
    }

}
