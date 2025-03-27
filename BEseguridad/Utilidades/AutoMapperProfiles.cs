using AutoMapper;
using BEseguridad.Dtos;
using BEseguridad.Models;

namespace BEseguridad.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            CreateMap<CategoriasAmenaza, CategoriaDto>()
                
                ;

        }
    }
}
