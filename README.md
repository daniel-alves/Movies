# movies
 
- para rodar é necessário alterar a string de conexão no arquivo Movies.App/appsettings.json

- instalar a ferramenta dotnet-ef globalmente na versão 3.0.0

- para rodar os comandos do dotnet-ef é necessário estar dentro da pasta Movies.Infra

Excluir a base
--------------
dotnet ef database drop -c MovieContext -s ..\Movies.App

Adicionar migrações
-------------------
dotnet ef migrations add MIGRATION_NAME -c MovieContext -s ..\Movies.App -o .\Data\Migrations

Rodar as migrações manualmente
------------------------------
dotnet ef database update -c MovieContext -s ..\Movies.App 

Endereço da aplicação públicada na Azure
----------------------------------------
https://moviesapp20191208034407.azurewebsites.net/

# OBS
- Todas as classes possuem interface mesmo sem necessidade aparente para facilitar na hora de criar os testes.
