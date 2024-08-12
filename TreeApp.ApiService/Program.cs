using Microsoft.EntityFrameworkCore;
using TreeApp.ApiService.Extensions;
using TreeApp.ApiService.Infrastructure;
using TreeApp.ApiService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.AddNpgsqlDbContext<TreeAppContext>("treeAppDb");
builder.Services.AddApplicationDependencies();


var app = builder.Build();

app.UseMiddleware<ExceptionLoggingMiddleware>();
app.MapDefaultEndpoints();
app.UseStaticFiles();


app.UseSwagger();
app.UseStaticFiles(new StaticFileOptions()
{
    ServeUnknownFileTypes = true
});
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TreeAppContext>();
    context.Database.Migrate();
}

// Configure Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/Definitions/swagger.yaml", "Users API");
    c.RoutePrefix = "swagger";
});




app.MapControllers();


app.Run();


