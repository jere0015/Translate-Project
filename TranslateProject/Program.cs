using TranslateProject.Models;
using TranslateProject.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITranslationService, TranslationService>();
builder.Services.AddHttpClient();

TranslationApiKeys translationApiKeys = builder.Configuration.GetSection("TranslationApiKeys").Get<TranslationApiKeys>();

string nlp = translationApiKeys.Nlp;

builder.Services.AddSingleton(translationApiKeys);

WebApplication app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();