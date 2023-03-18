using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using WebAppSampleCore.Interfaces.Services;
using WebAppSampleCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Identity
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//}).AddEntityFrameworkStores<ApplicationDbContext>();
#endregion

// Add Localization 
builder.Services.AddLocalization();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

// Configure supported cultures and localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var defaultCulture = "tr";
    var supportedCultures = new[]
        {
        new CultureInfo(defaultCulture),
        new CultureInfo("en"),
        new CultureInfo("fr")
    };

    // State what the default culture for your application is. This will be used if no specific culture
    // can be determined for a given request.
    options.DefaultRequestCulture = new RequestCulture(culture: defaultCulture, uiCulture: defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    //options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    //{
    //    var requestCulture = context.Features.Get<IRequestCultureFeature>();
    //    if(requestCulture?.Provider != null)
    //    {
    //        return await requestCulture!.Provider.DetermineProviderCultureResult(context);
    //    }

    //    var requestCookie = context.Request.Cookies.FirstOrDefault(x => x.Key == CookieRequestCultureProvider.DefaultCookieName);

    //    // My custom request culture logic
    //    return await Task.FromResult(new ProviderCultureResult(defaultCulture));
    //}));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    #region creating database during deployment
    //// For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
    //try
    //{
    //    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>()
    //        .CreateScope())
    //    {
    //        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
    //             .Database.Migrate();
    //    }
    //}
    //catch { }
    #endregion

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use Localization
//var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
//app.UseRequestLocalization(locOptions.Value);
app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.Run();
