
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apirest.Conexion;
using apirest.Model;
using Microsoft.AspNetCore.Authorization;

namespace apirest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AplicationContext _context;

        public CategoriasController(AplicationContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Getcategoria()
        {
          if (_context.categoria == null)
          {
              return NotFound();
          }
            return await _context.categoria.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
          if (_context.categoria == null)
          {
              return NotFound();
          }
            var categoria = await _context.categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
          if (_context.categoria == null)
          {
              return Problem("Entity set 'AplicationContext.categoria'  is null.");
          }
            _context.categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (_context.categoria == null)
            {
                return NotFound();
            }
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return (_context.categoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
