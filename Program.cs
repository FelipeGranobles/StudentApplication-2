using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Repositories;
using StudentApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Connection"); 
builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add repositories
#region Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
#endregion

//Add Services
#region Services
builder.Services.AddScoped<IStudentService, StudentService>();
#endregion 


var app = builder.Build();

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
