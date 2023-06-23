namespace JelloBackend.Models;

public record Column
{
    public int id { get; set; }

    public string name { get; set; }

    public ICollection<Element> elements { get; set; } = null!;
}