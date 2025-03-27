using System;
using System.Collections.Generic;

namespace BEseguridad.Models;

public partial class CategoriasAmenaza
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Amenaza> Amenazas { get; set; } = new List<Amenaza>();
}
