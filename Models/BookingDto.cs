using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CreateBookingDto
{
    [Required(ErrorMessage = "RoomId is required")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "BorrowerName is required")]
    public string BorrowerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "BorrowerId (NIM) is required")]
    public string BorrowerId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Purpose is required")]
    public string Purpose { get; set; } = string.Empty;

    [Required(ErrorMessage = "StartTime is required")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "EndTime is required")]
    public DateTime EndTime { get; set; }
}

public class UpdateStatusDto
{
    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(Pending|Approved|Rejected)$", ErrorMessage = "Status must be 'Pending', 'Approved', or 'Rejected'")]
    public string Status { get; set; } = string.Empty;
}
