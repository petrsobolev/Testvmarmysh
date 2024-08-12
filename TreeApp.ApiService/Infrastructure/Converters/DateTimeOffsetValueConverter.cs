using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TreeApp.ApiService.Infrastructure.Converters;

public class DateTimeOffsetValueConverter : ValueConverter<DateTimeOffset, DateTime>
{
    public DateTimeOffsetValueConverter()
        : base(model => model.UtcDateTime, persistence => new DateTimeOffset(persistence, TimeSpan.Zero))
    {
    }
}
