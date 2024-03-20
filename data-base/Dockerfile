
FROM mcr.microsoft.com/mssql/server:2017-latest

ENV SA_PASSWORD ClienteSenha@247
ENV ACCEPT_EULA Y

EXPOSE 1432

WORKDIR /usr/src/app

COPY ./sql-scripts/ /usr/src/app/
CMD /opt/mssql/bin/sqlservr

