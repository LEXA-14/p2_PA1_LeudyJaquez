using Microsoft.EntityFrameworkCore;
using p2_PA1_P2.DAL;
using p2_PA1_P2.Models;
using System.Linq.Expressions;

namespace p2_PA1_P2.Service;

public class PedidoService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Guardar(Pedido pedido)
    {
        if (!await Existe(pedido.NombreCliente))
        {
            return await Insertar(pedido);

        }
        else
        {
            return  await Modificar(pedido);
        }
    }

    private async Task AfectarPedido(PedidoDetalle[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto=await dbFactory.CreateDbContextAsync();

        foreach(var item in detalle)
        {
            var componente=await contexto.Componentes.SingleOrDefaultAsync(c=>c.ComponenteId == item.ComponenteId);

            if(componente != null)
            {
                if (tipoOperacion == TipoOperacion.Suma)
                {
                    componente.Existencia += item.Cantidad;
                }
                else
                {
                    componente.Existencia-=item.Cantidad;
                }
            }
        }await contexto.SaveChangesAsync();
    }

    private async Task<bool> Modificar(Pedido pedido)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var pedidoOriginal=await contexto.Pedido.Include(d=>d.detalle)
            .AsNoTracking().
            SingleOrDefaultAsync(p=>p.PedidoId==pedido.PedidoId);

        if (pedidoOriginal == null) return false;

        await AfectarPedido(pedidoOriginal.detalle.ToArray(), TipoOperacion.Resta);

        var detalleAnterior=await contexto.PedidoDetalle.Where(d=>d.PedidoId==pedido.PedidoId).ToListAsync();

        contexto.PedidoDetalle.RemoveRange(detalleAnterior);

        await AfectarPedido(pedido.detalle.ToArray(), TipoOperacion.Suma);

        contexto.Update(pedido);
        return await contexto.SaveChangesAsync() > 0;


    }

    private async Task<bool> Insertar( Pedido pedido)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Add(pedido);
        await AfectarPedido(pedido.detalle.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Existe(string nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Pedido.AnyAsync(a => a.NombreCliente.ToLower() == nombre.ToLower());
    }
    //eliminar

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var pedido = await contexto.Componentes.Include(d => d.detalle)
             .FirstOrDefaultAsync(e => e.ComponenteId == id);

        if (pedido == null) return false;

        await AfectarPedido(pedido.detalle.ToArray(), TipoOperacion.Suma);

        contexto.PedidoDetalle.RemoveRange(pedido.detalle);
        contexto.Componentes.Remove(pedido);

        return await contexto.SaveChangesAsync() > 0;

    }
   
    public async Task<Pedido?>BuscarId(int id)
    {
        await using var contexto=await dbFactory.CreateDbContextAsync();

        return await contexto.Pedido.Include(d => d.detalle).
            ThenInclude(c => c.Componente).
            FirstOrDefaultAsync(p => p.PedidoId == id);
    }

    //Listar 

    public async Task<List<Pedido>> GetLista(Expression<Func<Pedido, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.Pedido.Include(d=>d.detalle).
            ThenInclude(c=>c.Componente)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Componente>> GetListaComponente()
    {
        await using var contexto=await dbFactory.CreateDbContextAsync();

        return await contexto.Componentes.AsNoTracking().ToListAsync();
    }

    public enum TipoOperacion
    {
        Suma=1,
        Resta=2
    }

}
