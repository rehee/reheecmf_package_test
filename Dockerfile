FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY release .

ENTRYPOINT ["dotnet", "ReheeCmfPackageTest.dll"]
