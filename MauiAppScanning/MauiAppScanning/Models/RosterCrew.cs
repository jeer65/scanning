using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppScanning.Models;

[Table("RosterCrew", Schema = "Storm")]
public class RosterCrew
{
    [Key]
    public int RosterCrewId { get; set; }

    [ForeignKey(nameof(VendorRoster))]
    public int RosterId { get; set; }
    public VendorRoster VendorRoster { get; set; }

    [ForeignKey(nameof(CrewMember))]
    public int CrewMemberId { get; set; }
    public CrewMember CrewMember { get; set; }
}
