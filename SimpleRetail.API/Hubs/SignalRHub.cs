using Microsoft.AspNetCore.SignalR;

namespace SimpleRetail.API.Hubs;


/// <summary>
/// This class is only intended to demonstrate SingnalR and create a POC message. 
/// For real use BE needs to be connected with FE part.
/// </summary>
public class SignalRHub: Hub
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
