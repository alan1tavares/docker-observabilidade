using System;

namespace docker_observabilidade
{
    public class WeatherForecast
    {
        public WeatherForecast()
            => Id = Guid.NewGuid();

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}