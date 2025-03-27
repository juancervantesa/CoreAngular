using System;
using System.Collections.Generic;

namespace BEseguridad.Models;

public partial class Amenaza
{
    public int AmenazaId { get; set; }

    public int? CategoriaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int NivelRiesgo { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public virtual CategoriasAmenaza? Categoria { get; set; }
}
