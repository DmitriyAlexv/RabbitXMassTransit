using MassTransit;
using WebAPI.Contract;

namespace WebAPI.Consumer.Consumer;

public class PostConsumer: IConsumer<PostContract>
{
    public async Task Consume(ConsumeContext<PostContract> context)
    {
        Console.WriteLine(context.Message.Message);
    }
}