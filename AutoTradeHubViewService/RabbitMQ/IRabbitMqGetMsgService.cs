namespace AutoTradeHubViewService.RabbitMQ
{
    public interface IRabbitMqGetMsgService
    {
        string GetMessage(string queueName);
    }
}
