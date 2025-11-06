using Microsoft.AspNetCore.SignalR;

namespace ProyectoMaytapiMVC.Hubs
{
    public class ChatHub: Hub
    {
        // Método para enviar un mensaje a todos los clientes conectados
        public async Task BroadcastMessage(string numero, string mensaje)
        {
            await Clients.All.SendAsync("ReceiveMessage", numero, mensaje);
        }
    }
}
