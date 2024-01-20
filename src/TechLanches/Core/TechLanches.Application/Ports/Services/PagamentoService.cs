﻿using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _repository;
        private readonly IServiceProvider _serviceProvider;

        private static string UsuarioId = AppSettings.Configuration.GetSection($"ApiMercadoPago:UserId").Value;
        private static string PosId = AppSettings.Configuration.GetSection($"ApiMercadoPago:PosId").Value;

        public PagamentoService(IPagamentoRepository repository,
                                IPagamentoACLService pagamentoACLService)
        {
            _repository = repository;
            _pagamentoACLService = pagamentoACLService;
        }

        public async Task Aprovar(Pagamento pagamento)
        {
            pagamento.Aprovar();
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task Reprovar(Pagamento pagamento)
        {
            pagamento.Reprovar();
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            var pagamento = await _repository.BuscarPagamentoPorPedidoId(pedidoId);

            if (pagamento is null)
                throw new DomainException($"Não foi encontrado nenhum pedido com id: {pedidoId}.");

            return pagamento;
        }

        public async Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            var pagamentoExistente = await _repository.BuscarPagamentoPorPedidoId(pedidoId);

            if (pagamentoExistente is not null)
                throw new DomainException($"Pagamento já efetuado para o pedido: {pedidoId}.");

            Pagamento pagamento = new(pedidoId, valor, formaPagamento);
            await _repository.Cadastrar(pagamento);
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento)
        {
            var pagamento = await BuscarPagamentoPorPedidoId(pedidoId);

            if (statusPagamento == StatusPagamentoEnum.Aprovado)  
                pagamento.Aprovar();  
            else
                pagamento.Reprovar();

            await _repository.UnitOfWork.Commit();

            return pagamento.StatusPagamento == StatusPagamento.Aprovado;
        }

        public async Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago)
        {
            var pedido = JsonSerializer.Serialize(pedidoMercadoPago);

            var resultado = await _pagamentoACLService.GerarPagamentoEQrCode(pedido, UsuarioId, PosId);

            return resultado;
        }

        //exemplo de pedidoId = 13971205222
        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial)
            => await _pagamentoACLService.ConsultarPagamento(pedidoComercial);

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMockado(string pedidoComercial)
            => await _pagamentoACLService.ConsultarPagamento(pedidoComercial);

        public Task<string> GerarPagamentoEQrCodeMockado(PedidoACLDTO pedidoMercadoPago)
        {
            if (isMockado)
                return _serviceProvider.GetRequiredService(typeof(IMercadoPagoMockadoService)) as IPagamentoACLService;

            return _serviceProvider.GetService(typeof(IMercadoPagoService)) as IPagamentoACLService;
        }
    }
}
