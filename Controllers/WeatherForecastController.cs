using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace docker_observabilidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static IList<WeatherForecast> _weatherForecasts;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into WeatherForecastController");

            if (_weatherForecasts == null)
            {
                var rng = new Random();
                _weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToList();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("call to WeatherForecastController controller Get method");
            return Ok(_weatherForecasts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation("call to WeatherForecastController controller Get/{id} method");
            return Ok(_weatherForecasts.Where(x => x.Id.Equals(id)).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast weatherForecast)
        {
            _logger.LogInformation("call to WeatherForecastController controller Post method");

            weatherForecast.Id = Guid.NewGuid();
            _weatherForecasts.Add(weatherForecast);

            return CreatedAtAction(nameof(Post), new { Id = weatherForecast.Id }, weatherForecast);
        }

        [HttpPut]
        public IActionResult Put(WeatherForecast weatherForecast)
        {
            _logger.LogInformation("call to WeatherForecastController controller Put method");

            WeatherForecast weatherForecastRemove = _weatherForecasts.Where(x => x.Id.Equals(weatherForecast.Id)).FirstOrDefault();
            _weatherForecasts.Remove(weatherForecastRemove);
            _weatherForecasts.Add(weatherForecast);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("call to WeatherForecastController controller Delete method");

            WeatherForecast weatherForecastRemove = _weatherForecasts.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _weatherForecasts.Remove(weatherForecastRemove);

            return Ok();
        }
    }
}