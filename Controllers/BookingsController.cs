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
        // Validasi apakah Room ada
        var roomExists = await _context.Rooms.AnyAsync(r => r.Id == bookingDto.RoomId);
        if (!roomExists)
        {
            return BadRequest(new { message = "Room not found" });
        }

        // Validasi waktu
        if (bookingDto.EndTime <= bookingDto.StartTime)
        {
            return BadRequest(new { message = "EndTime must be greater than StartTime" });
        }

        // Cek konflik jadwal ruangan
        var hasConflict = await _context.Bookings
            .AnyAsync(b => b.RoomId == bookingDto.RoomId
                && b.Status == "Approved"
                && ((bookingDto.StartTime >= b.StartTime && bookingDto.StartTime < b.EndTime)
                    || (bookingDto.EndTime > b.StartTime && bookingDto.EndTime <= b.EndTime)
                    || (bookingDto.StartTime <= b.StartTime && bookingDto.EndTime >= b.EndTime)));

        if (hasConflict)
        {
            return Conflict(new { message = "Room is already booked for the selected time slot" });
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
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            return NotFound(new { message = $"Booking with ID {id} not found" });
        }

        // Update status
        booking.Status = statusDto.Status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { message = "Error updating booking status" });
        }

        return Ok(new { message = "Booking status updated successfully", booking });
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
            return NotFound(new { message = $"Booking with ID {id} not found" });
        }

        if (booking.DeletedAt != null)
        {
            return BadRequest(new { message = "Booking has already been deleted" });
        }

        // Soft Delete: Set DeletedAt timestamp
        booking.DeletedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { message = "Error deleting booking" });
        }

        return Ok(new { message = "Booking deleted successfully (soft delete)" });
    }
}
