namespace SeDevOps.Api.Dtos
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
    }
}
