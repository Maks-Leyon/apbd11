using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.Services;

var builder = WebApplication.CreateBuilder(args);
    
string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlServer(connectionString)
);
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
        

app.Run();