FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Healthmed.Appointment.API/Healthmed.Appointment.API.csproj", "Healthmed.Appointment.API/"]
COPY ["Healthmed.Appointment.Core/Healthmed.Appointment.Core.csproj", "Healthmed.Appointment.Core/"]
COPY ["Healthmed.Appointment.Infrastructure/Healthmed.Appointment.Infrastructure.csproj", "Healthmed.Appointment.Infrastructure/"]
RUN dotnet restore "Healthmed.Appointment.API/Healthmed.Appointment.API.csproj"
COPY . .
WORKDIR "/src/Healthmed.Appointment.API"
RUN dotnet build "Healthmed.Appointment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Healthmed.Appointment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Healthmed.Appointment.API.dll"]
