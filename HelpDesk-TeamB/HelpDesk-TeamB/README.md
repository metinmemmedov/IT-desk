# IT Help Desk — Texniki Dəstək Müraciətlərinin İdarə Edilməsi Sistemi

Project Status: In Development

Universitet əməkdaşları və tələbələri üçün texniki dəstək sistemi. İstifadəçi problemi qeydiyyata alır, IT mütəxəssisi müraciəti qəbul edib həll edir, istifadəçi isə statusu izləyir.

## Mündəricat
1. [Əsas imkanlar](#əsas-imkanlar)
2. [Texnologiyalar](#texnologiyalar)
3. [Quraşdırma](#quraşdırma)
4. [Test hesabları](#test-hesabları)
5. [Rollar və icazələr](#rollar-və-icazələr)
6. [Müraciətin həyat dövrü](#müraciətin-həyat-dövrü)
7. [Layihənin strukturu](#layihənin-strukturu)
8. [Komanda və branch-lər](#komanda-və-branch-lər)
9. [Tez-tez rast gəlinən problemlər](#tez-tez-rast-gəlinən-problemlər)
10. [Məhdudiyyətlər](#məhdudiyyətlər)

## Əsas imkanlar
- **Ana səhifə** — qısa təqdimat və müraciətlərin ümumi sayı ilə statuslara görə statistikası.
- **Müraciət yaratmaq** — başlıq, təsvir, kateqoriya (Hardware, Software, Network, Account, Other) və prioritet (Low, Medium, High, Critical).
- **Siyahı və filtr** — kateqoriyaya və statusa görə süzgəc.
- **Detal səhifəsi** — müraciətin bütün məlumatı, yaradan və məsul IT mütəxəssisi.
- **Status idarəsi** — yalnız IT mütəxəssisi statusu dəyişə bilir.
- **Təhlükəsizlik** — giriş/qeydiyyat, parolun hash-lənməsi, rol əsaslı icazə, server tərəfi validasiya, CSRF qoruması.

## Texnologiyalar
ASP.NET Core MVC (.NET 8) · Entity Framework Core · SQL Server · HTML, CSS, Bootstrap 5 · JavaScript

## Quraşdırma
**Tələblər:** [.NET 8 SDK](https://dotnet.microsoft.com/download), SQL Server (və ya Visual Studio ilə gələn LocalDB), Git.

```bash
git clone REPOSITORY_URL
cd HelpDesk-TeamB
```

1. `appsettings.json` faylında `ConnectionStrings:Default` dəyərini öz serverinizə uyğunlaşdırın. Standart dəyər LocalDB üçündür.
2. Layihəni işə salın:
   ```bash
   dotnet restore
   dotnet run
   ```
3. Terminalda yazılan ünvanı brauzerdə açın (məsələn, `https://localhost:5001`).

İlk işə salınmada baza avtomatik yaranır və nümunə məlumat əlavə olunur. İstəsəniz bazanı əl ilə `database.sql` ilə də yarada bilərsiniz; bu halda test hesablarının parolları tətbiq ilk dəfə açılanda avtomatik təyin olunur.

## Test hesabları
| Rol | Email | Parol |
|-----|-------|-------|
| IT mütəxəssisi | it@uni.az | It12345! |
| Tələbə | student@uni.az | Student123! |

Yeni qeydiyyatdan keçən hər istifadəçi **Student** rolunu alır.

## Rollar və icazələr
| Əməliyyat | Student / Staff | ITSpecialist / Admin |
|-----------|:---:|:---:|
| Müraciət yaratmaq | ✔ | ✔ |
| Öz müraciətlərinə baxmaq | ✔ | ✔ |
| Bütün müraciətlərə baxmaq | ✘ | ✔ |
| Statusu dəyişmək | ✘ | ✔ |

IT rolunu hələlik yalnız bazada `Users.Role` dəyərini dəyişməklə vermək olar.

## Müraciətin həyat dövrü
`New` → `InProgress` → `Resolved` → `Closed`

IT mütəxəssisi statusu `InProgress` etdikdə müraciət avtomatik ona təyin olunur.

## Layihənin strukturu
```
HelpDesk-TeamB/
├── Controllers/        Home, Account, Ticket
├── Models/             AppUser.cs, Ticket.cs
├── Data/               AppDbContext.cs, Seed.cs
├── Views/              Home, Ticket, Account, Shared
├── wwwroot/            css/, js/
├── docs/               GIT-WORKFLOW.md
├── database.sql        SQL sxemi, nümunə məlumat, statistika sorğuları
├── test-cases.md       Test ssenariləri və xəta siyahısı
├── appsettings.json    Konfiqurasiya
└── Program.cs          Başlanğıc nöqtəsi
```

## Komanda və branch-lər
Hər üzv öz branch-ində işləyir və Pull Request vasitəsilə `main`-ə göndərir. `main`-ə birbaşa push qadağandır. Ətraflı: [docs/GIT-WORKFLOW.md](docs/GIT-WORKFLOW.md).

| Vəzifə | Branch |
|--------|--------|
| Frontend Developer | `feature/frontend` |
| Backend Developer | `feature/backend` |
| Database Developer | `feature/database` |
| Security Developer | `feature/security` |
| QA Engineer | `feature/testing` |
| Documentation & DevOps | `feature/documentation` |

## Tez-tez rast gəlinən problemlər
| Problem | Həll |
|---------|------|
| Bazaya qoşulmur | Connection string-i və SQL Server-in işlədiyini yoxlayın |
| `dotnet` tanınmır | .NET 8 SDK quraşdırın, terminalı yenidən açın |
| HTTPS sertifikat xətası | `dotnet dev-certs https --trust` |
| Giriş işləmir | Email və parolu yoxlayın; test hesabları yuxarıdadır |

## Məhdudiyyətlər
Bu, böyük məhsul deyil, prototipdir. Şərh, fayl əlavəsi, səhifələmə, e-poçt bildirişi və admin paneli yoxdur.
