using apirest.Conexion;
using apirest.Dto;
using apirest.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apirest.Model
{
    public class LibroSearch
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AplicationContext _context;

        public LibroSearch(IHttpContextAccessor httpContextAccessor, AplicationContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ActionResult<Serializer<LibroDto>>> Search(int page = 1, string nombre = "")
        {
            try
            {
                IQueryable<Libro> query = BuildQuery(nombre);
                int total = await query.CountAsync();
                Paginacion pagination = new Paginacion(page, total);
                List<Libro> model = await GetPagedData(query, pagination);
                var librosDto = MapToDto(model);

                return new Serializer<LibroDto>
                {
                    items = librosDto,
                    _meta = GetMeta(pagination),
                    _links = GetLinks(page, pagination, nombre),
                    messagge = "Ok",
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new Serializer<LibroDto>
                {
                    messagge = ex.Message,
                    status = 500
                };
            }
        }

        private IQueryable<Libro> BuildQuery(string nombre)
        {
            IQueryable<Libro> query = _context.libro.Include(libro => libro.Categoria);
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(filter => filter.Titulo.Contains(nombre));
            }
            return query;
        }

        private async Task<List<Libro>> GetPagedData(IQueryable<Libro> query, Paginacion pagination)
        {
            return await query.Skip(pagination.StartIndex).Take(pagination.ItemsPerPage).ToListAsync();
        }

        private IEnumerable<LibroDto> MapToDto(List<Libro> model)
        {
            return model.Select(libro => new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Editorial = libro.Editorial,
                Categoria = new CategoriaDto
                {
                    Id = libro.Categoria.Id,
                    Nombre = libro.Categoria.Nombre,
                }
            });
        }

        private Meta GetMeta(Paginacion pagination)
        {
            return new Meta
            {
                totalCount = pagination.TotalItems,
                pageCount = pagination.TotalPages,
                currentPage = pagination.CurrentPage,
                perPage = pagination.ItemsPerPage
            };
        }

        private Links GetLinks(int currentPage, Paginacion pagination, string nombre)
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
