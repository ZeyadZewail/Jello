namespace JelloBackend.Models;

public record Column
{
    public int id { get; set; }

    public string name { get; set; }

    public ICollection<Element> Elements { get; set; } = null!;
};