using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using AutoTradeHubDataWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using AutoTradeHubDataWebApi.Controllers;

namespace AutoTradeHubDataWebApi.RabbitMQ
{
	public class RabbitMqListener : BackgroundService
	{
		private IConnection _connection;
		private IModel _channel;
		private readonly string _queueName;

		public RabbitMqListener()
		{
			_queueName = "MyQueue";
			var factory = new ConnectionFactory { Uri = new Uri(MyConfig.CloudAMQPUri) };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, eventArgs) =>
			{
				var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

				// Каким-то образом обрабатываем полученное сообщение
				Debug.WriteLine($"Получено сообщение: {content}");

				HttpClient httpClient = new HttpClient();
				httpClient.GetAsync("http://localhost:55281/api/Car");

				_channel.BasicAck(eventArgs.DeliveryTag, false);
			};

			_channel.BasicConsume(_queueName, false, consumer);

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_channel.Close();
			_connection.Close();
			base.Dispose();
		}
	}
}
