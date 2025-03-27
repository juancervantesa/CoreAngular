using BEseguridad.Dtos;
using BEseguridad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BEseguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AmenazasController : ControllerBase
    {
        
        private readonly SeguridadInformaticaContext _context;
        public AmenazasController(SeguridadInformaticaContext context)
        {
            _context = context;
        }
        // GET: api/Amenazas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAmenazas()
        {

            // return await _context.Amenazas.ToListAsync();
            //return await _context.CategoriasAmenazas.FromSqlRaw("SELECT * FROM CategoriasAmenazas").ToListAsync();
            return await _context.Database.SqlQueryRaw<CategoriaDto>("SELECT [c].[CategoriaID], [c].[Nombre], [a].[AmenazaID], [a].[Titulo],  [a].[Descripcion], [a].[NivelRiesgo], [a].[FechaPublicacion]\r\nFROM [CategoriasAmenazas] AS [c]\r\nLEFT JOIN [Amenazas] AS [a] ON [c].[CategoriaID] = [a].[CategoriaID]\r\nORDER BY [c].[CategoriaID]").ToListAsync();
            // return await _context.CategoriasAmenazas.Include(x => x.Amenazas).ToListAsync();
        }
      
        // GET: api/Amenazas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenaza>> GetAmenaza(int id)
        {
            var amenaza = await _context.Amenazas.FindAsync(id);
            if (amenaza == null)
            {
                return NotFound();
            }
            return amenaza;
        }
        // POST: api/Amenazas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Amenaza> PostAmenaza(Amenaza amenaza)
        {
            _context.Amenazas.Add(amenaza);
            _context.SaveChanges();
            return CreatedAtAction("GetAmenaza", new { id = amenaza.AmenazaId }, amenaza);
        }
        // PUT: api/Amenazas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutAmenaza(int id, Amenaza amenaza)
        {
            if (id != amenaza.AmenazaId)
            {
                return BadRequest();
            }
            _context.Entry(amenaza).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenazaExists(id))
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
        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public ActionResult<Amenaza> DeleteAmenaza(int id)
        {
            var amenaza = _context.Amenazas.Find(id);
            if (amenaza == null)
            {
                return NotFound();
            }
            _context.Amenazas.Remove(amenaza);
            _context.SaveChanges();
            return amenaza;
        }
        private bool AmenazaExists(int id)
        {
            return _context.Amenazas.Any(e => e.AmenazaId == id);
        }


    }
}
