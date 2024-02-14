using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamelib.Models;

public class Game
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Title { get; set; }
    public required int ReleaseYear { get; set; }
    public required string CoverImage { get; set; }
    public required string Description { get; set; }
    public int Rating { get; set; }
    public bool IsStarred { get; set; }

    public int GenreId { get; set; }
    [ForeignKey("GenreId")]
    public virtual Genre Genre { get; set; }

    public int PlatformId { get; set; }
    [ForeignKey("PlatformId")]
    public virtual Platform Platform { get; set; }

    public virtual ObservableCollection<Tag> Tags { get; } = new();
}