using BBIS.Api.Extensions;
using Microsoft.AspNetCore.SpaServices;
using Newtonsoft.Json.Serialization;
using NLog;
using VueCliMiddleware;

var builder = WebApplication.CreateBuilder(args);
var SpaPath = "./ClientApp/dist";
var spaCorsPolicy = "CorsPolicy";

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureMysqlDbContext(builder.Configuration);
builder.Services.ConfigureCorsPolicy(spaCorsPolicy);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureTimedHostedService();
builder.Services.ConfigureMailSetting(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerOption();
builder.Services.AddMvc();

builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.ConfigureAuthorizationPolicies();
/*==================================================================================================*/

builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = SpaPath);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the ability for the API to serve static content (i.e. Image files and CSS files)
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseSpaStaticFiles();

app.UseCors(spaCorsPolicy);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        endpoints.MapToVueCliProxy(
            "{*path}",
            new SpaOptions { SourcePath = SpaPath },
            npmScript: "serve",
            regex: "Compiled successfully");
    }
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = SpaPath;
});

app.Run();
