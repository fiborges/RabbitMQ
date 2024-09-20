FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

ARG DEVEXPRESS_NUGET_KEY
RUN dotnet nuget add source https://nuget.devexpress.com/api/v3/index.json -n DevExpress -u DevExpress -p ${DEVEXPRESS_NUGET_KEY} --store-password-in-clear-text

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /app/Reports ./Reports

ENTRYPOINT ["dotnet", "ReportGenerator.dll"]
