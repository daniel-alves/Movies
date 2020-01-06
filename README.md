Excluir a base
--------------
dotnet ef database drop -c MovieContext -s ..\Movies.App

Adicionar migrações
-------------------
dotnet ef migrations add MIGRATION_NAME -c MovieContext -s ..\Movies.App -o .\Data\Migrations

Rodar as migrações manualmente
------------------------------
dotnet ef database update -c MovieContext -s ..\Movies.App 
