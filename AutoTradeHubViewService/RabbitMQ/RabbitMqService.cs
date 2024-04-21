using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using AutoTradeHubViewService.Data;
using System.Diagnostics;

namespace AutoTradeHubViewService.RabbitMQ
{
	public class RabbitMqService : IRabbitMqService
	{

        public void SendMessage(object obj, string _routingKey, string commandName)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message, _routingKey, commandName);
		}

		public void SendMessage(string message, string _routingKey, string commandName)
		{
			var factory = new ConnectionFactory() { Uri = new Uri(MyConfig.CloudAMQPUri) };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queue: _routingKey,
						   durable: true,
						   exclusive: false,
						   autoDelete: false,
						   arguments: null);

			var body = Encoding.UTF8.GetBytes(message);
			var props = channel.CreateBasicProperties();
            props.Headers = new Dictionary<string, object>();
            props.Headers["CommandName"] = commandName;

            channel.BasicPublish(exchange: "AutoTradeHubWebApi",
						   routingKey: _routingKey,
						   basicProperties: props,
						   body: body);
			Debug.WriteLine($"Сообщение отправлено: очередь: {_routingKey}, команда: {commandName}, сообщение: {message}");
		}
	}
}
