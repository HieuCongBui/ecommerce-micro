# ---------- Build Stage ----------
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /src
    
    # Copy solution và các project file để restore
    COPY *.sln .
    COPY AuthService.API/*.csproj ./AuthService.API/
    COPY AuthService.Application/*.csproj ./AuthService.Application/
    COPY AuthService.Infrastructure/*.csproj ./AuthService.Infrastructure/
    COPY AuthService.Domain/*.csproj ./AuthService.Domain/
    COPY AuthService.Intergration.Test/*.csproj ./AuthService.Intergration.Test/
    
    # Restore các packages
    RUN dotnet restore
    
    # Copy toàn bộ source code
    COPY . .
    
    # Build và publish project
    WORKDIR /src/AuthService.API
    RUN dotnet publish -c Release -o /app/publish
    
    # ---------- Runtime Stage ----------
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
    WORKDIR /app
    
    # Set ASP.NET Core environment to listen on port 80
    ENV ASPNETCORE_URLS=http://0.0.0.0:80

    # Copy kết quả publish từ stage trước
    COPY --from=build /app/publish .
    
    # Mở cổng 80 cho ứng dụng
    EXPOSE 80
    
    # Chạy ứng dụng
    ENTRYPOINT ["dotnet", "AuthService.API.dll"]
    