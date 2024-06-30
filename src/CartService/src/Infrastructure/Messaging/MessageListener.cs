using System.Text.Json;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using CartService.Application.CartItems.Commands.UpdateCartItems;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartService.Infrastructure.Messaging;
public class MessageListener : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _serviceBusConnectionString;
    private readonly string _queueName;
    private readonly IMapper _mapper;


    public MessageListener(IConfiguration configuration, IServiceProvider serviceProvider, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _serviceBusConnectionString = configuration.GetConnectionString("ServiceBus")!;
        _queueName = configuration.GetValue<string>("ServiceBus:QueueName")!;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var client = new ServiceBusClient(_serviceBusConnectionString);
        var receiver = client.CreateReceiver(_queueName);

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await receiver.ReceiveMessageAsync(maxWaitTime: TimeSpan.FromSeconds(10), cancellationToken: stoppingToken);
            if (message != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {

                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var messageBody = message.Body.ToString();

                    var updateCartItemsDto = JsonSerializer.Deserialize<UpdateCartItemsDto>(messageBody);

                    Guard.Against.Null(updateCartItemsDto);

                    var updateCommand = _mapper.Map<UpdateCartItemsCommand>(updateCartItemsDto);

                    await mediator.Send(updateCommand, stoppingToken);

                    // Complete the message
                    await receiver.CompleteMessageAsync(message);
                }
            }
        }
    }
}
