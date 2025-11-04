using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace p2_PA1_P2.Models;

public class Componente
{
    [Key]
    public int ComponenteId { get; set; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    public string Descripcion { get; set; } = string.Empty;

    public double Precio { get; set; }

    public int Existencia { get; set; }

    [InverseProperty("Componente")]
    public ICollection<PedidoDetalle> detalle { get; set; }
}
