namespace Mikrotik.Exporter.Providers
{
    public class MikrotikConfigurationProvider
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public MikrotikConfigurationProvider(IConfiguration configuration)
        {
            Host = configuration["MIKROTIK_HOST"] 
                ?? throw new ArgumentNullException(nameof(configuration), "MIKROTIK_HOST is not set in environment variables or configuration.");
            string portStr = configuration["MIKROTIK_PORT"] ?? "8728"; // Default port for Mikrotik API
            if (!ushort.TryParse(portStr, out ushort port))
            {
                throw new ArgumentException($"Invalid port number: {portStr}");
            }
            Port = port;
            Username = configuration["MIKROTIK_USERNAME"] 
                ?? throw new ArgumentNullException(nameof(configuration), "MIKROTIK_USERNAME is not set in environment variables or configuration.");
            Password = configuration["MIKROTIK_PASSWORD"] 
                ?? throw new ArgumentNullException(nameof(configuration), "MIKROTIK_PASSWORD is not set in environment variables or configuration.");
        }
    }
}
