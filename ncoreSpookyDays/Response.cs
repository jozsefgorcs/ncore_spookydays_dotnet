using Newtonsoft.Json;

class Response
{
    [JsonProperty("eventid")]
    public string EventId { get; set; }
    
    [JsonProperty("userid")]
    public string UserId { get; set; }
}