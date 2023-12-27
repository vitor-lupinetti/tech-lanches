using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoService _filaPedidoService;
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<FilaPedidosHostedService> _logger;
        private readonly WorkerOptions _workerOptions;
        private const string QUEUENAME = "pedidos";

        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoService filaPedidoService,
            IOptions<WorkerOptions> workerOptions,
            IPedidoService pedidoService)
        {
            _logger = logger;
            _filaPedidoService = filaPedidoService;
            _workerOptions = workerOptions.Value;
            _pedidoService = pedidoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "123456", DispatchConsumersAsync = true }; // Configurações de conexão com o RabbitMQ
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUENAME, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {

                var body = ea.Body.ToArray();
                var pedidoId = Convert.ToInt32(Encoding.UTF8.GetString(body));
                await ProcessMessageAsync(pedidoId, stoppingToken);
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: QUEUENAME, autoAck: false, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public async Task ProcessMessageAsync(int pedidoId, CancellationToken stoppingToken)
        {
            _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", pedido.Id);

            if (pedido.StatusPedido != StatusPedido.PedidoEmPreparacao)
                await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoEmPreparacao);

            _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", pedido.Id);

            await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos, stoppingToken);

            _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", pedido.Id);

            await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoPronto);

            _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", pedido.Id);
        }
    }

}