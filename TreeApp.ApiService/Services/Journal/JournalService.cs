using Microsoft.EntityFrameworkCore;
using TreeApp.ApiService.Exceptions;
using TreeApp.ApiService.Extensions;
using TreeApp.ApiService.Infrastructure;
using TreeApp.ApiService.Model;
using TreeApp.ApiService.Model.Dto;

namespace TreeApp.ApiService.Services.Journal;

public class JournalService : IJournalService
{
    private readonly TreeAppContext _context;

    public JournalService(TreeAppContext context)
    {
        _context = context;
    }
    public async Task WriteToJournalAsync(HttpContext context, Exception exception)
    {
        var journal = new JournalItem
        {
            Type = exception.GetType().ToString(),
            Timestamp = DateTime.UtcNow,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            QueryParametrs = context.Request.QueryString.ToString(),
            BodyParametrs = await new StreamReader(context.Request.Body).ReadToEndAsync()
        };
        await _context.JournalItems.AddAsync(journal);
        await _context.SaveChangesAsync();

    }

    public async Task<MJournal> GetJournalByIdAsync(int id, CancellationToken cancellationToken)
    {
        var journalItem = await _context.JournalItems.FindAsync(id, cancellationToken);

        if (journalItem is null)
        {
            throw new NotFoundJournalException(ErrorMessages.GetNotFoundJournalExceptionMessage(id));
        }

        var journal = new MJournal
        {
            Id = journalItem.Id,
            EventId = journalItem.EventId, // Assuming EventId is the same as Id in this context
            CreatedAt = journalItem.Timestamp,
            Text = journalItem.Message
        };

        return journal;
    }

    public async Task<object?> GetJournalRangeAsync(int skip, int take, VJournalFilter filter, CancellationToken cancellationToken)
    {
        var query = _context.JournalItems.AsQueryable();

        if (filter.From.HasValue)
            query = query.Where(x => x.Timestamp >= filter.From.Value);

        if (filter.To.HasValue)
            query = query.Where(x => x.Timestamp <= filter.To.Value);

        if (!string.IsNullOrEmpty(filter.Search))
            query = query.Where(x => x.Message.Contains(filter.Search));

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip(skip).Take(take).Select(x => new MJournalInfo
        {
            Id = x.Id,
            EventId = x.EventId,
            CreatedAt = x.Timestamp
        }).ToListAsync(cancellationToken);

        return new MRange<MJournalInfo>
        {
            Skip = skip,
            Count = count,
            Items = items
        };
    }
}