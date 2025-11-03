using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace p2_PA1_P2.Models;

public class Pedido
{
    [Key]
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]

    public DateTime Fecha = DateTime.Now;

    public string NombreCliente { get; set; }
    public double Total { get; set; }

    [InverseProperty("Pedido")]
    public ICollection<PedidoDetalle> detalle { get; set; }

}
