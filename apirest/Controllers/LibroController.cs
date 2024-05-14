using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apirest.Conexion;
using apirest.Model;
using apirest.Dto;
using apirest.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace apirest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly AplicationContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LibroController(AplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Libro
        [HttpGet]
        public async Task<ActionResult<Serializer<LibroDto>>> Getlibro(int page = 1, string nombre = "")
        {
            LibroSearch model = new LibroSearch(_httpContextAccessor, _context);
            return await model.Search(page, nombre);
        }


        // GET: api/Libro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
          if (_context.libro == null)
          {
              return NotFound();
          }
            var libro = await _context.libro.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // PUT: api/Libro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
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

        // POST: api/Libro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
          if (_context.libro == null)
          {
              return Problem("Entity set 'AplicationContext.libro'  is null.");
          }
            _context.libro.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
        }

        // DELETE: api/Libro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            if (_context.libro == null)
            {
                return NotFound();
            }
            var libro = await _context.libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.libro.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return (_context.libro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
