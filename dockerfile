# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiamos el archivo de proyecto y restauramos las dependencias
COPY freak-store.csproj ./
RUN dotnet restore

# Copiamos el resto de los archivos de la aplicación
COPY . ./

# Publicamos la aplicación en modo Release
RUN dotnet publish freak-store.csproj -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos los archivos generados en la etapa de construcción
COPY --from=build-env /app/out .

# Establecemos el nombre de la aplicación principal
ENV APP_NET_CORE freak-store.dll 

# Definimos el comando para ejecutar la aplicación
CMD ASPNETCORE_URLS=http://0.0.0.0:$PORT dotnet $APP_NET_CORE
