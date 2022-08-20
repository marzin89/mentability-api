using MentabilityAPI.Data;
using Microsoft.EntityFrameworkCore;

var TrustedOrigins = "TrustedOrigins";
var builder = WebApplication.CreateBuilder(args);

// Möjliggör anrop från andra domäner
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: TrustedOrigins,
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000",
                "https://localhost:7096"
            );
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection av context-klassen, databasen och anslutningssträngen
builder.Services.AddDbContext<MentabilityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbString")));

var app = builder.Build();

// Akviterar CORS
app.UseCors(TrustedOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
