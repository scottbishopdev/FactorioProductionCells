using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FRCDataAccessLibrary;

namespace ModUpdateScheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private HttpClient _httpClient;
        private IModService _modService;
        

        public Worker(
            ILogger<Worker> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ModUpdateScheduler starting up at {Time}", DateTime.UtcNow);
            
            _httpClient = new HttpClient();
            _modService = new ModService(_httpClient);

            
            // Hostname will probably have top change here.
            var factory = new ConnectionFactory() { HostName = "RabbitMQ" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // This statement declares (and creates?) a queue with the name startUpdate. We should do this in both clients and producers, as we can't be sure which will start first.
                    channel.QueueDeclare(queue: "startUpdate",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    // This is where we're defining what to do when we receive a message to the above queue.
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        // Is the "Encoding.UTF8" bit necessary
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    // Huh?
                    channel.BasicConsume(queue: "startUpdate",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
            

            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<ModDTO> newModList = await _modService.GetAllMods();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var modDb = scope.ServiceProvider.GetRequiredService<ModContext>();

                int counter = 0;
                foreach(ModDTO newMod in newModList)
                {
                    _logger.LogInformation($"Searching for {newMod.Name} in database...");

                    // TODO: Implement error handling for a failed DB connection.
                    try
                    {
                        var dbMod = modDb.Mods
                            .Include(mod => mod.Releases)
                            .SingleOrDefault(m => m.Name == newMod.Name);

                        if(dbMod == null)
                        {
                            // We didn't find a matching mod, so we need to add this new one to the database.



                        }
                        if(dbMod.Releases.OrderBy(r => r.AddDate).Single().ReleasedAt < newMod.LatestRelease.ReleasedAt)
                        {
                            // We found a matching mod, but it needs to be updated.
                        }
                        else
                        {
                            // We found a matching mod and it doesn't need to be updated.
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.Source);
                        Console.WriteLine(ex.StackTrace);
                    }

                    if(counter >= 10)
                    {
                        break;
                    }
                    counter++;
                }
            }

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
            _logger.LogInformation("ModUpdateScheduler shutting down at {Time}", DateTime.UtcNow);

            _httpClient.Dispose();

            return base.StopAsync(stoppingToken);
        }
    }
}
