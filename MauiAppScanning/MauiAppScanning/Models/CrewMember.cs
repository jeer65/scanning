using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MauiAppScanning.Models;

[Table("CrewMember", Schema = "Storm")]
public class CrewMember
{
    [Key]
    [Column("CrewMemberId")]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string StateCode { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Gender { get; set; }
    public bool NoFlyList { get; set; }

    [ForeignKey(nameof(Vendor))]
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; }
   
    public List<Registration> Registrations { get; set; }
}
