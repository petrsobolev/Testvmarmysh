using TreeApp.ApiService.Services.Journal;

namespace TreeApp.ApiService.Middleware;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJournalService _journalService;

    public ExceptionLoggingMiddleware(RequestDelegate next, IJournalService journalService)
    {
        _next = next;
        _journalService = journalService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogExceptionAsync(context, ex);
            throw; // rethrow the exception after logging it
        }
    }

    private async Task LogExceptionAsync(HttpContext context, Exception ex)
    {
        await _journalService.WriteToJournalAsync(context, ex);
    }
}