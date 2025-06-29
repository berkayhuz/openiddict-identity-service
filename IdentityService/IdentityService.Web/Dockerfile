# =========================
# 📦 Dockerfile - IdentityService
# =========================

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["IdentityService.Web/IdentityService.Web.csproj", "IdentityService.Web/"]
RUN dotnet restore "IdentityService.Web/IdentityService.Web.csproj"

COPY . .
WORKDIR "/src/IdentityService.Web"
RUN dotnet publish "IdentityService.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 443
EXPOSE 80

ENTRYPOINT ["dotnet", "IdentityService.Web.dll"]