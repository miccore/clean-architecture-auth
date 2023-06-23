FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app

COPY ./src/Miccore.CleanArchitecture.Auth.Api/bin/Release/net6.0/publish/. .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "Miccore.CleanArchitecture.Auth.Api.dll"]
