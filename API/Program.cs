
 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

 var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
  builder.Services.AddDbContext<StoreContext>(options=>options.UseSqlite(configuration));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo{Title="Api",Version="v1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","API V1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
