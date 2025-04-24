# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
EXPOSE 8080
EXPOSE 8081
ARG TARGETARCH
COPY ./app/src/api-cadastro/. /source/api-cadastro
COPY  ./app/src/artefatos/. /source/artefatos
COPY ./app/src/observabilidade/ObservabilidadeAspire/ObservabilidadeAspire.ServiceDefaults/. /source/observabilidade/ObservabilidadeAspire/ObservabilidadeAspire.ServiceDefaults
WORKDIR /source

RUN dotnet build api-cadastro/Api.Cadastro/Api.Cadastro/Api.Cadastro.csproj
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish api-cadastro/Api.Cadastro/Api.Cadastro/Api.Cadastro.csproj  -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
COPY ./app/src/api-cadastro/Api.Cadastro /source
COPY  ./app/src/artefatos /source/artefatos
WORKDIR /source
CMD dotnet run --no-launch-profile

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
# Instalar tzdata e configurar o timezone para Brasilia
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime \
    && echo "America/Sao_Paulo" > /etc/timezone


# Definir a variável de ambiente para o .NET seguir o timezone do sistema
ENV TZ=America/Sao_Paulo
WORKDIR /app
COPY --from=build /app .
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser
USER appuser
ENTRYPOINT ["dotnet", "Api.Cadastro.dll"]