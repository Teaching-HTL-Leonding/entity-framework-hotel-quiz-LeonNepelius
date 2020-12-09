using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

var factory = new HotelContextFactory();
var context = factory.CreateDbContext();


switch (args[0])
{
    case "add":
        await AddHotels();
        break;
    case "query":
        await QueryHotel();
        break;
}

async Task QueryHotel()
{
    using (System.IO.StreamWriter file = new System.IO.StreamWriter("hotels.md", false, Encoding.UTF8))
    {
        foreach (var hotel in await context.Hotels.Include(x => x.RoomTypes).ThenInclude(x => x.Price).Include(x => x.Specials).ToArrayAsync())
        {
            await file.WriteLineAsync($"# {hotel.Name}");
            await file.WriteLineAsync();
            await file.WriteLineAsync("## Location");
            await file.WriteLineAsync();
            await file.WriteLineAsync($"{hotel.Address}");
            await file.WriteLineAsync();
            await file.WriteLineAsync("## Specials");
            foreach (var special in hotel.Specials)
            {
                await file.WriteLineAsync($"* {special.Special}");
            }
            await file.WriteLineAsync();
            await file.WriteLineAsync("## Room Types");
            await file.WriteLineAsync();
            await file.WriteLineAsync($"| {"Room Type",-20} | {"Size",6} | {"Price Valid From",-16} | {"Price Valid To",-14} | {"Price in €",10} |");
            await file.WriteLineAsync($"| {new string('-', 20)} | {new string('-', 5)}: | {new string('-', 16)} | {new string('-', 14)} | {new string('-', 9)}: |");
            foreach (var roomType in hotel.RoomTypes)
            {
                await file.WriteLineAsync($"| {roomType.Title,-20} | {roomType.Size + " m²",6} | {roomType.Price!.ValidFrom,-16} | {roomType.Price.ValidTo,-14} | {roomType.Price.PriceEUR + " €",10} |");
            }
        }
    }
}

async Task AddHotels()
{
    var marianne = new Hotel()
    {
        Name = "Pension Marianne",
        Address = "Am Hausberg 17, 1234 Irgendwo",
        Specials = { new() { Special = Special.DogFriendly }, new() { Special = Special.OrganicFood } },
        RoomTypes = new()
        {
            new()
            {
                Title = "Luxury single room with a view to the alps",
                Description = "Single room with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "3 x 10 m²",
                DisabilityAccessible = false,
                Price = new() { PriceEUR = 40 },
                RoomsAvailable = 8
            },
            new()
            {
                Title = "Luxury double room with a view to the alps",
                Description = "Double room with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "10 x 15 m²",
                DisabilityAccessible = false,
                Price = new() { PriceEUR = 60 },
                RoomsAvailable = 2
            }
        }
    };
    var goldenerHirsch = new Hotel()
    {
        Name = "Grand Hotel Goldener Hirsch",
        Address = "Im stillen Tal 42, 4711 Schönberg",
        Specials = { new() { Special = Special.Spa }, new() { Special = Special.Sauna }, new() { Special = Special.IndoorPool }, new() { Special = Special.OutdoorPool } },
        RoomTypes = new()
        {
            new()
            {
                Title = "Luxury single room with a view to the alps",
                Description = "Single room with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "10 x 15 m²",
                DisabilityAccessible = true,
                Price = new() { PriceEUR = 70 },
                RoomsAvailable = 7
            },
            new()
            {
                Title = "Luxury double room with a view to the alps",
                Description = "Double room with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "25 x 30 m²",
                DisabilityAccessible = true,
                Price = new() { PriceEUR = 120 },
                RoomsAvailable = 4
            },
            new()
            {
                Title = "Luxury junior suites with a view to the alps",
                Description = "Junior suites with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "5 x 45 m²",
                DisabilityAccessible = true,
                Price = new() { PriceEUR = 190 },
                RoomsAvailable = 9
            },
            new()
            {
                Title = "Luxury honeymoon suite with a view to the alps",
                Description = "Honeymoon suite with balcony. Enjoy a great view to the surrounding moutains.",
                Size = "1 x 100 m²",
                DisabilityAccessible = true,
                Price = new() { PriceEUR = 300 },
                RoomsAvailable = 1
            }
        }
    };
    await context.Hotels.AddRangeAsync(new[] { marianne, goldenerHirsch });
    await context.SaveChangesAsync();
}

enum Special
{
    Spa,
    Sauna,
    DogFriendly,
    IndoorPool,
    OutdoorPool,
    BikeRental,
    ECarChargingStation,
    VegetarianCuisine,
    OrganicFood
}

class Hotel
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    public List<HotelSpecial> Specials { get; set; } = new();

    public List<RoomType> RoomTypes { get; set; } = new();
}

class HotelSpecial
{
    public int Id { get; set; }

    public Special Special { get; set; }

    public Hotel? Hotels { get; set; }
}

class RoomType
{
    public int Id { get; set; }

    public Hotel? Hotel { get; set; }

    public Price? Price { get; set; }

    public int HotelId { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Size { get; set; } = string.Empty;

    public bool DisabilityAccessible { get; set; }

    public uint RoomsAvailable { get; set; }
}

class Price
{
    public int Id { get; set; }

    public int RoomTypeId { get; set; }

    public RoomType? RoomType { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal PriceEUR { get; set; }
}


class HotelContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

class HotelContextFactory : IDesignTimeDbContextFactory<HotelContext>
{
    public HotelContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<HotelContext>();
        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new HotelContext(optionsBuilder.Options);
    }
}