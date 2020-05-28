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
        private ModService _modService;

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

            /*
            // Hostname will probably have top change here.
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // This statement declared (and creates?) a queue with the name startUpdate. We should do this in both clients and producers, as we can't be sure which will start first.
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
            */

            // Is this where I should tie into FluentMigrator?

            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<ModDTO> modList = await _modService.GetAllMods();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModContext>();

                int counter = 0;
                foreach(ModDTO mod in modList)
                {
                    Console.WriteLine($"Looking for mod {mod.Name} in database...");

                    try
                    {
                        var dbMod = db.Mods
                            .Include(mod => mod.Releases)
                            .SingleOrDefault(m => m.Name == mod.Name);

                        if(dbMod != null)
                        {
                            Console.WriteLine($"    Mod {mod.Name} was found and loaded:");
                            Console.WriteLine($"    mod.id: {dbMod.Id}");
                            Console.WriteLine($"    mod.name: {dbMod.Name}");
                            Console.WriteLine($"    mod.add_date: {dbMod.AddDate}");
                            Console.WriteLine($"    mod.update_date: {dbMod.UpdateDate}");

                            foreach(Release dbRelease in dbMod.Releases)
                            {
                                Console.WriteLine($"    mod_release.version: {dbRelease.Version}");
                                Console.WriteLine($"        mod_release.id: {dbRelease.Id}");
                                Console.WriteLine($"        mod_release.mod_id: {dbRelease.ModId}");
                                Console.WriteLine($"        mod_release.add_date: {dbRelease.AddDate}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"    Mod {mod.Name} could not be found.");
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
