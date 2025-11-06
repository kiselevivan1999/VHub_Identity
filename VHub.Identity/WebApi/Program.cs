using WebApi;
using WebApi.Middlewares;
using Infrastructure.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

#region Services
var services = builder.Services;
var config = builder.Configuration;

services.AddInfrastructure(config);
services.AddApplicationServices();
services.AddControllers();

services.AddCors(corOpt => { corOpt.AddDefaultPolicy(conf => conf.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); });
services.AddAuthenticationAndAuthorizationService(config);

services.AddSwaggerService(config);

#endregion

#region app
var app = builder.Build();

await app.Services.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
