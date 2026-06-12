using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var nizKonekcije = builder.Configuration.GetConnectionString("Konekcija");
Konekcija.NizKonekcije = nizKonekcije!;

builder.Services.AddDbContext<TurnirDbContext>(opcije =>
    opcije.UseSqlServer(nizKonekcije));

builder.Services.AddScoped<TurnirRepozitorijum>();
builder.Services.AddScoped<IgracRepozitorijum>();
builder.Services.AddScoped<KorisnikRepozitorijum>();

var app = builder.Build();

using (var opseg = app.Services.CreateScope())
{
    var kontekst = opseg.ServiceProvider.GetRequiredService<TurnirDbContext>();
    kontekst.Database.Migrate();
    PocetniPodaci.Inicijalizuj(kontekst);
}

app.UseAuthorization();
app.MapControllers();
app.Run();