using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Room
{
    public int Id { get; set; }

    [Required]
    public string RoomCode { get; set; } = string.Empty;

    [Required]
    public string RoomName { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string? Location { get; set; }
}
