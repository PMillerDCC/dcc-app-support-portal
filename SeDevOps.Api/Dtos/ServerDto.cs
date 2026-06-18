namespace SeDevOps.Api.Dtos
{
    public class ServerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Hostname { get; set; }
        public string IPAddress { get; set; }
    }
}
