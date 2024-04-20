namespace AutoTradeHubDataWebApi.RabbitMQ
{
	public interface IRabbitMqService
	{
		void SendMessage(object obj, string routingKey);
		void SendMessage(string message, string routingKey);
	}
}

