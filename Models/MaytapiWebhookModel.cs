namespace ProyectoMaytapiMVC.Models
{
    public class MaytapiWebhookModel
    {
        public string type { get; set; } = string.Empty;
        public Message message { get; set; } = new Message();

        public class Message
        {
            public string from { get; set; } = string.Empty;
            public string text { get; set; } = string.Empty;
        }
    }
}
