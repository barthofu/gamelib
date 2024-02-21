using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamelib.Models;

public class Genre
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    // relations
    public virtual ICollection<Game> Games { get; } = new List<Game>();
}