using Microsoft.AspNetCore.Mvc;
using ProyectoMaytapiMVC.Models;
using System.Text;
using System.Text.Json;

namespace ProyectoMaytapiMVC.Controllers
{
    public class WhatsAppController: Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public WhatsAppController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        // Mostrar vista principal
        public IActionResult Index()
        {
            return View(new MaytapiMessageModel());
        }

        // Acción para enviar mensaje        
        [HttpPost]
        public async Task<IActionResult> Enviar(MaytapiMessageModel model)
        {
            if (string.IsNullOrWhiteSpace(model.NumeroDestino) || string.IsNullOrWhiteSpace(model.Mensaje))
            {
                model.Resultado = "⚠️ Debes ingresar número y mensaje.";
                return View("Index", model);
            }

            var client = _httpClientFactory.CreateClient();

            string productId = _config["Maytapi:ProductId"];
            string phoneId = _config["Maytapi:PhoneId"];
            string apiKey = _config["Maytapi:ApiKey"];

            string url = $"https://api.maytapi.com/api/{productId}/{phoneId}/sendMessage";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("x-maytapi-key", apiKey);

            // ✅ Estructura correcta para Maytapi
            var payload = new
            {
                to_number = model.NumeroDestino.Replace("+", "").Trim(), // quitar + si el usuario lo pone
                type = "text",
                message = model.Mensaje
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            string resultJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                model.Resultado = "✅ Mensaje enviado correctamente.";
            }
            else
            {
                model.Resultado = $"❌ Error al enviar mensaje: {resultJson}";
            }

            return View("Index", model);
        }

    }
}
