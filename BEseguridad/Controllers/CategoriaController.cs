using AutoMapper;
using BEseguridad.Dtos;
using BEseguridad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEseguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly SeguridadInformaticaContext _context;
        private readonly IMapper _mapper;

        public CategoriaController(SeguridadInformaticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriasAmenaza>>> GetCategorias()
        {
            return await _context.CategoriasAmenazas.ToListAsync();
   
        }

    }
}
