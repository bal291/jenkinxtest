FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY subjectmicroservice/subjectmicroservice.csproj subjectmicroservice/
RUN dotnet restore subjectmicroservice/subjectmicroservice.csproj
COPY . .
WORKDIR /src/subjectmicroservice
RUN dotnet build subjectmicroservice.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish subjectmicroservice.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "subjectmicroservice.dll"]
