using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Models;

[Index(nameof(Name), IsUnique = true)]
public class Setting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required SettingNameEnum Name { get; set; }
    public required string Value { get; set; }
}