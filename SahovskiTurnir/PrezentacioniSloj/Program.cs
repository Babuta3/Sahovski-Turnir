using SlojPoslovneLogike.Ogranicenja;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddHttpClient("SahovskiApi", klijent =>
{
    klijent.BaseAddress = new Uri(builder.Configuration["ServisUrl"]!);
});

builder.Services.AddHttpClient<CitacPravila>(klijent =>
{
    klijent.BaseAddress = new Uri(builder.Configuration["ServisUrl"]!);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Nalog}/{action=Prijava}/{id?}");

app.Run();