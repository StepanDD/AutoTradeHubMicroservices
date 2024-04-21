using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using AutoTradeHubDataWebApi.Data;

namespace AutoTradeHubDataWebApi.RabbitMQ
{
	public class RabbitMqService : IRabbitMqService
	{

        public void SendMessage(object obj, string _routingKey)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message, _routingKey);
		}

		public void SendMessage(string message, string _routingKey)
		{
			var factory = new ConnectionFactory() { Uri = new Uri(MyConfig.CloudAMQPUri) };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			var queueArgs = new Dictionary<string, object>();
			queueArgs["x-expires"] = 1000; // Попробовать добить
			channel.QueueDeclare(queue: _routingKey,
						   durable: true,
						   exclusive: false,
						   autoDelete: false,
						   arguments: null);

			channel.QueueBind(_routingKey, "AutoTradeHubViewService", _routingKey);

			var body = Encoding.UTF8.GetBytes(message);

			channel.BasicPublish(exchange: "AutoTradeHubViewService",
						   routingKey: _routingKey,
						   basicProperties: null,
						   body: body);
		}
	}
}
