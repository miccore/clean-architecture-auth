FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app

COPY . .
WORKDIR /app/src/Miccore.CleanArchitecture.Auth.Api
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
 
#Copy in files from the build
COPY --from=build /out .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "Miccore.CleanArchitecture.Auth.Api.dll"]