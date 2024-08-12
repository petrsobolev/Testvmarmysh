using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TreeApp.ApiService.Infrastructure.Converters;

public class JsonValueConverter<TModel>() : ValueConverter<TModel, string>(
    domain => JsonSerializer.Serialize(domain, JsonSerializerOptions.Default),
    persistence => JsonSerializer.Deserialize<TModel>(persistence, JsonSerializerOptions.Default)!);
