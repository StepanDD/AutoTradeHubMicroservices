using AutoTradeHubViewService.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace AutoTradeHubViewService.RabbitMQ
{
    public class RabbitMqGetMsgService : IRabbitMqGetMsgService
    {

        public string GetMessage(string queueName)
        {
            var factory = new ConnectionFactory { Uri = new Uri(MyConfig.CloudAMQPUri) };
            var _connection = factory.CreateConnection();
            var _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            string content = "";
            var data = _channel.BasicGet(queueName, true);

            if (data != null)
            {
                content = Encoding.UTF8.GetString(data.Body.ToArray());
                Debug.WriteLine($"Получено сообщение: {content}");
            }

            _channel.Close();
            _connection.Close();
            return content;
        }
    }
}
