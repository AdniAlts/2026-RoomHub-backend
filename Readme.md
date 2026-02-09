# 🏢 RoomHub Backend - Sistem Peminjaman Ruangan Kampus

API Backend untuk sistem peminjaman ruangan kampus yang dibangun menggunakan ASP.NET Core dan PostgreSQL. Sistem ini mendukung manajemen ruangan, peminjaman dengan validasi konflik jadwal, dan soft delete untuk keamanan data.

## 📋 Deskripsi

RoomHub Backend adalah RESTful API yang menyediakan fungsionalitas lengkap untuk mengelola peminjaman ruangan di lingkungan kampus. Sistem ini dilengkapi dengan:

- **Manajemen Ruangan**: Database 30 ruangan kampus dengan berbagai tipe (lab, kelas, auditorium, dll)
- **CRUD Peminjaman**: Create, Read, Update status, dan Soft Delete booking
- **Validasi Ketat**: Validasi waktu, konflik jadwal, dan format data
- **Soft Delete**: Data tidak dihapus permanen untuk keperluan audit
- **Bahasa Indonesia**: Pesan error dan response dalam bahasa Indonesia

## 🚀 Teknologi

- **Framework**: ASP.NET Core 10.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 10.0
- **Database Provider**: Npgsql.EntityFrameworkCore.PostgreSQL 10.0
- **Environment Management**: DotNetEnv 3.1.1
- **Language**: C# .NET 10

## 📦 Instalasi

### Prerequisites

- .NET SDK 10.0.102 atau lebih tinggi
- PostgreSQL 12 atau lebih tinggi
- Git

### Langkah Instalasi

1. **Clone Repository**
   ```bash
   git clone <repository-url>
   cd 2026-RoomHub-backend
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Setup Database**
   
   Pastikan PostgreSQL sudah terinstall dan berjalan di sistem Anda.

4. **Konfigurasi Environment Variables**
   
   Buat file `.env` di root project:
   ```bash
   cp .env.example .env
   ```
   
   Edit file `.env` dengan kredensial database Anda:
   ```env
   # Database Configuration
   DB_HOST=localhost
   DB_NAME=peminjaman_ruangan
   DB_USER=postgres
   DB_PASSWORD=your_password_here
   ```

5. **Jalankan Migrasi Database**
   ```bash
   # Install EF Core tools (jika belum)
   dotnet tool install --global dotnet-ef --version 10.0.2

   # Jalankan migrasi
   dotnet ef database update
   ```

6. **Build Project**
   ```bash
   dotnet build
   ```

## 🔧 Environment Variables

Konfigurasi berikut diperlukan dalam file `.env`:

| Variable | Deskripsi | Contoh |
|----------|-----------|--------|
| `DB_HOST` | Host database PostgreSQL | `localhost` |
| `DB_NAME` | Nama database | `peminjaman_ruangan` |
| `DB_USER` | Username PostgreSQL | `postgres` |
| `DB_PASSWORD` | Password PostgreSQL | `your_password` |

⚠️ **Penting**: File `.env` sudah tercantum di `.gitignore` untuk keamanan. Jangan pernah commit file ini ke repository!

## ▶️ Menjalankan Aplikasi

### Development Mode

```bash
dotnet run
```

Aplikasi akan berjalan di:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### Production Mode

```bash
dotnet run --configuration Release
```

## 📚 API Endpoints

### Bookings

| Method | Endpoint | Deskripsi |
|--------|----------|-----------|
| GET | `/api/Bookings` | Daftar semua peminjaman |
| GET | `/api/Bookings/{id}` | Detail peminjaman spesifik |
| POST | `/api/Bookings` | Buat peminjaman baru |
| PATCH | `/api/Bookings/{id}/status` | Update status peminjaman |
| DELETE | `/api/Bookings/{id}` | Hapus peminjaman (soft delete) |

### Contoh Request

**Create Booking:**
```bash
POST /api/Bookings
Content-Type: application/json

{
  "roomId": 1,
  "borrowerName": "John Doe",
  "borrowerId": "12345678",
  "purpose": "Rapat Organisasi",
  "startTime": "2026-02-15T10:00:00",
  "endTime": "2026-02-15T12:00:00"
}
```

**Update Status:**
```bash
PATCH /api/Bookings/1/status
Content-Type: application/json

{
  "status": "Approved",
  "adminNote": "Disetujui untuk acara resmi"
}
```

## 🗄️ Database Schema

### Rooms
- Id (PK)
- RoomCode (unique)
- RoomName
- Capacity
- Location

### Bookings
- Id (PK)
- RoomId (FK)
- BorrowerName
- BorrowerId (NIM)
- Purpose
- StartTime
- EndTime
- Status (Pending/Approved/Rejected)
- DeletedAt (Soft Delete)

## 🔒 Fitur Keamanan

- ✅ Environment variables untuk credentials
- ✅ Input validation dengan Data Annotations
- ✅ Soft delete untuk audit trail
- ✅ SQL injection protection (EF Core parameterized queries)
- ✅ HTTPS redirect

## 🧪 Testing

Jalankan tests (jika sudah diimplementasikan):
```bash
dotnet test
```

## 📝 Validasi Bisnis

1. **Waktu Peminjaman**: EndTime harus lebih besar dari StartTime
2. **Waktu Masa Lalu**: StartTime tidak boleh di masa lalu
3. **Konflik Jadwal**: Sistem mencegah double booking pada ruangan yang sama
4. **Status Valid**: Status hanya boleh "Approved" atau "Rejected"
5. **Format Data**: Validasi panjang string dan format data

## 🤝 Contributing

1. Fork repository
2. Buat branch fitur (`git checkout -b feature/AmazingFeature`)
3. Commit perubahan (`git commit -m 'feat: add some AmazingFeature'`)
4. Push ke branch (`git push origin feature/AmazingFeature`)
5. Buat Pull Request

## 📄 License

Distributed under the MIT License.

## 👥 Author

RoomHub Development Team - 2026

## 📞 Support

Jika mengalami masalah, silakan buat issue di repository ini.

---

**Version**: 1.0.0  
**Last Updated**: February 10, 2026