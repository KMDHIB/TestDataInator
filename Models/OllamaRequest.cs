namespace TestDataInator.Models {
    public class OllamaRequest {
        public string model { get; set; } = string.Empty;
        public string prompt { get; set; } = string.Empty;
        public bool stream { get; set; } = false;
        public double temperature { get; set; } = 0.7;
    }
}