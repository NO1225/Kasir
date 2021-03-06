using System;
using System.Threading;
using System.Threading.Tasks;
using Client.WorkerService.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Client.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // You must run Kasir.Api and Client.WorkerService together.
                    Console.WriteLine("Hello World!");
                    string baseUrl = "https://localhost:5021";
                    LoginClient loginClient = new LoginClient(baseUrl);
                    var result = await loginClient.CreateAsync(new GetTokenQuery
                    {
                        Email = "test@test.com",
                        Password = "Matech_1850"
                    }, stoppingToken);

                    if (result.Succeeded)
                    {
                        CountriesClient citiesClient = new CountriesClient(baseUrl);
                        citiesClient.SetBearerToken(result.Data.Token);
                        var res = await citiesClient.GetAllCountriesAsync(stoppingToken);     //consume a webApi get action
                        foreach (var item in res.Data)
                        {
                            Console.WriteLine($"City: { item.Name} ");
            
                        }
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error Message: ", ex);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
