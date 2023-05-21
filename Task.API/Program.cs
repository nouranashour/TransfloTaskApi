using Task.Domain.Helpers;
using Task.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DBSetting dbSetting = new DBSetting();
builder.Configuration.GetSection("DBSetting").Bind(dbSetting);

builder.Services.Configure<DBSetting>(
    builder.Configuration.GetSection(nameof(DBSetting)));

DependencyContainer.RegisterServices(builder.Services);  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(Policy =>Policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
