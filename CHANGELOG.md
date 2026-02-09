# Changelog

All notable changes to the RoomHub Backend project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-02-10

### Added

#### Database & Models
- Initial database schema with Entity Framework Core and PostgreSQL integration
- `Room` entity model with properties: Id, RoomCode, RoomName, Capacity, Location
- `Booking` entity model with properties: Id, RoomId, BorrowerName, BorrowerId, Purpose, StartTime, EndTime, Status, DeletedAt
- Soft delete implementation on Booking entity using `DeletedAt` timestamp
- Global query filter to automatically exclude soft-deleted records
- Database seeder with 30 predefined rooms across campus buildings (labs, classrooms, auditoriums, meeting rooms, studios)
- Foreign key relationship between Booking and Room with RESTRICT delete behavior

#### API Endpoints
- **GET /api/Bookings** - Retrieve all bookings with room details (includes Room data via `.Include()`)
- **GET /api/Bookings/{id}** - Retrieve specific booking by ID with room details
- **POST /api/Bookings** - Create new booking with comprehensive validation
- **PATCH /api/Bookings/{id}/status** - Update booking status (Approved/Rejected)
- **DELETE /api/Bookings/{id}** - Soft delete booking

#### Data Transfer Objects (DTOs)
- `CreateBookingDto` with required field validations:
  - RoomId (required)
  - BorrowerName (required, max 100 characters)
  - BorrowerId/NIM (required, max 20 characters)
  - Purpose (required)
  - StartTime (required)
  - EndTime (required)
- `UpdateStatusDto` with regex validation for status field
  - Status must be "Approved" or "Rejected"
  - Optional AdminNote field for admin comments
- All validation messages in Indonesian language

#### Business Logic & Validations
- **Room existence validation** - Verify room exists before creating booking
- **Time range validation** - Ensure EndTime is greater than StartTime
- **Past date validation** - Prevent bookings with StartTime in the past
- **Conflict detection** - Check for overlapping bookings on the same room
  - Excludes "Rejected" bookings from conflict check
  - Excludes soft-deleted bookings from conflict check
  - Returns 409 Conflict with descriptive message when schedule overlaps
- Status update validation to prevent changes on soft-deleted bookings

#### Configuration & Security
- Environment variable configuration using DotNetEnv package
- `.env` file support for database credentials (DB_HOST, DB_NAME, DB_USER, DB_PASSWORD)
- Connection string builder from environment variables
- `.gitignore` configuration for .NET projects including .env file
- Secure database credential management

#### Infrastructure
- ASP.NET Core 10.0 project setup with minimal API template
- PostgreSQL database provider (Npgsql.EntityFrameworkCore.PostgreSQL v10.0)
- Entity Framework Core Design tools (v10.0.2) for migrations
- Database migrations: `InitialCreate` and `SeedRoomData`
- AppDbContext configuration with DbSet for Rooms and Bookings
- Controller-based API with attribute routing

#### Documentation
- Comprehensive README.md with:
  - Project description and features
  - Technology stack documentation
  - Installation guide with prerequisites
  - Environment variables configuration guide
  - API endpoints documentation with examples
  - Database schema overview
  - Security features list
  - Development and production run instructions
- CHANGELOG.md with version tracking

### Changed
- Removed default WeatherForecast sample endpoint
- Updated Program.cs to use Controllers instead of minimal API
- Enhanced error messages from English to Indonesian for better local UX

### Technical Details

#### Dependencies
```xml
- Microsoft.AspNetCore.OpenApi (10.0.x)
- Microsoft.EntityFrameworkCore.Design (10.0.2)
- Npgsql.EntityFrameworkCore.PostgreSQL (10.0.0)
- DotNetEnv (3.1.1)
```

#### Database Seeds
30 rooms seeded across campus:
- 5 Computer Labs (LK-101 to LK-201, LP-101, LJ-101)
- 13 Classrooms (RK-101 to RK-302)
- 4 Meeting Rooms (RM-101 to RM-103)
- 2 Auditoriums (AUD-01: 300 capacity, AUD-02: 150 capacity)
- 4 Other Labs (Language, Physics, Chemistry)
- 2 Studios (Multimedia, Recording)
- 2 Seminar Rooms (RS-101, RS-102)
- 1 Design Lab (LD-101)

#### Commit History
- `feat: initial setup with .NET Core Web API project`
- `feat: implement database models and EF Core configuration`
- `feat: add seed data for 30 rooms across campus buildings`
- `feat(booking): implement CRUD controller with soft delete`
- `feat(booking): add conflict validation and date constraints`

---

## [Unreleased]

### Planned Features
- User authentication and authorization
- Role-based access control (Admin/User)
- Booking approval workflow
- Email notifications
- Booking history and reports
- Room availability calendar view
- Booking cancellation by users
- Unit and integration tests

---

**For detailed changes, see the git commit history.**