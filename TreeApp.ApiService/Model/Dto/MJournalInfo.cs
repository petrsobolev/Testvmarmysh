namespace TreeApp.ApiService.Model.Dto;

public class MJournalInfo
{
    public long Id { get; set; }
    public Guid EventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}