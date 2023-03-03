namespace TranslateProject.Models
{
    public class NlpResponse
    {
        public int status { get; set; }
        public string? from { get; set; }
        public string? to { get; set; }
        public string? original_text { get; set; }
        public Dictionary<string, string> translated_text { get; set; }
        public int translated_characters { get; set; }
    }
}
