namespace Banco.Bari.ConsoleAPp.Providers
{
    public interface IRabbitMQProvider
    {
        void Publish(string message, string topic);

        void Subscribe(string topic);

    }
}
