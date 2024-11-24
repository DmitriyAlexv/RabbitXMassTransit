using System.Text.Json;

namespace WebAPI.Contract;

public class PostContract
{
    public string? Message { get; set; }
}

public static class PostContractMessagePattern
{
    public static string GetPostContractJson(PostContract postContract)
    {
        var messageObject = new
        {
            destinationAddress = "rabbitmq://localhost/Post",
            headers = new { },
            message = new { Message = postContract.Message },
            messageType = new[] { "urn:message:WebAPI.Contract:PostContract" }
        };
        return JsonSerializer.Serialize(messageObject);
    }
}