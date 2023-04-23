using Microsoft.Extensions.Configuration;
using System;
using System.Xml.Linq;

namespace MonitoringSystem
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string _ip, _comment;
        private int _port;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _ip = configuration["TcpFunction:Ip"];
            _port = int.Parse(configuration["TcpFunction:Port"]);
            _comment = configuration["TcpFunction:Comment"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Connector.ConnectTCPIP(_ip, _port, _comment);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}