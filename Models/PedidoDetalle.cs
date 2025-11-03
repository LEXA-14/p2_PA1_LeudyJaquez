using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace p2_PA1_P2.Models;

public class PedidoDetalle
{
    [Key]
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int ComponenteId { get; set; }
    public int Cantidad { get; set; }

    public double Precio { get; set; }

    [ForeignKey("PedidoId")]
    public virtual Pedido pedido { get; set; }

    [ForeignKey("ComponenteId")]
    public virtual Componente Componente { get; set; }
}
