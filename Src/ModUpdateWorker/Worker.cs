using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Infrastructure.Services.ModPortalService;

namespace FactorioProductionCells.ModUpdateWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _httpClient;
        private ModPortalService _modPortalService;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ModUpdateWorker starting up at {Time}", DateTime.UtcNow);
            
            _httpClient = new HttpClient();
            _modPortalService = new ModPortalService(_httpClient);

            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IModDTO newMod = await _modPortalService.GetModByName("boblibrary");
            newMod.PrintMod();

            /*
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60*1000, stoppingToken);
            }
            */
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ModUpdateWorker shutting down at {Time}", DateTime.UtcNow);

            _httpClient.Dispose();

            return base.StopAsync(stoppingToken);
        }
    }
}
