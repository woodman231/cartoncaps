using Microsoft.EntityFrameworkCore;
using CartonCapsDbContext;
using CartonCapsAccountRepository;
using CartonCapsInvitationRepository;
using CartonCapsAccountService;
using CartonCapsInvitationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add the db context
var connectionString = builder.Configuration.GetConnectionString("CartonCapsDb");
builder.Services.AddDbContext<CartonCapsEFDbContext>(
    options => options.UseSqlServer(connectionString)
);

// Register repositories
builder.Services.AddScoped<ICartonCapsAccountRepository, CartonCapsAccountEFRepository>();
builder.Services.AddScoped<ICartonCapsInvitationRepository, CartonCapsInvitationEFRepository>();

// Register Services
builder.Services.AddScoped<ICartonCapsAccountService, CartonCapsAccountAppService>();
builder.Services.AddScoped<ICartonCapsInvitationService, CartonCapsInvitationAppService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/openapi.json", "CartonCapsAPI v1");
    });
}

app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();
