using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class RegisterDto
{
    [Required(ErrorMessage = "Username wajib diisi")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username harus antara 3-50 karakter")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nama lengkap wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama lengkap maksimal 100 karakter")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "NIM/NIP maksimal 20 karakter")]
    public string? StudentId { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password minimal 6 karakter")]
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    [Required(ErrorMessage = "Username wajib diisi")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password wajib diisi")]
    public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? StudentId { get; set; }
    public string Role { get; set; } = string.Empty;
}
