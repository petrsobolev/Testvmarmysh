using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeApp.ApiService.Model;

public class JournalItem
{
    public JournalItem()
    {
        
    }
    public JournalItem(
        int id
        , Guid eventId
        , string type
        , DateTimeOffset timestamp
        , string message
        , string? stackTrace
        , string queryParams
        , string bodyParams)
    {
        Id = id;
        EventId = eventId;
        Type = type;
        Timestamp = timestamp;
        Message = message;
        StackTrace = stackTrace;
        QueryParametrs = queryParams;
        BodyParametrs = bodyParams;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid EventId { get; set; }

    public string Type { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string Message { get; set; }

    public string? StackTrace { get; set; }

    public string QueryParametrs { get; set; }
    
    public string BodyParametrs { get; set; }
}