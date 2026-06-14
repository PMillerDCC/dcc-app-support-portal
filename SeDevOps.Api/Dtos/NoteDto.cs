namespace SeDevOps.Api.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
    }
}
