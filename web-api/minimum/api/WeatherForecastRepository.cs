record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class WeatherForecastRepository
{
	static private string[] _summaries = [
    	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	public static WeatherForecast[]? GetWeatherForecast() {
		var forecast =  Enumerable.Range(1, 5).Select(index =>
			new WeatherForecast
			(
				DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				Random.Shared.Next(-20, 55),
				_summaries[Random.Shared.Next(_summaries.Length)]
			))
			.ToArray();
		return forecast;
	}
}
