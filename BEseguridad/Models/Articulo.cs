using System;
using System.Collections.Generic;

namespace BEseguridad.Models;

public partial class Articulo
{
    public int ArticuloId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime? FechaPublicacion { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
