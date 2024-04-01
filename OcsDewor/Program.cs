using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OcsDewor.Services;
using OcsDewor.Models;
using OcsDewor.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ITypeActivityRepository, TypeActivityRepository>();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("Postgres");
    options.UseNpgsql(connection);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "test Ocs", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();