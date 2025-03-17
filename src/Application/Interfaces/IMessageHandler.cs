namespace Application.Interfaces;

public interface IMessageHandler<in TMessage>
{
    Task HandleAsync(TMessage message, CancellationToken cts);
}