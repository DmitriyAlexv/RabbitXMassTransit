using WebAPI.Configuration;
using WebAPI.Consumer.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasstransitRabbitMq(builder.Configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>()!);

var app = builder.Build();

app.Run();

