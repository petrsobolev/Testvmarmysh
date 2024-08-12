namespace TreeApp.ApiService.Model.Dto;

public class MJournal
{
    public string Text { get; set; }
    public long Id { get; set; }
    public Guid EventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}