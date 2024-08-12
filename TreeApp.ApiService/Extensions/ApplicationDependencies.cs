using TreeApp.ApiService.Services.Journal;
using TreeApp.ApiService.Services.Tree;

namespace TreeApp.ApiService.Extensions;

public static class ApplicationDependencies
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddTransient<ITreeService, TreeService>();
        services.AddTransient<IJournalService, JournalService>();
    }
}