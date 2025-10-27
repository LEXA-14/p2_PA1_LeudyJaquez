using p2_PA1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace p2_PA1_P2.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Registro> Registro { get; set; }
}
