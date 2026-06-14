# Evidencija Šahovskih Turnira

Seminarski rad iz predmeta **Razvoj Višeslojnog Softvera** (RVS 2025/26)  
Tehnički fakultet „Mihajlo Pupin", Zrenjanin  
Student: Uroš Milin | Broj indeksa: SI 20/22

## Opis projekta

Veb aplikacija za evidenciju šahovskih turnira. Omogućava unos, pregled, izmenu i brisanje turnira sa konačnim plasmanom igrača. Automatski se izračunava nagrada za igrače koji su završili na mestima koja nose nagradu, proporcionalno prema plasmanskom koeficijentu.

**Poslovno pravilo:** AKO igrač završi na jednom od prvih X mesta (X se čita iz XML fajla `pravila_nagrade.xml`), ONDA mu se automatski izračunava i dodeljuje odgovarajući deo nagradnog fonda.

## Tehnologije

- .NET 8
- ASP.NET Core MVC (prezentacioni sloj)
- ASP.NET Core Web API (sloj servisa)
- Entity Framework Core 8 (rad sa bazom podataka)
- Microsoft SQL Server
- Bootstrap 5

## Struktura projekta

SahovskiTurnir/
├── SlojPodataka/ # Entiteti, EF Context, Repository klase, DBUtils
├── SlojPoslovneLogike/ # Poslovno pravilo, čitač XML parametara
├── SlojServisa/ # REST API kontroleri, DTO klase, Mapper klase
└── PrezentacioniSloj/ # MVC kontroleri, ViewModeli, Views

## Pokretanje projekta

### Preduslovi

- Visual Studio 2022
- .NET 8 SDK
- Microsoft SQL Server

### Koraci

1. Kloniraj repozitorijum
2. Otvori `SahovskiTurnir.sln` u Visual Studiou
3. U `SlojServisa/appsettings.json` podesi connection string:

```json
"ConnectionStrings": {
  "Konekcija": "Server=IME_TVOG_SERVERA;Database=SahovskiTurnirDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. Desni klik na Solution → **Set Startup Projects** → postavi `SlojServisa` i `PrezentacioniSloj` na **Start**
5. Pokreni sa **F5** — baza se automatski kreira pri prvom pokretanju

### Podrazumevani kredencijali

- Korisničko ime: `admin`
- Lozinka: `admin123`

## SQL skripte

U folderu `/sql` nalaze se:

- `SQLSkripta.sql` — kreiranje baze i tabela
- `SQLProcedure.sql` — stored procedure

## Poslovno pravilo — XML parametrizacija

Broj mesta koja nose nagradu čita se iz:
SlojServisa/Ogranicenja/pravila_nagrade.xml
Podrazumevana vrednost je **3** (prvih 3 mesta dobijaju nagradu). Vrednost se može promeniti direktno u XML fajlu bez rekompajliranja aplikacije.
