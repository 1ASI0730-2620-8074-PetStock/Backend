FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY PetStock.API/*.csproj PetStock.API/
RUN dotnet restore ./PetStock.API
COPY . .
RUN dotnet publish ./PetStock.API -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "PetStock.API.dll"]
