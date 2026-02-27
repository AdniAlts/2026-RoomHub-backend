using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Global Query Filter untuk Soft Delete
        modelBuilder.Entity<Booking>()
            .HasQueryFilter(b => b.DeletedAt == null);

        // Konfigurasi relasi Room -> Booking
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany()
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfigurasi relasi User -> Booking
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Unique index on Username
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Seed Data untuk Users
        // admin123 and mahasiswa123 hashes (BCrypt, pre-computed)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                FullName = "Administrator",
                StudentId = null,
                PasswordHash = "$2a$11$QpGo3uStqRjoAQHuBd0oc.WT8IOxjBz1aUTtSHzPMnYmeh43mwJKS", // admin123
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "mahasiswa",
                FullName = "Mahasiswa Demo",
                StudentId = "2026001",
                PasswordHash = "$2a$11$QpGo3uStqRjoAQHuBd0oc.mQp5PkfBq9E7MwqvCIVXfpEKvZgjkUe", // mahasiswa123
                Role = "Mahasiswa"
            }
        );

        // Seed Data untuk Rooms
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomCode = "LK-101", RoomName = "Lab Komputer 1", Capacity = 40, Location = "Gedung A Lantai 1" },
            new Room { Id = 2, RoomCode = "LK-102", RoomName = "Lab Komputer 2", Capacity = 40, Location = "Gedung A Lantai 1" },
            new Room { Id = 3, RoomCode = "LK-201", RoomName = "Lab Komputer 3", Capacity = 35, Location = "Gedung A Lantai 2" },
            new Room { Id = 4, RoomCode = "RK-101", RoomName = "Ruang Kelas 101", Capacity = 50, Location = "Gedung B Lantai 1" },
            new Room { Id = 5, RoomCode = "RK-102", RoomName = "Ruang Kelas 102", Capacity = 50, Location = "Gedung B Lantai 1" },
            new Room { Id = 6, RoomCode = "RK-103", RoomName = "Ruang Kelas 103", Capacity = 45, Location = "Gedung B Lantai 1" },
            new Room { Id = 7, RoomCode = "RK-201", RoomName = "Ruang Kelas 201", Capacity = 50, Location = "Gedung B Lantai 2" },
            new Room { Id = 8, RoomCode = "RK-202", RoomName = "Ruang Kelas 202", Capacity = 50, Location = "Gedung B Lantai 2" },
            new Room { Id = 9, RoomCode = "RK-203", RoomName = "Ruang Kelas 203", Capacity = 45, Location = "Gedung B Lantai 2" },
            new Room { Id = 10, RoomCode = "RK-301", RoomName = "Ruang Kelas 301", Capacity = 40, Location = "Gedung B Lantai 3" },
            new Room { Id = 11, RoomCode = "RM-101", RoomName = "Ruang Meeting 1", Capacity = 20, Location = "Gedung C Lantai 1" },
            new Room { Id = 12, RoomCode = "RM-102", RoomName = "Ruang Meeting 2", Capacity = 15, Location = "Gedung C Lantai 1" },
            new Room { Id = 13, RoomCode = "RM-201", RoomName = "Ruang Meeting 3", Capacity = 25, Location = "Gedung C Lantai 2" },
            new Room { Id = 14, RoomCode = "AUD-01", RoomName = "Auditorium Utama", Capacity = 300, Location = "Gedung D" },
            new Room { Id = 15, RoomCode = "AUD-02", RoomName = "Auditorium Mini", Capacity = 150, Location = "Gedung D" },
            new Room { Id = 16, RoomCode = "LB-101", RoomName = "Lab Bahasa", Capacity = 30, Location = "Gedung E Lantai 1" },
            new Room { Id = 17, RoomCode = "LF-101", RoomName = "Lab Fisika 1", Capacity = 30, Location = "Gedung F Lantai 1" },
            new Room { Id = 18, RoomCode = "LF-102", RoomName = "Lab Fisika 2", Capacity = 30, Location = "Gedung F Lantai 1" },
            new Room { Id = 19, RoomCode = "LK-103", RoomName = "Lab Kimia", Capacity = 25, Location = "Gedung F Lantai 2" },
            new Room { Id = 20, RoomCode = "ST-101", RoomName = "Studio Multimedia", Capacity = 20, Location = "Gedung G Lantai 1" },
            new Room { Id = 21, RoomCode = "ST-102", RoomName = "Studio Recording", Capacity = 15, Location = "Gedung G Lantai 1" },
            new Room { Id = 22, RoomCode = "RK-104", RoomName = "Ruang Kelas 104", Capacity = 48, Location = "Gedung B Lantai 1" },
            new Room { Id = 23, RoomCode = "RK-204", RoomName = "Ruang Kelas 204", Capacity = 48, Location = "Gedung B Lantai 2" },
            new Room { Id = 24, RoomCode = "RK-302", RoomName = "Ruang Kelas 302", Capacity = 40, Location = "Gedung B Lantai 3" },
            new Room { Id = 25, RoomCode = "LP-101", RoomName = "Lab Pemrograman", Capacity = 35, Location = "Gedung A Lantai 2" },
            new Room { Id = 26, RoomCode = "LJ-101", RoomName = "Lab Jaringan", Capacity = 30, Location = "Gedung A Lantai 3" },
            new Room { Id = 27, RoomCode = "RS-101", RoomName = "Ruang Seminar 1", Capacity = 100, Location = "Gedung H Lantai 1" },
            new Room { Id = 28, RoomCode = "RS-102", RoomName = "Ruang Seminar 2", Capacity = 80, Location = "Gedung H Lantai 2" },
            new Room { Id = 29, RoomCode = "RM-103", RoomName = "Ruang Meeting 4", Capacity = 12, Location = "Gedung C Lantai 2" },
            new Room { Id = 30, RoomCode = "LD-101", RoomName = "Lab Desain Grafis", Capacity = 25, Location = "Gedung G Lantai 2" }
        );
    }
}

