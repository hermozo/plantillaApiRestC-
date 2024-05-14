using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apirest.Conexion;
using apirest.Model;
using apirest.Helpers;

namespace apirest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AplicationContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductoController(AplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<Serializer<Producto>>> actionIndex(int page = 1, string nombre = "")
        {
            ProductoSearch productoSearch = new ProductoSearch(_httpContextAccessor, _context);
            return await productoSearch.Search(page, nombre);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> actionView(int id)
        {
            if (_context.Producto == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> actionUpdate(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!findModel(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> actionCreate(Producto producto)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'AplicationContext.Producto'  is null.");
            }
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> actionDelete(int id)
        {
            if (_context.Producto == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool findModel(int id)
        {
            return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
