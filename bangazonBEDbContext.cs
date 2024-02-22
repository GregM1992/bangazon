using Microsoft.EntityFrameworkCore;
using bangazonBE.Models;

public class bangazonBEDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Category> Categories { get; set; }

    public bangazonBEDbContext(DbContextOptions<bangazonBEDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, Uid = "jsUn6Bt9QFWVaLvZMFUyN8hygCH2", FirstName = "Gregory", LastName = "Markus", Address = "2004 Nashville St. Nashville", Email = "myemail.com", IsSeller = true, UserName = "gregGroks13" },
            new User { Id = 2, FirstName = "Joe", Uid = null, LastName = "Guy", Address = "102 theway dr.", Email = "email2.com", UserName = "username2", IsSeller = true}

        });
        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1, CategoryId = 1, ProductName = "bottle", PricePerUnit = 2.00m, QuantityAvailable = 4, SellerId =1},
            new Product { Id = 2, CategoryId = 1, ProductName = "mug", PricePerUnit = 3.23m, QuantityAvailable = 3, SellerId = 1},
            new Product { Id = 3, CategoryId = 3, ProductName = "sharpie", PricePerUnit = 1.00m, QuantityAvailable = 2, SellerId = 2},
            new Product { Id = 4, CategoryId = 1, ProductName = "screwdriver", PricePerUnit = 2.00m, QuantityAvailable = 2, SellerId = 1},
            new Product { Id = 5, CategoryId = 3, ProductName = "stapler", PricePerUnit = 12.00m, QuantityAvailable = 3, SellerId = 1},
            new Product { Id = 6, CategoryId = 2, ProductName = "microwave", PricePerUnit = 200.00m, QuantityAvailable = 12, SellerId = 2},
        });
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CustomerId = 1, IsComplete = false, PaymentTypeId = 2},
            new Order { Id = 2, CustomerId = 2, IsComplete = false, PaymentTypeId = 1},
            new Order { Id = 3, CustomerId = 2, IsComplete = true , PaymentTypeId = 2}
        });
        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType { Id = 1, Type = "Credit"},
            new PaymentType { Id = 2, Type = "Cash"},
            new PaymentType { Id = 3, Type = "Check"}
        });
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, Title = "Tools"},
            new Category { Id = 2, Title = "Appliances"},
            new Category { Id = 3, Title = "Home/Office"}
        });
   
    }
}

