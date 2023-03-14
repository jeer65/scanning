using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppScanning.Models;

[Table("Storm", Schema = "Storm")]
public class Storm
{
    [Key]
    public int StormId { get; set; }

    [Column("StormName")]
    public string Name { get; set; }

    [Column("StormDateStart")]
    public DateTimeOffset DateStart { get; set; }

    [Column("StormDateEnd")]
    public DateTimeOffset DateEnd { get; set; }
}
