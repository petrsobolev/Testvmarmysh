using TreeApp.ApiService.Model.Dto;

namespace TreeApp.ApiService.Services.Journal;

public interface IJournalService
{
    Task WriteToJournalAsync(HttpContext context, Exception exception);
    Task<MJournal> GetJournalByIdAsync(int id, CancellationToken cancellationToken);
    Task<object?> GetJournalRangeAsync(int skip, int take, VJournalFilter filter, CancellationToken cancellationToken);
}   