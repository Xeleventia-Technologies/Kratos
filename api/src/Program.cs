using Serilog;

using Kratos.Api.Middleware;
using Kratos.Api.Startup;
using Kratos.Api.Common.Options;
using Kratos.Api._Pages;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithConfig(builder.Configuration);
builder.Services.AddProblemDetails();

// ----Add services here-----
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddCommonServices();
builder.Services.AddServicesFromAssembly();
// --------------------------

builder.Services.AddFirebase();
builder.Services.AddJwtAuth(builder.Configuration.GetRequiredSection(JwtOptions.SectionName).Get<JwtOptions>()!);
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("Default")!);
builder.Services.AddCors();
builder.Services.AddRazorComponents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.ToString()); });

var app = builder.Build();

app.CreateRequiredFolders();
await app.InitializeDatabaseAsync();

app.UseSerilogRequestLogging();

// ----Map endpoints here-----
app.MapEndpointsFromAssembly();
// --------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseCors(x => x
    .SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithExposedHeaders("Content-Disposition")
);

app.UseFileServer();

app.UseAuthentication();
app.UseAuthorization();

app.UseForwardedHeaders();
app.UseExceptionHandler();

app.UseAntiforgery();
app.MapRazorComponents<_App>();

app.Run();
await Log.CloseAndFlushAsync();

public partial class Program;
