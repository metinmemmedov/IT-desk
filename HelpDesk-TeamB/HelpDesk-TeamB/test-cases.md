# Test ssenariləri (QA)

| ID | Ssenari | Addımlar | Gözlənilən nəticə | Nəticə |
|----|---------|----------|-------------------|--------|
| TC-01 | Uğurlu qeydiyyat | /Account/Register, düzgün məlumat | Login-ə yönləndirmə, rol Student | ☐ |
| TC-02 | Zəif parol | Parol `abc` | "ən azı 8 simvol" xətası | ☐ |
| TC-03 | Yanlış giriş | Yanlış parol | "Email və ya parol yanlışdır" | ☐ |
| TC-04 | Müraciət yaratmaq | Giriş → Yeni → bütün sahələr | Detallar səhifəsi, status New | ☐ |
| TC-05 | Qısa başlıq | Başlıq 2 simvol | Validasiya xətası, qeyd olunmur | ☐ |
| TC-06 | Status dəyişmə (IT) | it@uni.az → müraciət → In Progress | Status yenilənir, məsul IT təyin olunur | ☐ |
| TC-07 | Status icazəsi | Tələbə hesabı ilə POST /Ticket/ChangeStatus | 403 / Login-ə yönləndirmə | ☐ |
| TC-08 | Başqasının müraciəti | Tələbə başqa istifadəçinin Details/ID-sinə girir | 403 | ☐ |
| TC-09 | Filtr | Kateqoriya=Network + Status=New | Yalnız uyğun sətirlər | ☐ |
| TC-10 | Statistika | Ana səhifə | Ümumi say = statusların cəmi | ☐ |
| TC-11 | XSS | Başlıq: `<script>alert(1)</script>` | Mətn kimi göstərilir | ☐ |
| TC-12 | Loginsiz giriş | /Ticket | /Account/Login-ə yönləndirmə | ☐ |

## Aşkar edilmiş xətalar
QA testləri icra etdikcə burada doldurur (ID, addımlar, gözlənilən/faktiki nəticə, ciddilik).

## Məlum məhdudiyyətlər
- IT rolu yalnız bazada dəyişməklə verilir (admin paneli yoxdur).
- Şərh, fayl əlavəsi və səhifələmə yoxdur.
