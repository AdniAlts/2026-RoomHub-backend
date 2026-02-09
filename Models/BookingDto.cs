using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CreateBookingDto
{
    [Required(ErrorMessage = "ID Ruangan wajib diisi")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Nama peminjam wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama peminjam maksimal 100 karakter")]
    public string BorrowerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "NIM wajib diisi")]
    [StringLength(20, ErrorMessage = "NIM maksimal 20 karakter")]
    public string BorrowerId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tujuan peminjaman wajib diisi")]
    public string Purpose { get; set; } = string.Empty;

    [Required(ErrorMessage = "Waktu mulai wajib diisi")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Waktu selesai wajib diisi")]
    public DateTime EndTime { get; set; }
}

public class UpdateStatusDto
{
    [Required(ErrorMessage = "Status wajib diisi")]
    [RegularExpression("^(Approved|Rejected)$", ErrorMessage = "Status hanya boleh 'Approved' atau 'Rejected'")]
    public string Status { get; set; } = string.Empty;

    public string? AdminNote { get; set; }
}
