using System;
using System.Collections.Generic;

namespace DL;

public partial class Calculadora
{
    public int IdCalculadora { get; set; }

    public string? Numero { get; set; }

    public int? Resultado { get; set; }

    public DateTime? FechaHora { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
    public int? Existe { get; set; }
}
