class ReceivedEvent
{
    public string EventId { get; set; }
    public string Type { get; set; }
    public bool Recaptcha { get; set; }
    public bool Success { get; set; }
    public string Text { get; set; }
}