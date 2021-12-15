using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace docker_observabilidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
            => _logger = logger;

        [HttpGet]
        [Route("Information")]
        public IActionResult Information()
        {
            _logger.LogInformation("call to LogController controller Information method");
            return Ok();
        }

        [HttpGet]
        [Route("Debug")]
        public IActionResult Debug()
        {
            _logger.LogDebug("call to LogController controller Debug method");
            return Ok();
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            _logger.LogError("call to LogController controller Error method");
            return Ok();
        }

        [HttpGet]
        [Route("Trace")]
        public IActionResult Trace()
        {
            _logger.LogTrace("call to LogController controller Trace method");
            return Ok();
        }

        [HttpGet]
        [Route("Warning")]
        public IActionResult Warning()
        {
            _logger.LogWarning("call to LogController controller Warning method");
            return Ok();
        }

        [HttpGet]
        [Route("Critical")]
        public IActionResult Critical()
        {
            _logger.LogCritical("call to LogController controller Critical method");
            return Ok();
        }
    }
}