using Microsoft.Extensions.Configuration;
using Quartz.Util;
using System;
using System.Xml.Linq;

namespace MonitoringSystem
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string _ip, _comment;
        private int _port;
        private string _domain;
        private string _functionUrl;
        private int _delay;
        private string _securityKey;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _ip = configuration["TcpFunction:Ip"];
            _port = int.Parse(configuration["TcpFunction:Port"]);
            _comment = configuration["TcpFunction:Comment"];
            _domain = configuration["HttpServer:Domain"];
            _functionUrl = configuration["HttpServer:PostDataUrl"];
            _delay = int.Parse(configuration["TcpFunction:Delay"]);
            _securityKey = configuration["securityKey"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(_delay, stoppingToken);
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    try
                    {
                        var responseTcp = Connector.ConnectTCPIP(_ip, _port, _comment);

                        var httpClient = new HttpClient();
                        var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("responseData", responseTcp)
                        });
                        httpClient.DefaultRequestHeaders.Add("SecurityKey", _securityKey);
                        var response = await httpClient.PostAsync($"{_domain}{_functionUrl}", formContent);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
        }
    }
}