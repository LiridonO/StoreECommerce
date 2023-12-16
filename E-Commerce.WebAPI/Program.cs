using E_Commerce.WebAPI.Extensions;
using E_Commerce.WebAPI.Helpers;
using E_Commerce.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;


builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

//if (app.Environment.IsDevelopment())
//{
//}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.UseRouting();
app.UseStaticFiles();
app.UseCors("CorsPolicy");


var scope = app.Services.CreateScope();
var context= scope.ServiceProvider.GetRequiredService<StoreContext>();
var lf = app.Services.GetRequiredService<ILoggerFactory>();
await context.SeedAsync(lf);

app.Run();
