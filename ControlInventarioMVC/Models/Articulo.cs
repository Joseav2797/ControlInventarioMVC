using System;
using System.Collections.Generic;

namespace ControlInventarioMVC.Models;

public partial class Articulo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Stock { get; set; }

    public ICollection<MovimientoInventario> MovimientoInventario { get; set; } = new List<MovimientoInventario>();


}
