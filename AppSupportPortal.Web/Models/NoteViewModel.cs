namespace AppSupportPortal.Web.Models
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ApplicationId { get; set; }
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? ApplicationName { get; set; }
    }
}
