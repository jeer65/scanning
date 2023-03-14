using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppScanning.Models;

[Table("Vendor", Schema = "Storm")]
public class Vendor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //TODO: Put this on others
    public int VendorId { get; set; }
    public string VendorName { get; set; }
}
