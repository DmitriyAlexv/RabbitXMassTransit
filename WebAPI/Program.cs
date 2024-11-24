using System.Text;
using System.Text.Json;
using MassTransit;
using MassTransit.RabbitMqTransport;
using RabbitMQ.Client;
using WebAPI.Configuration;
using WebAPI.Contract;
using WebAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddMasstransitRabbitMq(
    builder.Configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>()!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/post", async (Task<IConnection> _connection) =>
{
    var connection = await _connection;

    await using var channel = await connection.CreateChannelAsync();
    await channel.QueueDeclareAsync(queue: "post_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

    await channel.ExchangeDeclareAsync(exchange: "post_exchange", durable: true, type:"fanout", autoDelete: false, arguments: null);

    await channel.QueueBindAsync(queue: "post_queue", exchange: "post_exchange", routingKey: "", arguments: null);

    var body = Encoding.UTF8.GetBytes(PostContractMessagePattern.GetPostContractJson(new PostContract(){Message = "Hello world"}));

    await channel.BasicPublishAsync(exchange: "post_exchange", routingKey: "", mandatory: false, body: body);
});

app.Run();