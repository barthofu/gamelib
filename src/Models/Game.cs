﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Models;

[Index(nameof(Slug), IsUnique = true)]
[Index(nameof(RawgId), IsUnique = true)]
public class Game
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int RawgId { get; set; }

    public required string Slug { get; set; }
    public required string Title { get; set; }
    public required string ReleaseDate { get; set; }
    public required string CoverImage { get; set; }
    public required string Description { get; set; }
    public double Rating { get; set; }
    public bool IsStarred { get; set; }

    // relations
    public virtual ICollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();
    public virtual ICollection<Platform> Platforms { get; set; } = new ObservableCollection<Platform>();
    public virtual ICollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();
}