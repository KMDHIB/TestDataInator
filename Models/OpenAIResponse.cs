namespace TestDataInator.Models
{
    public class OpenAIResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<OpenAIChoice> choices { get; set; }
        public OpenAIUsage usage { get; set; }
    }

    public class OpenAIChoice
    {
        public int index { get; set; }
        public OpenAIMessage message { get; set; }
        public string finish_reason { get; set; }
    }

    public class OpenAIUsage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
