using Jemma.Api;
using Jemma.Application;
using Jemma.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin(); // Allow all origins
                    builder.AllowAnyMethod(); // Allow any HTTP method
                    builder.AllowAnyHeader(); // Allow any HTTP headers
                });
        });

    builder.Services
        .AddPresentation(builder.Configuration)
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
// using (var scope = scopeFactory.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//     IdentitySeeder.CreateDefaultUsers(userManager, roleManager, context, builder.Configuration).Wait();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();
