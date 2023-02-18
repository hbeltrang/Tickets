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
