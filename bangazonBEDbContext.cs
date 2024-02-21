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
            new User { Id = 1, uId = "jsUn6Bt9QFWVaLvZMFUyN8hygCH2", firstName = "Gregory", lastName = "Markus", address = "2004 Nashville St. Nashville", email = "myemail.com", isSeller = true, userName = "gregGroks13" },
            new User { Id = 2, firstName = "Joe", uId = null, lastName = "Guy", address = "102 theway dr.", email = "email2.com", userName = "username2", isSeller = true}

        });
        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1, categoryId = 1, productName = "bottle", pricePerUnit = 2.00m, quantityAvailable = 4, sellerId =1},
            new Product { Id = 2, categoryId = 1, productName = "mug", pricePerUnit = 3.23m, quantityAvailable = 3, sellerId = 1},
            new Product { Id = 3, categoryId = 3, productName = "sharpie", pricePerUnit = 1.00m, quantityAvailable = 2, sellerId = 2}
        });
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, customerId = 1, isComplete = false, paymentTypeId = 2},
            new Order { Id = 2, customerId = 2, isComplete = false, paymentTypeId = 1},
            new Order { Id = 3, customerId = 2, isComplete = true , paymentTypeId = 2}
        });
        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType { Id = 1, type = "Credit"},
            new PaymentType { Id = 2, type = "Cash"},
            new PaymentType { Id = 3, type = "Check"}
        });
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, Title = "Tools"},
            new Category { Id = 2, Title = "Appliances"},
            new Category { Id = 3, Title = "Home/Office"}
        });
    }
}

