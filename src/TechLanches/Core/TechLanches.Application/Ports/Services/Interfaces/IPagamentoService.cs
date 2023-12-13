﻿using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services.Interfaces;

public interface IPagamentoService
{
    Task<bool> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento);
    Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId);
    Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor);
    Task Aprovar(Pagamento pagamento);
    Task Reprovar(Pagamento pagamento); 
    Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago);
    Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial);
}
