# 🤝 Contributing to OpenIddict Identity Service

Thanks for your interest in contributing to this project!
I appreciate every contribution, whether it's a bug fix, feature idea, or pull request.

---

## 📦 Project Setup

Before contributing, make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- Docker (optional, for local SQL Server)
- Visual Studio or VS Code

### 🖥️ Local Development

```bash
git clone https://github.com/berkayhuz/openiddict-identity-service.git
cd openiddict-identity-service
dotnet restore
dotnet ef database update --project IdentityService.Web
dotnet run --project IdentityService.Web
```

Swagger UI: https://localhost:5001/swagger

---

## 🚀 Contributing Guidelines

### 📁 Code Style

- I use C# 11 or higher syntax where appropriate.
- I follow the existing folder structure and naming conventions.
- I always use braces `{ }`, even for single-line if/else statements.
- I prefer record classes for request/response models.

### 📏 Validation

- All request models should be validated using FluentValidation.
- I add validators under `Features/Validators/`.

### 🧪 Tests

- (Not implemented yet – but coming soon!)
- I plan to include unit/integration tests under `/tests`.

---

## 🛠️ How to Contribute

1. Fork this repository.
2. Create a new branch:
```bash
git checkout -b feature/my-awesome-feature
```
3. Make changes, then commit:
```bash
git commit -m "Add: My awesome feature"
```
4. Push to your fork:
```bash
git push origin feature/my-awesome-feature
```
5. Open a pull request targeting the `main` branch.

### ✅ Pull Request Checklist

- Code builds and runs successfully.
- No unused `using` directives or variables.
- No debug or commented-out code.
- Validators/mappers added if applicable.
- Followed documentation/comment style for new classes.

---

## 📮 Need Help?

Feel free to open an issue, start a discussion, or reach out to me on GitHub.
I'm happy to help you get started or review your ideas.

Thanks for helping improve this project! 💙