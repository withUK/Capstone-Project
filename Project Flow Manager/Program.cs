using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InnovationManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnovationManagerContext") ?? throw new InvalidOperationException("Connection string 'InnovationManagerContext' not found.")));

builder.Services.AddDbContext<ProjectFlowAdministrationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectFlowAdministrationContext") ?? throw new InvalidOperationException("Connection string 'ProjectFlowAdministrationContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.Run();
