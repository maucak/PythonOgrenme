using PythonOgrenme.Domain.Events;

namespace PythonOgrenme.Domain.Interfaces;

public interface IRabbitMQPublisher
{
    Task PublishAsync<T>(T domainEvent) where T : IDomainEvent;
}