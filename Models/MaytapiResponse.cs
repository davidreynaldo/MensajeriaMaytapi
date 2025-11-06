namespace ProyectoMaytapiMVC.Models
{
    public class MaytapiResponse
    {
        public bool Success { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Raw { get; set; } = string.Empty;
    }
}
