using System.ComponentModel.DataAnnotations;

namespace p2_PA1_P2.Models;

public class Registro
{
    [Key]

    public int Id { get; set; }
    [Required(ErrorMessage ="Este campo es requerido")]
    public DateTime Fecha=DateTime.Now;

    public int Cantidad { get; set; }
}
