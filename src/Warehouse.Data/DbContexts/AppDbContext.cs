using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<InputDocument> InputDocuments { get; set; }
    public DbSet<InputResource> InputResources { get; set; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
    public DbSet<Resource> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MeasurementUnit>().HasData(
            new MeasurementUnit() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 1, Name = "тонна" },
            new MeasurementUnit() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 2, Name = "шт" },
            new MeasurementUnit() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 3, Name = "литр" },
            new MeasurementUnit() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 4, Name = "кг" },
            new MeasurementUnit() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 5, Name = "гр", Status = EntityStatus.InActive }
            );

        modelBuilder.Entity<Resource>().HasData(
            new Resource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 1, Name = "Вода" },
            new Resource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 2, Name = "Картошка" },
            new Resource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 3, Name = "Зерно" },
            new Resource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 4, Name = "Nvidia GeForce RTX 5070 Ti" },
            new Resource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 5, Name = "Монитор", Status = EntityStatus.InActive }
            );

        modelBuilder.Entity<InputDocument>().HasData(
            new InputDocument() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 1, Number = "#001", Date = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc) },
            new InputDocument() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 2, Number = "#002", Date = new DateTime(2025, 8, 7, 0, 0, 0, DateTimeKind.Utc) },
            new InputDocument() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 3, Number = "#003", Date = new DateTime(2025, 8, 6, 0, 0, 0, DateTimeKind.Utc) },
            new InputDocument() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 4, Number = "#004", Date = new DateTime(2025, 8, 5, 0, 0, 0, DateTimeKind.Utc) },
            new InputDocument() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 5, Number = "#005", Date = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc) }
            );

        modelBuilder.Entity<InputResource>().HasData(
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 1, InputDocumentId = 2, ResourceId = 5, MeasurementUnitId = 2, Amount = 10 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 2, InputDocumentId = 1, ResourceId = 1, MeasurementUnitId = 3, Amount = 500 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 3, InputDocumentId = 1, ResourceId = 2, MeasurementUnitId = 4, Amount = 800 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 4, InputDocumentId = 1, ResourceId = 3, MeasurementUnitId = 1, Amount = 5 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 5, InputDocumentId = 3, ResourceId = 4, MeasurementUnitId = 2, Amount = 7 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 6, InputDocumentId = 4, ResourceId = 1, MeasurementUnitId = 3, Amount = 250 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 7, InputDocumentId = 5, ResourceId = 4, MeasurementUnitId = 2, Amount = 15 },
            new InputResource() { CreatedAt = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc), Id = 8, InputDocumentId = 5, ResourceId = 5, MeasurementUnitId = 2, Amount = 15 }
            );
    }
}