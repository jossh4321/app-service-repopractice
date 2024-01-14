using Microsoft.FeatureManagement;
using WebAppPractice.IService;
using WebAppPractice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = "Endpoint=https://appconfig0100.azconfig.io;Id=30MU;Secret=VhQ1F85RmiltpgtSqUT8367u0by8qtt2HeG3HqUJrSM=";
builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(config =>
    {
        config.Connect(connectionString)
        .UseFeatureFlags(featureFlag =>
        {
            featureFlag.CacheExpirationInterval = TimeSpan.FromSeconds(1);
        });
    });
});

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddRazorPages();
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseAzureAppConfiguration();

app.Run();
