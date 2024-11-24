using WebAPI.Configuration;
using MassTransit;
using WebAPI.Consumer.Consumer;
using WebAPI.Contract;

namespace WebAPI.Consumer.Extension;

public static class AddMasstransitExt
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services, RabbitMqConfig config)
    {
        return services.AddMassTransit(bcfg =>
        {
            bcfg.UsingRabbitMq((ctx, fcfg) =>
            {
                fcfg.Host(
                    config.Hostname,
                    5672, 
                    "/", 
                    cfg =>
                    {
                        cfg.Username(config.Username);
                        cfg.Password(config.Password);
                    });
                fcfg.ReceiveEndpoint("post_queue", configurator =>
                {
                   configurator.Consumer<PostConsumer>();
                });
            });
        });
    }
}