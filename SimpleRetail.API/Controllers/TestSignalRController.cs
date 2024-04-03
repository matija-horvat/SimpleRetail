using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SimpleRetail.API.Hubs;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class TestSignalRController: ControllerBase
{
    private readonly IHubContext<SignalRHub> _hubContext;
    private readonly ILogger<TestSignalRController> _logger;

    public TestSignalRController(IHubContext<SignalRHub> hubContext, ILogger<TestSignalRController> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> SendMessage(string message)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            _logger.LogInformation($"Message sent: {message}");
            return Ok("Message sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending message: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
