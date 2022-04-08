


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.2-buster-slim
WORKDIR /app
EXPOSE 80

ENV GOOGLE_APPLICATION_CREDENTIALS=/app/kikker-accelerate-d2a8b9c315f4.json

#COPY app/publish .
COPY bin/Release/netcoreapp3.1/publish/ .
ENTRYPOINT ["dotnet", "FirestoreSample.dll"]