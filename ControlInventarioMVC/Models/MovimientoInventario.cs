using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControlInventarioMVC.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }

        [Required]
        public int ArticuloId { get; set; }

        [ForeignKey("ArticuloId")]
        public Articulo Articulo { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        public Ubicacion Ubicacion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string TipoMovimiento { get; set; }
    }

}
