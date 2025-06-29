using MikrotikDotNet;

namespace Mikrotik.Exporter.Providers
{
    public class MikrotikMetricsProvider(MikrotikConfigurationProvider cfg) : IDisposable
    {
        private readonly MKConnection _connection = new(cfg.Host, cfg.Username, cfg.Password, cfg.Port);

        public bool IsConnected()
        {
            if (!_connection.IsOpen)
            {
                _connection.Open();
            }
            return _connection.IsOpen;
        }

        public IEnumerable<string> ExecuteCommand(string command)
        {
            if (!IsConnected())
            {
                throw new InvalidOperationException("Connection to Mikrotik device is not established.");
            }
            var cmd = _connection.CreateCommand(command);
            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
