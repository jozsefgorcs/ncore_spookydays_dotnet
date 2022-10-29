using System;
using System.Threading;
using Newtonsoft.Json;
using Websocket.Client;

const string UserId = "";
try
{
    var exitEvent = new ManualResetEvent(false);
    var url = new Uri(
        "wss://spooky.ncore.pro:3001/spooky");

    using (var client = new WebsocketClient(url))
    {
        client.ReconnectTimeout = TimeSpan.FromSeconds(30);
        client.ReconnectionHappened.Subscribe(info =>
        {
            Console.WriteLine("Reconnection happened, type: " + info.Type);
        });
        client.MessageReceived.Subscribe(msg =>
        {
            Console.WriteLine($"Message arrived: {msg}");
            var receivedEvent = JsonConvert.DeserializeObject<ReceivedEvent>(msg.Text);
            
            switch (receivedEvent.Type) {
                case "spooky":
                    if (receivedEvent.Recaptcha) {
                        Console.WriteLine("recaptcha");
                        return;
                    }
                 

                    var response = new Response{
                        EventId =  receivedEvent.EventId,
                        UserId = UserId
                    };

                    string json = JsonConvert.SerializeObject(response, Formatting.Indented);
                    client.Send(json);
                    break;

                case "spooky-event":
                    Console.WriteLine($"Success? {receivedEvent.Success}");
                    break;
            }
        });
        client.Start();
        exitEvent.WaitOne();
    }
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.ToString());
}