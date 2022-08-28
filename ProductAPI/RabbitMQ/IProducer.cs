namespace ProductAPI.RabbitMQ
{
    public interface IProducer
    {
        public void SendMessage<T>(T message);
    }
}
