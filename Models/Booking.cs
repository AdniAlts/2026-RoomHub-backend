using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required]
    public string BorrowerName { get; set; } = string.Empty;

    public string? BorrowerId { get; set; }

    public string? Purpose { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Status { get; set; } = "Pending";

    // Soft Delete Implementation
    public DateTime? DeletedAt { get; set; }

    // Navigation Property
    public Room? Room { get; set; }
}
