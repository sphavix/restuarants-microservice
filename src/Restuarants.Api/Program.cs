using Restuarants.Api.Extensions;
using Restuarants.Api.Middlewares;
using Restuarants.Application.Extensions;
using Restuarants.Domain.Entities;
using Restuarants.Infrastructure.Extensions;
using Restuarants.Infrastructure.SeedData;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestuarantSeeder>();

await seeder.SeedData();

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.MapGroup("api/accounts")
    .WithTags("Accounts")
    .MapIdentityApi<ApplicationUser>();
app.UseAuthorization();

app.MapControllers();

app.Run();
