# User Management – ASP.NET Core MVC + Identity

## Projekt célja

A projekt egy felhasználókezelő (User Management) rendszer megvalósítása ASP.NET Core MVC és Identity segítségével.

A rendszer lehetővé teszi:

* felhasználók regisztrációját
* bejelentkezését
* profil szerkesztését
* szerepkörök (Role) kezelését
* adminisztrátori felhasználó létrehozását

---

# Használt technológiák

* ASP.NET Core MVC
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* Razor Pages
* Bootstrap

---

# Projekt felépítése

```
Controllers/
Data/
Models/
Views/
Areas/
Migrations/
wwwroot/
```

---

# 1. ApplicationUser

Az IdentityUser osztály kibővítésre került.

```csharp
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int UsernameChangeLimit { get; set; } = 10;
    public byte[]? ProfilePicture { get; set; }
}
```

---

# 2. ApplicationDbContext

```csharp
public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>
{
}
```

---

# 3. Migráció

Migráció létrehozása:

```powershell
Add-Migration AddApplicationUserFields
```

Adatbázis frissítése:

```powershell
Update-Database
```

---

# 4. Regisztráció módosítása

A Register oldalon új mezők kerültek hozzáadásra:

* First Name
* Last Name

A regisztráció során ezek az adatok bekerülnek az ApplicationUser objektumba.

---

# 5. Profil szerkesztése

A Profile (Manage/Index) oldalon szerkeszthető:

* Username
* First Name
* Last Name
* Phone Number
* Profile Picture

---

# 6. UserRolesController

A projekt tartalmaz egy UserRolesController osztályt.

Főbb Action-ök:

## Index()

Felhasználók listázása.

## Manage()

Felhasználó szerepköreinek megjelenítése.

## Manage (POST)

Felhasználó szerepköreinek módosítása.

---

# 7. ViewModel-ek

## UserRolesViewModel

Tulajdonságok:

* UserId
* Email
* FirstName
* LastName
* Roles

## ManageUserRolesViewModel

Tulajdonságok:

* RoleId
* RoleName
* Selected

---

# 8. Jogosultságkezelés

A Controller csak megfelelő szerepkörrel érhető el.

```csharp
[Authorize(Roles = "SuperAdmin,Admin")]
```

---

# 9. Szerepkörök

A rendszer az alábbi szerepköröket használja:

* SuperAdmin
* Admin
* Moderator
* Basic

---

# 10. Profilkép

A profilkép byte[] formában kerül eltárolásra az adatbázisban.

Feltöltése az Index.cshtml oldalon történik.

---

# 11. Főbb fájlok

```
Data/
    ApplicationUser.cs
    ApplicationDbContext.cs

Controllers/
    UserRolesController.cs

Models/
    UserRolesViewModel.cs
    ManageUserRolesViewModel.cs

Areas/
    Identity/
        Pages/
            Account/
                Register.cshtml
                Register.cshtml.cs

            Manage/
                Index.cshtml
                Index.cshtml.cs
```

---

# Használt Identity szolgáltatások

* UserManager<ApplicationUser>
* SignInManager<ApplicationUser>
* RoleManager<IdentityRole>

---

# Adatbázis

Az Identity automatikusan létrehozza az alábbi táblákat:

* AspNetUsers
* AspNetRoles
* AspNetUserRoles
* AspNetUserClaims
* AspNetRoleClaims
* AspNetUserTokens
* AspNetUserLogins

---

# Fő funkciók

✅ Regisztráció

✅ Bejelentkezés

✅ Profil módosítása

✅ Telefonszám módosítása

✅ Profilkép feltöltése

✅ Szerepkörök kezelése

✅ Admin felület

✅ User lista

---

# Indítás

1. Nyisd meg a projektet Visual Studio-ban.
2. Futtasd:

```powershell
Update-Database
```

3. Indítsd el az alkalmazást.
4. Regisztrálj egy felhasználót.
5. Jelentkezz be.
6. Nyisd meg a Profile oldalt.
7. Admin jogosultsággal használd a User Management felületet.

---

# Továbbfejlesztési lehetőségek

* Profilkép megjelenítése
* Jelszó visszaállítás
* E-mail megerősítés
* Kétfaktoros hitelesítés (2FA)
* Felhasználók keresése
* Lapozás (Pagination)
* Audit naplózás
* Soft Delete
* Felhasználók zárolása (Lockout)
