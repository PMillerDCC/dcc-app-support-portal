namespace AppSupportPortal.Web.Models;

public class ApplicationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ServerId { get; set; }
    public string? ServerName { get; set; }
    //public IEnumerable<ServerViewModel>? Servers { get; set; }
}
