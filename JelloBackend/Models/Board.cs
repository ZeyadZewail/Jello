namespace JelloBackend.Models;

public class Board
{
    public int id { get; set; }

    public string name { get; set; }

    public ICollection<Column> columns { get; set; } = null!;
}