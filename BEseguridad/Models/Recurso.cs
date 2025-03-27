using System;
using System.Collections.Generic;

namespace BEseguridad.Models;

public partial class Recurso
{
    public int RecursoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? EnlaceUrl { get; set; }
}
