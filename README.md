# Tickets

#Ubicarse en la carpeta: Tickets/backend

dotnet new sln --name Tickets.Solution
mkdir src
dotnet new classlib -o src/Core/Domain --name Tickets.Domain
dotnet new classlib -o src/Core/Application --name Tickets.Application
dotnet new classlib -o src/Infrastructure --name Tickets.Infrastructure
dotnet new webapi -o src/Api --name Tickets.Api

# agregar referencias a la solucion

dotnet sln add src/Infrastructure/Tickets.Infrastructure.csproj
dotnet sln add src/Core/Domain/Tickets.Domain.csproj
dotnet sln add src/Core/Application/Tickets.Application.csproj
dotnet sln add src/Api/Tickets.Api.csproj

# agregar referencias entre proyectos

dotnet add src/Infrastructure/Tickets.Infrastructure.csproj reference src/Core/Application/Tickets.Application.csproj
dotnet add src/Core/Application/Tickets.Application.csproj reference src/Core/Domain/Tickets.Domain.csproj
dotnet add src/Api/Tickets.Api.csproj reference src/Core/Application/Tickets.Application.csproj
dotnet add src/Api/Tickets.Api.csproj reference src/Infrastructure/Tickets.Infrastructure.csproj

#dominio dev

http://boletixapidev.somee.com
http://www.boletixapidev.somee.com


Addresses:	ftp://boletixapidev.somee.com/www.boletixapidev.somee.com
ftp://198.37.116.30/www.boletixapidev.somee.com
Username:	hbeltrang
Pwd: Somee.11@9


SQL Server version:	MS SQL 2016 Express
SQL Server address:	Tickets.mssql.somee.com
Login name:	hbeltrang_SQLLogin_1
pwd: haw7b3wwwt
conenction string: workstation id=Tickets.mssql.somee.com;packet size=4096;user id=hbeltrang_SQLLogin_1;pwd=haw7b3wwwt;data source=Tickets.mssql.somee.com;persist security info=False;initial catalog=Tickets

