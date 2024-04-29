[Microservices YouTube Tutorial Walkthrough](https://www.youtube.com/watch?v=Z4bINJudHX8&list=PL6tu16kXT9PrlCX-b1o0WdBc56rXHJXLy) 

- The tutorial describes how to create the project on a Windows Machine and the steps work for a .
- The Project on this repo was ran on MacOS using:
    1. Visual Studio Code
    2. SQL Server (mssql) Extension on Visual Studio Code
    3. Docker image - mcr.microsoft.com/mssql/server:latest (The AMD can run on an ARM Mac machine)
        - The aspnetwork option on running docker wasn't used
        ```
        docker run --rm -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<PASSWORD>" -p 1433:1433 --name sqlserver -h sqlserver  -it mcr.microsoft.com/mssql/server:latest
        ```
