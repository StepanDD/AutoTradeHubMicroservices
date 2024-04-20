﻿using AutoTradeHubDataWebApi.Data;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace AutoTradeHubDataWebApi.Services.RabbitMqListeners
{
	public class CarListener : BackgroundService
	{
		private IConnection _connection;
		private IModel _channel;
		private readonly string _queueName = "Car";

		public CarListener()
		{
			var factory = new ConnectionFactory { Uri = new Uri(MyConfig.CloudAMQPUri) };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, eventArgs) =>
			{
				var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
				var commandNameBytes = (byte[])eventArgs.BasicProperties.Headers["CommandName"];
				var commandName = Encoding.UTF8.GetString(commandNameBytes);

				Debug.WriteLine($"Получено сообщение: {content}, команда: {commandName}");

				//HttpClient httpClient = new HttpClient();
				//httpClient.GetAsync("http://localhost:55281/api/Car");

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