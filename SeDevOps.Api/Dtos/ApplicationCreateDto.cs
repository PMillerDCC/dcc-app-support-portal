namespace SeDevOps.Api.Dtos
{
    public class ApplicationCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ServerId { get; set; }
    }
}