using Microsoft.EntityFrameworkCore;
using p2_PA1_P2.DAL;
using p2_PA1_P2.Models;
using System.Linq.Expressions;

namespace p2_PA1_P2.Service;

public class RegistroService(IDbContextFactory<Contexto> dbFactory)
{


    //Listar 

    public async Task<List<Registro>> GetLista(Expression<Func<Registro, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.Registro
            .Where(criterio)
            .ToListAsync();
    }

}
