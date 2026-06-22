namespace SeDevOps.Api.Dtos
{
    public class ApplicationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServerId { get; set; }
    }
}
