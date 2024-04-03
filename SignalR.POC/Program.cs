var httpClient = new HttpClient();
var message = "Hello from SignalR!";
var response = await httpClient.GetAsync($"https://localhost:7063/api/TestSignalR?message={message}");
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Message sent successfully!");
}
else
{
    Console.WriteLine($"Failed to send message. Status code: {response.StatusCode}");
}
