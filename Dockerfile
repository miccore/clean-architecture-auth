FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY ./src/Miccore.CleanArchitecture.Auth.Api/bin/Release/net6.0/publish/. .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "Miccore.CleanArchitecture.Auth.Api.dll"]
