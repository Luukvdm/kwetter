using System;
using RabbitMQ.Client;

namespace Kwetter.BuildingBlocks.EventBus.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}