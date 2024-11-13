FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY media-api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -o /app
RUN dotnet publish -o /publish

# FROM ${BASE_IMAGE}
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

COPY --from=build /publish /app
WORKDIR /app
# ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 8080
CMD ["./media-api"]
