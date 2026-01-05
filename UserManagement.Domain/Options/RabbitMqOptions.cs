namespace UserManagement.Domain.Options
{
    public sealed class RabbitMqOptions
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> ReceivedEndPoints { get; set; }
    }

}
