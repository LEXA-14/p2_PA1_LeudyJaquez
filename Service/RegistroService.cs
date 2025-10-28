using Microsoft.EntityFrameworkCore;
using p2_PA1_P2.DAL;
using p2_PA1_P2.Models;
using System.Linq.Expressions;

namespace p2_PA1_P2.Service;

public class RegistroService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Guardar(Registro registro)
    {
        if (!await Existe(registro.Nombre))
        {
            return await Insertar(registro);

        }
        else
        {
            return false;
        }
    }
    private async Task<bool> Insertar( Registro aportes)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Add(aportes);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Existe(string nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Registro.AnyAsync(a => a.Nombre.ToLower() == nombre.ToLower());
    }

    //Listar 

    public async Task<List<Registro>> GetLista(Expression<Func<Registro, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.Registro
            .Where(criterio)
            .ToListAsync();
    }

}
