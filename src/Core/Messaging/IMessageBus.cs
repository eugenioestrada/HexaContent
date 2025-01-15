namespace HexaContent.Core.Messaging;

/// <summary>
/// Interface for a message bus that supports publishing, enqueuing, and subscribing to messages.
/// </summary>
public interface IMessageBus
{
	/// <summary>
	/// Publishes a message to a specified exchange.
	/// </summary>
	/// <typeparam name="TMessage">The type of the message.</typeparam>
	/// <param name="message">The message to publish.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

	/// <summary>
	/// Enqueues a message to a specified queue.
	/// </summary>
	/// <typeparam name="TMessage">The type of the message.</typeparam>
	/// <param name="message">The message to enqueue.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task EnqueueAsync<TMessage>(TMessage message) where TMessage : IMessage;

	/// <summary>
	/// Subscribes to a specified topic and processes received messages.
	/// </summary>
	/// <typeparam name="TMessage">The type of the message.</typeparam>
	/// <param name="onMessageReceived">The action to perform when a message is received.</param>
	void Subscribe<TMessage>(Action<TMessage> onMessageReceived) where TMessage : IMessage;

	/// <summary>
	/// Subscribes to a specified queue and processes received messages.
	/// </summary>
	/// <typeparam name="TMessage">The type of the message.</typeparam>
	/// <param name="onMessageReceived">The function to perform when a message is received. Returns true if the message was processed successfully, otherwise false.</param>
	void SubscribeToQueue<TMessage>(Func<TMessage, bool> onMessageReceived) where TMessage : IMessage;
}
