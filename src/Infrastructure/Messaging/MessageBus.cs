using HexaContent.Core.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace HexaContent.Infrastructure.Messaging;

public partial class MessageBus(IConnection _connection) : IMessageBus
{
	IModel _channel = _connection.CreateModel();

	public async Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage
	{
		string exchange = typeof(TMessage).Name;
		_channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
		var body = JsonSerializer.SerializeToUtf8Bytes(message);
		await Task.Run(() =>
		{
			_channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);
		});
	}

	public async Task EnqueueAsync<TMessage>(TMessage message) where TMessage : IMessage
	{
		string queue = typeof(TMessage).Name;
		_channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);

		var properties = _channel.CreateBasicProperties();
		properties.Persistent = true;

		var body = JsonSerializer.SerializeToUtf8Bytes(message);

		await Task.Run(() =>
		{
			_channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
		});
	}

	public void Subscribe<TMessage>(Action<TMessage> onMessageReceived) where TMessage : IMessage
	{
		string exchange = typeof(TMessage).Name;

		_channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
		var queueName = _channel.QueueDeclare().QueueName;
		_channel.QueueBind(queue: queueName, exchange: exchange, routingKey: "");

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = JsonSerializer.Deserialize<TMessage>(body.AsSpan());
			if (message != null)
			{
				onMessageReceived(message);
			}
		};

		_channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

	}

	public void SubscribeToQueue <TMessage>(Func<TMessage, bool> onMessageReceived) where TMessage : IMessage
	{
		string queue = typeof(TMessage).Name;

		_channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = JsonSerializer.Deserialize<TMessage>(body.AsSpan());

			bool success = false;

			if (message != null)
			{
				success = onMessageReceived(message);
			}

			if (success)
			{
				// Confirm that the message was processed successfully
				_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
			}
			else
			{
				// Requeue the message for retry
				_channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
				Thread.Sleep(5000); // Wait before retrying (configurable delay)
			}
		};

		_channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
	}
}
