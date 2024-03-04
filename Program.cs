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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5003")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/products", (bangazonBEDbContext db) =>
{
    return db.Products.Include(p => p.Category).ToList();

});



app.MapGet("/products/{sellerId}/store", (bangazonBEDbContext db, int sellerId) => //get products from a single seller
{
    return db.Products.Where(p => p.SellerId == sellerId).ToList();
});



app.MapGet("/products/{id}", (bangazonBEDbContext db, int id) =>  // get product details
{
    return db.Products.Single(p => p.Id == id);
});



app.MapGet("/orders", (bangazonBEDbContext db) => //get all orders
{
    return db.Orders.Include(p => p.Products).ToList();
});



app.MapGet("/orders/{id}",(bangazonBEDbContext db, int id) =>  //get a single users orders
{
    return db.Orders.Where(o =>  o.Id == id).ToList();
});



app.MapGet("/user/{id}", (bangazonBEDbContext db, int id) => //get a single users details
{
    return db.Orders.Single(u => u.Id == id);
});

app.MapGet("/checkuser/{uid}", (bangazonBEDbContext db, string uid) => //check for user
{
    var user = db.Users.Where(user => user.Uid == uid).ToList();

    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});


app.MapGet("/categories", (bangazonBEDbContext db) => { // get all categories
    return db.Categories.ToList();
});



app.MapPost("/user", (bangazonBEDbContext db, User newUser) =>  // register a user
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


app.MapPost("/openOrder", (bangazonBEDbContext db , Order newOrder) => // get open order/cart
{
    if (db.Orders.Any(o => o.IsComplete)){
    throw new Exception("You already have an open cart, please complete that one first.");

    } else
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
    }

});




app.MapPost("/orders/addProductToCart", (bangazonBEDbContext db, addProductDTO newProduct) =>  // add product to open cart
{
    Order openOrder = db.Orders.Include(p => p.Products).FirstOrDefault(o => !o.IsComplete);

    newProduct.OrderId = openOrder.Id;

    if (openOrder == null)
    {
        return Results.NotFound("Order not found.");
    }

    var product = db.Products.Find(newProduct.ProductId);

    if (product == null)
    {
        return Results.NotFound("Product not found.");
    }

    openOrder.Products.Add(product);

    db.SaveChanges();

    return Results.Created($"/orders/addProduct", newProduct);
});

app.MapDelete("/cart/{productId}", (bangazonBEDbContext db, int productId) =>  //delete product from cart
{
    Order openOrder = db.Orders.Include(o => o.Products).ThenInclude(p => p.Category).FirstOrDefault(o => !o.IsComplete);

    var productToDelete = openOrder.Products.FirstOrDefault(o => o.Id == productId);

    openOrder.Products.Remove(productToDelete);

    db.SaveChanges();

});

app.MapGet("/payments", (bangazonBEDbContext db, int id) => // get all payment types
{
    return db.PaymentTypes.ToList();
});

app.MapDelete("/orders", (bangazonBEDbContext db, int id) =>  //delete single order
{
    var orderToDelete = db.Orders.FirstOrDefault(o => o.Id == id);
    
    db.Orders.Remove(orderToDelete);
    db.SaveChanges();

});

app.MapPatch("/orders", (bangazonBEDbContext db, int id) => //close order
{
    var orderToClose = db.Orders.FirstOrDefault(o => o.Id == id);
    orderToClose.IsComplete = true;
    db.SaveChanges();
});

app.MapGet("/cart", (bangazonBEDbContext db) => // get open order/cart to show products
{
    Order openOrder = db.Orders.Include(o => o.Products).ThenInclude(p => p.Category).FirstOrDefault(o => !o.IsComplete);
    return openOrder.Products;
    
});

app.MapGet("/openCart", (bangazonBEDbContext db) => //get order/cart
{
    Order openOrder = db.Orders.Include(o => o.Products).ThenInclude(p => p.Category).FirstOrDefault(o => !o.IsComplete);
    return openOrder;
});


app.MapPost("/createProduct", (bangazonBEDbContext db, Product newProduct) => //create new product
{
    db.Products.Add(newProduct);
    db.SaveChanges();
});

app.Run();

