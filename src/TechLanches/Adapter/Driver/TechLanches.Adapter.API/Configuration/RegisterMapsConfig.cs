﻿using Mapster;
using System.Reflection;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.API.Configuration
{
    public static class RegisterMapsConfig
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<Produto, ProdutoResponseDTO>.NewConfig()
                .Map(dest => dest.Categoria, src => CategoriaProduto.From(src.Categoria.Id));

            TypeAdapterConfig<Pedido, PedidoResponseDTO>.NewConfig()
                .Map(dest => dest.StatusPedido, src => src.StatusPedido.ToString())
                .Map(dest => dest.NomeCliente, src => src.Cliente == null ? MensagensConstantes.CLIENTE_NAO_IDENTIFICADO : src.Cliente.Nome);

            TypeAdapterConfig<Pedido, PedidoACLDTO>.NewConfig();

            TypeAdapterConfig<ItemPedido, ItemPedidoACLDTO>.NewConfig()
               .Map(dest => dest.NomeProduto, src => src.Produto.Nome);

            TypeAdapterConfig<ItemPedido, ItemPedidoResponseDTO>.NewConfig()
                .Map(dest => dest.NomeProduto, src => src.Produto.Nome);

            TypeAdapterConfig<Cliente, ClienteResponseDTO>.NewConfig()
                .Map(dest => dest.Email, src => src.Email.EnderecoEmail)
                .Map(dest => dest.CPF, src => src.CPF.Numero);

            TypeAdapterConfig<Pagamento, PagamentoStatusResponseDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.PedidoId)
                .Map(dest => dest.StatusPagamento, src => src.StatusPagamento.ToString());

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}