using CourseProjectDb;
using CourseProjectNew;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    DbSeeder.SeedAirplaneTypes(dbContext);
    DbSeeder.SeedDepartments(dbContext);
    DbSeeder.SeedPassengers(dbContext);
    DbSeeder.SeedFlights(dbContext);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
