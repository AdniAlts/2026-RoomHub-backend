using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Bookings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Room)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();

        return Ok(bookings);
    }

    // GET: api/Bookings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBooking(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound(new { message = $"Booking with ID {id} not found" });
        }

        return Ok(booking);
    }

    // POST: api/Bookings
    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDto bookingDto)
    {
        // VALIDASI 1: Cek apakah Ruangan ada di database
        var room = await _context.Rooms.FindAsync(bookingDto.RoomId);
        if (room == null)
        {
            return BadRequest(new { message = "Ruangan tidak ditemukan" });
        }

        // VALIDASI 2: Pastikan EndTime lebih besar dari StartTime
        if (bookingDto.EndTime <= bookingDto.StartTime)
        {
            return BadRequest(new { message = "Waktu selesai harus lebih besar dari waktu mulai" });
        }

        // VALIDASI 3: Pastikan StartTime tidak di masa lalu
        if (bookingDto.StartTime < DateTime.Now)
        {
            return BadRequest(new { message = "Waktu mulai tidak boleh di masa lalu" });
        }

        // VALIDASI 4: Cek konflik jadwal ruangan (exclude yang Rejected dan soft deleted)
        var hasConflict = await _context.Bookings
            .Where(b => b.RoomId == bookingDto.RoomId
                && b.Status != "Rejected"
                && b.DeletedAt == null
                && ((bookingDto.StartTime >= b.StartTime && bookingDto.StartTime < b.EndTime)
                    || (bookingDto.EndTime > b.StartTime && bookingDto.EndTime <= b.EndTime)
                    || (bookingDto.StartTime <= b.StartTime && bookingDto.EndTime >= b.EndTime)))
            .AnyAsync();

        if (hasConflict)
        {
            return Conflict(new { message = $"Ruangan {room.RoomName} sudah dibooking pada rentang waktu tersebut" });
        }

        var booking = new Booking
        {
            RoomId = bookingDto.RoomId,
            BorrowerName = bookingDto.BorrowerName,
            BorrowerId = bookingDto.BorrowerId,
            Purpose = bookingDto.Purpose,
            StartTime = bookingDto.StartTime,
            EndTime = bookingDto.EndTime,
            Status = "Pending" // Default status
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // Load Room data untuk response
        await _context.Entry(booking).Reference(b => b.Room).LoadAsync();

        return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
    }

    // PATCH: api/Bookings/5/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateBookingStatus(int id, UpdateStatusDto statusDto)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound(new { message = $"Peminjaman dengan ID {id} tidak ditemukan" });
        }

        // Validasi: Tidak bisa mengubah status booking yang sudah soft deleted
        if (booking.DeletedAt != null)
        {
            return BadRequest(new { message = "Tidak dapat mengubah status peminjaman yang sudah dihapus" });
        }

        // Update status dan catatan admin (jika ada)
        var oldStatus = booking.Status;
        booking.Status = statusDto.Status;

        try
        {
            await _context.SaveChangesAsync();

            return Ok(new 
            { 
                message = $"Status peminjaman berhasil diubah dari {oldStatus} menjadi {booking.Status}",
                adminNote = statusDto.AdminNote,
                booking 
            });
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { message = "Terjadi kesalahan saat mengubah status peminjaman" });
        }
    }

    // DELETE: api/Bookings/5 (Soft Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        // Gunakan IgnoreQueryFilters untuk mengambil data termasuk yang sudah soft deleted
        var booking = await _context.Bookings
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound(new { message = $"Peminjaman dengan ID {id} tidak ditemukan" });
        }

        if (booking.DeletedAt != null)
        {
            return BadRequest(new { message = "Peminjaman ini sudah dihapus sebelumnya" });
        }

        // Soft Delete: Set DeletedAt timestamp
        booking.DeletedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { message = "Terjadi kesalahan saat menghapus peminjaman" });
        }

        return Ok(new 
        { 
            message = "Peminjaman berhasil dihapus (soft delete)",
            deletedAt = booking.DeletedAt
        });
    }
}
