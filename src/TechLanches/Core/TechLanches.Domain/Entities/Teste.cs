using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.Validations;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Entities
{
    public class Teste : Usuario
    {
        private Teste()
        {

        }

        public Teste(
            string nome,
            string email,
            string cpf) : base(nome, email)
        {
            CPF = new Cpf(cpf);
        }

        public Cpf CPF { get; private set; }

        public IReadOnlyCollection<Pedido> Pedidos { get; private set; }

    }

    public class StatusPedidoValidacaoService : IStatusPedidoValidacaoService
    {
        private readonly IEnumerable<IStatusPedidoValidacao> Validacoes;

        public StatusPedidoValidacaoService(IEnumerable<IStatusPedidoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido)
        {
            var validacao = Validacoes.FirstOrDefault(x => x.StatusPedido == novoStatusPedido)
                ?? throw new NotImplementedException($"{nameof(StatusPedido)} {novoStatusPedido} não implementado.");

            var valido = validacao.Validar(statusPedido);

            if (!valido) throw new DomainException("O status selecionado não é válido");
        }
    }

    public class StatusPedidoValidacaoService2 : IStatusPedidoValidacaoService
    {
        private readonly IEnumerable<IStatusPedidoValidacao> Validacoes;

        public StatusPedidoValidacaoService2(IEnumerable<IStatusPedidoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido)
        {
            var validacao = Validacoes.FirstOrDefault(x => x.StatusPedido == novoStatusPedido)
                ?? throw new NotImplementedException($"{nameof(StatusPedido)} {novoStatusPedido} não implementado.");

            var valido = validacao.Validar(statusPedido);

            if (!valido) throw new DomainException("O status selecionado não é válido");
        }
    }

    public class StatusPedidoValidacaoService3 : IStatusPedidoValidacaoService
    {
        private readonly IEnumerable<IStatusPedidoValidacao> Validacoes;

        public StatusPedidoValidacaoService3(IEnumerable<IStatusPedidoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido)
        {
            var validacao = Validacoes.FirstOrDefault(x => x.StatusPedido == novoStatusPedido)
                ?? throw new NotImplementedException($"{nameof(StatusPedido)} {novoStatusPedido} não implementado.");

            var valido = validacao.Validar(statusPedido);

            if (!valido) throw new DomainException("O status selecionado não é válido");
        }
    }

    public class StatusPedidoValidacaoService4 : IStatusPedidoValidacaoService
    {
        private readonly IEnumerable<IStatusPedidoValidacao> Validacoes;

        public StatusPedidoValidacaoService4(IEnumerable<IStatusPedidoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido)
        {
            var validacao = Validacoes.FirstOrDefault(x => x.StatusPedido == novoStatusPedido)
                ?? throw new NotImplementedException($"{nameof(StatusPedido)} {novoStatusPedido} não implementado.");

            var valido = validacao.Validar(statusPedido);

            if (!valido) throw new DomainException("O status selecionado não é válido");
        }
    }

}
