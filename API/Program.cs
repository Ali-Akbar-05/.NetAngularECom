using System.Reflection;
using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlite(configuration));
builder.Services.AddScoped<StoreContextInitialiser>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerService();
builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<StoreContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.TrySeedAsync();
        }
        catch (Exception e)
        {
            var errorMsg = e.Message;
        }
    }
}
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
