using Microsoft.EntityFrameworkCore;
using AdSystem.Data;
using AdSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS settings
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// JSON Reference handler for supporting circular references.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register ApiService
builder.Services.AddHttpClient<ApiService>();

// Add DbContext for AdSystem
builder.Services.AddDbContext<AdDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DAL
builder.Services.AddScoped<DataAccessLayer>();

var app = builder.Build();

// Initialize the database and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AdDbContext>();

    // Apply migrations
    dbContext.Database.Migrate();

    // Seed database
    AdDatabaseInitializer.Initialize(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
