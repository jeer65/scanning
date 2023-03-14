using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiAppScanning.Models;

[Table("Registration", Schema = "Storm")]
public class Registration
{
    [Key]
    [Column("RegistrationId")]
    public int Id { get; set; }
    public DateTimeOffset? CheckInTime { get; set; }
    public DateTimeOffset? CheckOutTime { get; set; }
    public bool CheckInAccepted { get; set; }
    public bool DrivingAllowed { get; set; }
    public string DocumentNumber { get; set; }
    public DateTimeOffset DocumentExpiration { get; set; }

    [ForeignKey(nameof(CrewMember))]
    public int CrewMemberId { get; set; }
    public CrewMember CrewMember { get; set; }

    [ForeignKey(nameof(Storm))]
    public int StormId { get; set; }
    public Storm Storm { get; set; }
}
