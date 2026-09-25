# Етап 1: Збірка проєкту (використовуємо SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо csproj та відновлюємо залежності (це кешується Docker'ом для швидкості)
COPY ["DevDialogue/DevDialogue.csproj", "DevDialogue/"]
RUN dotnet restore "DevDialogue/DevDialogue.csproj"

# Копіюємо решту вихідного коду та публікуємо проєкт
COPY . .
WORKDIR "/src/DevDialogue"
RUN dotnet publish "DevDialogue.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Етап 2: Запуск (використовуємо легкий ASP.NET runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render за замовчуванням спрямовує трафік, тому вказуємо стандартний порт
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Запускаємо збудовану dll
ENTRYPOINT ["dotnet", "DevDialogue.dll"]