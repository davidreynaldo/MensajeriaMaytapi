using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProyectoMaytapiMVC.Hubs;
using ProyectoMaytapiMVC.Models;
using System.Text.Json;

namespace ProyectoMaytapiMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController: ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public WebhookController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement payload)
        {
            try
            {
                // Maytapi envía el mensaje en la propiedad "message"
                string json = payload.GetRawText();
                var data = JsonSerializer.Deserialize<MaytapiWebhookModel>(json);

                if (data != null && data.message != null)
                {
                    string numero = data.message.from ?? "Desconocido";
                    string texto = data.message.text ?? "(sin texto)";

                    // Envía el mensaje recibido al frontend en tiempo real
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", numero, texto);
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
     }
}
