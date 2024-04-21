namespace AutoTradeHubViewService.RabbitMQ
{
	public interface IRabbitMqService
	{
		void SendMessage(object obj, string routingKey, string commandName);
		void SendMessage(string message, string routingKey, string commandName);
	}
}

