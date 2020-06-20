using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FactorioProductionCells.Application.Common.Interfaces;
using FactorioProductionCells.Application.Common.Interfaces.ModPortalService;
using FactorioProductionCells.Application.Mods.Queries.GetModByName;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.ModUpdateScheduler
{
    public class Worker : BackgroundService
    {
        private const Int32 SleepDuration = 3*60*1000;
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
        private readonly IModPortalService _modPortalService;
        private readonly IMessageQueue _messageQueue;
        private readonly IDateTimeService _dateTimeService;

        public Worker(
            ILogger<Worker> logger,
            IMediator mediator,
            IModPortalService modPortalService,
            IMessageQueue messageQueue,
            IDateTimeService dateTimeService)
        {
            _logger = logger;
            _mediator = mediator;
            _modPortalService = modPortalService;
            _messageQueue = messageQueue;
            _dateTimeService = dateTimeService;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"-----ModUpdateScheduler starting up at {_dateTimeService.GetCurrentTime()}");

            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // TODO: Subsequent loops after the first execution don't actually do anything. I need to figure out why.
                Int32 modsAdded = 0;
                Int32 modsUpdated = 0;
                Int32 modsUpToDate = 0;

                _logger.LogInformation($"ModUpdateService running check for any mods that needs to be added or updated at: {_dateTimeService.GetCurrentTime()}");

                List<IModDTO> newModList = (List<IModDTO>) await _modPortalService.GetAllMods();

                _logger.LogInformation($"Got the list of mods from the mod portal. Found {newModList.Count} mods.");

                int counter = 0;
                foreach(IModDTO newMod in newModList)
                {
                    // TODO: Implement error handling for a failed DB connection.
                    try
                    {
                        _logger.LogInformation($"Trying to find {newMod.Name} in the database...");
                        
                        Mod dbMod = await _mediator.Send(new GetModByNameQuery{ Name = newMod.Name }, stoppingToken);

                        if(dbMod == null)
                        {
                            modsAdded++;
                            _logger.LogInformation($"[ ] Detected that mod {newMod.Name} does not exist, and needs to be added.");
                            _messageQueue.SendMessage("AddMod", newMod.Name);
                        }
                        else if(dbMod.GetLatestRelease().ReleasedAt < newMod.LatestRelease.ReleasedAt)
                        {
                            modsUpdated++;
                            _logger.LogInformation($"[-] Detected that mod {newMod.Name} exists and needs to be updated.");
                            _messageQueue.SendMessage("UpdateMod", newMod.Name);
                        }
                        else
                        {
                            modsUpToDate++;
                            _logger.LogInformation($"[X] Detected that mod {newMod.Name} exists and is up to date.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"An exception occurred while attempting to find the mod {newMod.Name}:");
                        Console.WriteLine(ex.ToString());
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.Source);
                        Console.WriteLine(ex.StackTrace);

                        throw ex;
                    }

                    if(counter >= 9) break;
                    counter++;
                }

                newModList = null;

                _logger.LogInformation($"ModUpdateScheduler completed execution at {_dateTimeService.GetCurrentTime()} with the following results:");
                _logger.LogInformation($"    Mods Added:       {modsAdded.ToString()}");
                _logger.LogInformation($"    Mods Updated:     {modsUpdated.ToString()}");
                _logger.LogInformation($"    Mods Up To Date:  {modsUpToDate.ToString()}");
                _logger.LogInformation($"Sleeping for {Worker.SleepDuration}...\n");

                await Task.Delay(Worker.SleepDuration, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"-----ModUpdateScheduler shutting down at {_dateTimeService.GetCurrentTime()}");
            return base.StopAsync(stoppingToken);
        }
    }
}
