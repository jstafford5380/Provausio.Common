FROM microsoft/dotnet:2.2-sdk
WORKDIR /build

ARG build_number
ARG nuget_key

COPY . /build

RUN dotnet restore src
RUN dotnet build src -c Release /p:BuildNumber=${build_number}
RUN dotnet test src
RUN dotnet pack src/Provausio.Common/Provausio.Common.csproj
RUN dotnet nuget push src/**/bin/Release/**.nupkg --source https://api.nuget.org/v3/index.json --api-key ${nuget_key}