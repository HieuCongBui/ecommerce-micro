# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution và các project file để restore
COPY *.sln .
COPY eTicket.V8.API/*.csproj ./eTicket.V8.API/
COPY eTicket.V8.Core/*.csproj ./eTicket.V8.Core/
COPY eTicket.V8.Data/*.csproj ./eTicket.V8.Data/
COPY eTicket.V8.Services/*.csproj ./eTicket.V8.Services/
COPY eTicket.V8.Domain/*.csproj ./eTicket.V8.Domain/

# Restore các packages
RUN dotnet restore

# Copy toàn bộ source code
COPY . .

# Build và publish project
WORKDIR /src/eTicket.V8.API
RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy kết quả publish từ stage trước
COPY --from=build /app/publish .

# Mở cổng 80 cho ứng dụng
EXPOSE 80

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "eTicket.V8.API.dll"]
