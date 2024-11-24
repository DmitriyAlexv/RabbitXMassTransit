using MassTransit;
using RabbitMQ.Client;
using WebAPI.Configuration;

namespace WebAPI.Extension;

public static class AddMasstransitExt
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services, RabbitMqConfig config)
    {
        return services.AddSingleton<Task<IConnection>>(provider =>
        {
            var factory = new ConnectionFactory() { HostName = config.Hostname, UserName = config.Username, Password = config.Password };
            return factory.CreateConnectionAsync();
        });
    }
}