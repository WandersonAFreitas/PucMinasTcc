FROM microsoft/dotnet:2.1-aspnetcore-runtime

RUN apt-get update -y && apt-get install -y libgdiplus lsof && apt-get install -y libc6-dev
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        zlib1g \
        fontconfig \
        libfreetype6 \
        libx11-6 \
        libxext6 \
        libxrender1 \
    && curl -o /usr/lib/libwkhtmltox.so \
        --location \
        https://github.com/rdvojmoc/DinkToPdf/raw/v1.0.8/v0.12.4/64%20bit/libwkhtmltox.so

WORKDIR /app
COPY app .
	
ENTRYPOINT ["dotnet", "WebAPI.dll"]