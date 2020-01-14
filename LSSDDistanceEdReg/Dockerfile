FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LSSDDistanceEdReg.WebFrontEnd/LSSDDistanceEdReg.WebFrontEnd.csproj", "LSSDDistanceEdReg.WebFrontEnd/"]
COPY ["LSSDDistanceEdReg.Domain/LSSDDistanceEdReg.Domain.csproj", "LSSDDistanceEdReg.Domain/"]
COPY ["LSSDDistanceEdReg.Data/LSSDDistanceEdReg.Data.csproj", "LSSDDistanceEdReg.Data/"]
COPY ["LSSDDistanceEdReg.Util/LSSDDistanceEdReg.Util.csproj", "LSSDDistanceEdReg.Util/"]
RUN dotnet restore "LSSDDistanceEdReg.WebFrontEnd/LSSDDistanceEdReg.WebFrontEnd.csproj"
COPY . .
WORKDIR "/src/LSSDDistanceEdReg.WebFrontEnd"
RUN dotnet build "LSSDDistanceEdReg.WebFrontEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LSSDDistanceEdReg.WebFrontEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY LSSDDistanceEdReg.WebFrontEnd/Images/ /app/Images/
ENTRYPOINT ["dotnet", "LSSDDistanceEdReg.WebFrontEnd.dll"]