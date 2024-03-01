using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () => WeatherForecastRepository.GetWeatherForecast()).WithName("GetWeatherForecast").WithOpenApi();

app.MapGet("/pizzas/{id}", (int id) => PizzaRepository.GetPizza(id)).WithOpenApi();
app.MapGet("/pizzas", () => PizzaRepository.GetPizzas()).WithOpenApi();
app.MapPost("/pizzas", (Pizza pizza) => PizzaRepository.CreatePizza(pizza)).WithOpenApi();
app.MapPut("/pizzas", (Pizza pizza) => PizzaRepository.UpdatePizza(pizza)).WithOpenApi();
app.MapDelete("/pizzas/{id}", (int id) => PizzaRepository.GetPizza(id)).WithOpenApi();

app.Run();
