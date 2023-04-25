using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitoringSystem
{
    public class GetData : IJob
    {
        private readonly ILogger<GetData> _logger;
        private readonly IConfiguration _configuration;
        public GetData(ILogger<GetData> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("GetData running at: {time}", DateTimeOffset.Now);
            UpdateAppSetting("test", "192.168.1.7:99");
            // Code that sends a periodic email to the user (for example)
            // Note: This method must always return a value 
            // This is especially important for trigger listers watching job execution 
            return Task.FromResult(true);
        }

        public void UpdateAppSetting(string key, string value)
        {
            try
            {
                var configJson = File.ReadAllText("appsettings.json");
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(configJson);
                if (config!= null)
                {
                    config[key] = value;
                    var updatedConfigJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText("appsettings.json", updatedConfigJson);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("GetData running at: {time} with error {error}", DateTimeOffset.Now, ex);
            }
        }
    }
}
