# 🔐 OpenIddict Identity Service

Thanks for your interest in this project!  
This is a secure authentication microservice built with **OpenIddict**, **ASP.NET Core Identity**, **EF Core**, and **FluentValidation** — designed for production-ready OAuth2 / OpenID Connect scenarios.

---

## 📦 Technologies Used

- ASP.NET Core 9
- OpenIddict (Access & Refresh Tokens, PKCE, Scopes)  
- ASP.NET Identity with `AppUser`  
- Entity Framework Core (SQL Server)  
- FluentValidation  
- Swagger UI (OAuth2 password flow)  
- Rate Limiting Middleware  
- Clean architecture ready  

---

## 🚀 Project Structure

```
src/
├── Controllers/             # API Endpoints (Token, Auth)
├── Entities/                # AppUser model
├── Features/
│   ├── DTOs/                # Output models (e.g. UserInfoDto)
│   ├── Requests/            # Input models
│   ├── Validators/          # FluentValidation rules
│   ├── Mappers/             # AutoMapper profiles
├── Middlewares/             # Rate limiting
├── Persistence/             # DbContext & OpenIddictSeeder
├── Config/                  # SwaggerDefaults, etc.
Program.cs                   # App startup and pipeline
```

---

## ⚙️ Configuration

### 🔑 Default OpenIddict Client

- Client ID: `authservice-client`  
- Client Secret: `C78A11D0-4D8A-4B61-BB20-F6D937C6B7F4`  
- Grant Types: password, refresh_token  
- Scopes: openid, profile, email, offline_access, api  

---

## 🧪 API Endpoints

| Method | Endpoint                          | Description                           |
|--------|-----------------------------------|---------------------------------------|
| POST   | `/connect/token`                  | Get access & refresh token            |
| POST   | `/connect/register`               | Register a new user                   |
| GET    | `/connect/confirm-email`          | Confirm user email                    |
| POST   | `/connect/resend-confirmation`    | Resend email confirmation             |
| POST   | `/connect/logout`                 | Logout user                           |
| POST   | `/connect/password-reset-request` | Request password reset                |
| POST   | `/connect/password-reset-confirm` | Confirm password reset                |
| GET    | `/connect/user-info`              | Get current user info (auth required) |
| POST   | `/connect/update-user-info`       | Update user info (auth required)      |
| POST   | `/connect/change-password`        | Change user password (auth required)  |
| POST   | `/connect/change-email`           | Request email change (auth required)  |
| GET    | `/connect/confirm-email-change`   | Confirm email change                  |

---

## ▶️ Run the Project

### 📌 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)  
- SQL Server (LocalDB or Docker)  
- Visual Studio / VS Code  

### 🖥️ Run Locally

```bash
git clone https://github.com/berkayhuz/openiddict-identity-service.git
cd openiddict-identity-service
dotnet restore
dotnet ef database update --project IdentityService.Web
dotnet run --project IdentityService.Web
```

Visit Swagger UI: https://localhost:5001/swagger

### 🐳 Run with Docker

```bash
docker-compose up --build
```

---

## 🔒 Security Notes

- Access token lifetime: 15 minutes  
- Refresh token lifetime: 7 days  
- Rate limits:  
  - Authenticated: 30 requests/minute  
  - Unauthenticated: 10 requests/minute  
- Ephemeral signing keys used in dev — replace with persistent keys in production  
- PKCE & Proof Key required  

---

## 🧱 Future Improvements

- Add client credentials grant  
- External login providers (Google, Microsoft)  
- SMTP email delivery  
- Redis and background queue integration  
- Audit logs and admin UI  

---

## 📄 License

MIT © 2025 Berkay Huz

---

## 🤝 Contributing

Pull requests and feedback are welcome!

```bash
git checkout -b feature/my-awesome-feature
```

---

## ✨ Author

Made with ❤️ by **Berkay Huz**  
GitHub: [github.com/berkayhuz](https://github.com/berkayhuz)
