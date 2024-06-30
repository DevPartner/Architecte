using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CatalogService.Infrastructure.Data;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace CleanArchitecture.Infrastructure.Messaging;

public class OutboxMessagePublisher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _serviceBusConnectionString;
    private readonly string _queueName;

    public OutboxMessagePublisher(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _serviceBusConnectionString = configuration.GetConnectionString("ServiceBus")!;
        _queueName = configuration.GetValue<string>("ServiceBus:QueueName")!;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await PublishUnprocessedMessages(stoppingToken);
            await Task.Delay(10000, stoppingToken); // Adjust delay as needed
        }
    }

    private async Task PublishUnprocessedMessages(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var unprocessedMessages = await dbContext.OutboxMessages
                .Where(m => !m.Processed)
                .Where(m => m.Type == "ProductUpdated")
                .ToListAsync(cancellationToken);

            await using var client = new ServiceBusClient(_serviceBusConnectionString);
            var sender = client.CreateSender(_queueName);

            foreach (var message in unprocessedMessages)
            {
                var retryPolicy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(5, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                        onRetry: (exception, retryCount, context) =>
                        {
                            // Log the exception or retry information
                            Console.WriteLine($"Retry {retryCount} for message {message.Id} due to {exception.Message}");
                        });

                await retryPolicy.ExecuteAsync(async () =>
                {
                    var serviceBusMessage = new ServiceBusMessage(message.Data)
                    {
                        MessageId = message.Id.ToString()
                    };

                    await sender.SendMessageAsync(serviceBusMessage, cancellationToken);

                    // Mark the message as processed
                    message.Processed = true;
                    dbContext.OutboxMessages.Update(message);
                    await dbContext.SaveChangesAsync(cancellationToken);
                });
            }
        }
    }
}
