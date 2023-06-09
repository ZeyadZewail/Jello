namespace JelloBackend.Models;

public class SignalCommand
{
    public string commandName { get; set; }
    
    public object payload { get; set; } 
}