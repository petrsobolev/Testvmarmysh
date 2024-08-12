var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("treeAppDb");

var apiService = builder
    .AddProject<Projects.TreeApp_ApiService>("apiservice")
    .WithReference(postgresdb)
    .WithExternalHttpEndpoints();


builder.Build().Run();
