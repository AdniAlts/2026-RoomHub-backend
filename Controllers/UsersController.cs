using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Users (exclude Admin users)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
        var users = await _context.Users
            .Where(u => u.Role != "Admin")
            .OrderBy(u => u.FullName)
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                StudentId = u.StudentId,
                Role = u.Role
            })
            .ToListAsync();

        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User dengan ID {id} tidak ditemukan" });
        }

        if (user.Role == "Admin")
        {
            return NotFound(new { message = $"User dengan ID {id} tidak ditemukan" });
        }

        return Ok(new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            StudentId = user.StudentId,
            Role = user.Role
        });
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto dto)
    {
        // Cek username unik
        var exists = await _context.Users.AnyAsync(u => u.Username == dto.Username);
        if (exists)
        {
            return BadRequest(new { message = "Username sudah digunakan" });
        }

        var user = new User
        {
            Username = dto.Username,
            FullName = dto.FullName,
            StudentId = dto.StudentId,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Mahasiswa"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            StudentId = user.StudentId,
            Role = user.Role
        };

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User dengan ID {id} tidak ditemukan" });
        }

        if (user.Role == "Admin")
        {
            return BadRequest(new { message = "Tidak dapat mengedit user Admin" });
        }

        user.FullName = dto.FullName;
        user.StudentId = dto.StudentId;

        // Update password hanya jika diisi
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { message = "Terjadi kesalahan saat mengupdate user" });
        }

        return Ok(new
        {
            message = $"User {user.FullName} berhasil diupdate",
            user = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                StudentId = user.StudentId,
                Role = user.Role
            }
        });
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User dengan ID {id} tidak ditemukan" });
        }

        if (user.Role == "Admin")
        {
            return BadRequest(new { message = "Tidak dapat menghapus user Admin" });
        }

        // Cek apakah user punya booking aktif
        var hasActiveBookings = await _context.Bookings
            .AnyAsync(b => b.UserId == id
                && b.DeletedAt == null
                && b.Status != "Rejected");

        if (hasActiveBookings)
        {
            return Conflict(new { message = $"User {user.FullName} masih memiliki peminjaman aktif dan tidak dapat dihapus" });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"User {user.FullName} berhasil dihapus" });
    }
}
