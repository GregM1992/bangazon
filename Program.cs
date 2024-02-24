using bangazonBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<bangazonBEDbContext>(builder.Configuration["bangazonBEDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/products", (bangazonBEDbContext db) =>
{
    return db.Products.ToList();
});



app.MapGet("/products/{sellerId}/store", (bangazonBEDbContext db, int sellerId) =>
{
    return db.Products.Where(p => p.SellerId == sellerId).ToList();
});



app.MapGet("/products/{id}", (bangazonBEDbContext db, int id) => 
{
    return db.Products.Single(p => p.Id == id);
});



app.MapGet("/orders", (bangazonBEDbContext db) =>
{
    return db.Orders.Include(p => p.Products).ToList();
});



app.MapGet("/orders/{id}",(bangazonBEDbContext db, int id) =>
{
    return db.Orders.Where(o =>  o.Id == id).ToList();
});



app.MapGet("/user/id", (bangazonBEDbContext db, int id) =>
{
    return db.Orders.Single(u => u.Id == id);
});



app.MapGet("/categories", (bangazonBEDbContext db) => {
    return db.Categories.ToList();
});



app.MapPost("/user", (bangazonBEDbContext db, User newUser) =>
{
    try
    {
        db.Users.Add(newUser);
        db.SaveChanges();
        return Results.Created($"/user/{newUser.Id}", newUser);
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("There was an issue creating the user, please check your input and try again");
    }
});


app.MapPost("/orders", (bangazonBEDbContext db , Order newOrder) =>
{
    try
    {
    db.Orders.Add(newOrder);
    db.SaveChanges();
    return Results.Created($"/orders/{newOrder.Id}", newOrder);
    } 
    catch (DbUpdateException)
    {
        return Results.BadRequest("There was an issue creating the order");
    }
});



app.MapPost("/orders/{orderId}/products/{productId}", (bangazonBEDbContext db, int orderId, int productId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found.");
    }

    var product = db.Products.Find(productId);

    if (product == null)
    {
        return Results.NotFound("Product not found.");
    }

    order.Products.Add(product);

    db.SaveChanges();

    return Results.Created($"/api/orders/{orderId}/products/{productId}", product);
});

app.MapDelete("/orders/{orderId}/products/{productId}", (bangazonBEDbContext db, int orderId, int productId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found.");
    }

    var product = db.Products.Find(productId);

    if (product == null)
    {
        return Results.NotFound("Product not found.");
    }

    order.Products.Remove(product);

    db.SaveChanges();

    return Results.Created($"/api/orders/{orderId}/products/{productId}", product);
});

app.MapGet("/payments", (bangazonBEDbContext db, int id) =>
{
    return db.PaymentTypes.ToList();
});


app.Run();

