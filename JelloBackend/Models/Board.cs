using System.ComponentModel.DataAnnotations;

namespace JelloBackend.Models;

public class Board
{
    public int Id { get; set; }
    
    public string title { get; set; }

    public ICollection<Column> Columns { get; set; } = null!;
};