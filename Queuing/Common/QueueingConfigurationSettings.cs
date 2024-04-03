namespace Queuing.Implementation;

public class QueueingConfigurationSettings
{
    public string Hostname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? Port { get; set; }
    public int? ConsumerConcurrency { get; set; }
}