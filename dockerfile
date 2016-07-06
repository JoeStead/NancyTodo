FROM mono:4.4.0

RUN mkdir -p /usr/src/app/source /usr/src/app/build
WORKDIR /usr/src/app/source

COPY . /usr/src/app/source
RUN nuget restore -NonInteractive TodoApi.sln
RUN xbuild /property:Configuration=Release /property:OutDir=/usr/src/app/build/
WORKDIR /usr/src/app/build

ENV LANG en_US.UTF-8

EXPOSE 8080

CMD [ "mono", "./TodoApi.exe"]
