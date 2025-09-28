using WebApi;

var builder = WebApplication.CreateBuilder(args);

#region Services
var services = builder.Services;
var config = builder.Configuration;

services.AddInfrastructure(config);
services.AddApplicationServices();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#endregion

#region app
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
#endregion
