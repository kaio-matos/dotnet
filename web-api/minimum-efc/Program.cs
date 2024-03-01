using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";
builder.Services.AddSqlite<PizzaDB>(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/pizzas", async (PizzaDB db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizzas", async (PizzaDB db, Pizza pizza) => {
   await db.Pizzas.AddAsync(pizza);
   await db.SaveChangesAsync();
   return Results.Created($"/pizzas/{pizza.Id}", pizza);
});
app.MapGet("/pizzas/{id}", async (PizzaDB db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizzas/{id}", async (PizzaDB db, Pizza updatePizza, int id) => {
   var pizza = await db.Pizzas.FindAsync(id);
   if (pizza is null) return Results.NotFound();
   pizza.Name = updatePizza.Name;
   pizza.Description = updatePizza.Description;
   await db.SaveChangesAsync();
   return Results.NoContent();
});
app.MapDelete("/pizzas/{id}", async (PizzaDB db, int id) => {
   var pizza = await db.Pizzas.FindAsync(id);
   if (pizza is null) return Results.NotFound();
   db.Pizzas.Remove(pizza);
   await db.SaveChangesAsync();
   return Results.Ok();
});


app.Run();
