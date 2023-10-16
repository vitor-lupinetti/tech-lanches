﻿using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<Pedido> BuscarPedidoPorId(int idPedido);
        Task<List<Pedido>> BuscarPedidosPorStatus(StatusPedido statusPedido);
    }
}