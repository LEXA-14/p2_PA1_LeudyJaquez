using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace p2_PA1_P2.Models;

public class PedidoDetalle
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    public int PedidoId { get; set; }
    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    public int ComponenteId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "La Cantidad no puede ser negativa")]
    public int Cantidad { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "El Precio no puede ser negativo")]
    public double Precio { get; set; }

    [ForeignKey("PedidoId")]
    public virtual Pedido pedido { get; set; }

    [ForeignKey("ComponenteId")]
    public virtual Componente Componente { get; set; }
}
