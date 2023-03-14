using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppScanning.Models;

[Table("VendorRoster", Schema = "Storm")]
public class VendorRoster
{
    [Key]
    [Column("RosterId")]
    public int Id { get; set; }

    [ForeignKey(nameof(Vendor))]
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; }

    public int StormId { get; set; } //TODO: Foreign Key in db schema
}