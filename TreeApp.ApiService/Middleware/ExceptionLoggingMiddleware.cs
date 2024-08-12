using TreeApp.ApiService.Services.Journal;

namespace TreeApp.ApiService.Middleware;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
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
        var journalService = context.RequestServices.GetService<IJournalService>();
        await journalService?.WriteToJournalAsync(context, ex)!;
    }
}