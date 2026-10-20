# ============================================================
#  Kovan – Docker imajı (Render gibi sunuculara yüklemek için)
#  1. aşama: .NET 8 SDK ile projeyi derle ve yayınla
#  2. aşama: sadece çalışma zamanı (ASP.NET Core) ile küçük imaj
# ============================================================

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Kovan/Kovan.csproj Kovan/
RUN dotnet restore Kovan/Kovan.csproj
COPY . .
RUN dotnet publish Kovan/Kovan.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
# Render, uygulamanın dinleyeceği portu PORT ortam değişkeniyle verir (yoksa 8080)
CMD ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} dotnet Kovan.dll"]
