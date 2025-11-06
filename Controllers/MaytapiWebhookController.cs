using Microsoft.AspNetCore.Mvc;

namespace ProyectoMaytapiMVC.Controllers
{
    public class MaytapiWebhookController: Controller
    {
        private readonly ILogger<MaytapiWebhookController> _logger;

        public MaytapiWebhookController(ILogger<MaytapiWebhookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Recibir([FromBody] object payload)
        {
            // Aquí puedes procesar el JSON entrante de Maytapi
            _logger.LogInformation($"Mensaje entrante: {payload}");
            return Ok();
        }
    }
}
