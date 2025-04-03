using System.ComponentModel.DataAnnotations;

namespace ControlInventarioMVC.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public ICollection<MovimientoInventario> MovimientoInventario { get; set; } = new List<MovimientoInventario>();
    }
}
