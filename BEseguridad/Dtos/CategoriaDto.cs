namespace BEseguridad.Dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }

        public int AmenazaId { get; set; }

        public string Titulo { get; set; } 

        public string Descripcion { get; set; } 

        public int NivelRiesgo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

    }
}
