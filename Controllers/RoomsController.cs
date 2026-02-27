using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Rooms (All roles)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
    {
        var rooms = await _context.Rooms
            .OrderBy(r => r.RoomCode)
            .ToListAsync();

        return Ok(rooms);
    }

    // GET: api/Rooms/5 (All roles)
    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound(new { message = $"Ruangan dengan ID {id} tidak ditemukan" });
        }

        return Ok(room);
    }

    // POST: api/Rooms (Admin only)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Room>> CreateRoom(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // DELETE: api/Rooms/5 (Admin only, Hard Delete)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound(new { message = $"Ruangan dengan ID {id} tidak ditemukan" });
        }

        // Cek apakah ada booking aktif (Pending/Approved) yang menggunakan ruangan ini
        var hasActiveBookings = await _context.Bookings
            .AnyAsync(b => b.RoomId == id
                && b.DeletedAt == null
                && b.Status != "Rejected");

        if (hasActiveBookings)
        {
            return Conflict(new { message = $"Ruangan {room.RoomName} masih memiliki peminjaman aktif dan tidak dapat dihapus" });
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Ruangan {room.RoomName} berhasil dihapus" });
    }
}
