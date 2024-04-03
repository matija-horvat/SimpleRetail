using Microsoft.AspNetCore.SignalR.Client;


namespace SimpleRetail.API.Hubs;

public class SignalRClient
{
    private readonly HubConnection _connection;

    public SignalRClient(string hubUrl)
    {
        _connection = new HubConnectionBuilder()
                            .WithUrl(hubUrl)
                            .Build();
    }

    public async Task StartAsync(string hubUrl)
    {
        await _connection.StartAsync();
    }

    public async Task SendMessage(string message)
    {
        await _connection.InvokeAsync("SendMessage", message);
    }
}
