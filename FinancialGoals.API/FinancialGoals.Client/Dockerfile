FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FinancialGoals.Client/FinancialGoals.Client.csproj", "FinancialGoals.Client/"]
RUN dotnet restore "FinancialGoals.Client/FinancialGoals.Client.csproj"
COPY . .
WORKDIR "/src/FinancialGoals.Client"
RUN dotnet build "FinancialGoals.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialGoals.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialGoals.Client.dll"]
