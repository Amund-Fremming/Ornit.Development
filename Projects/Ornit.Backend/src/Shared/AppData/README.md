# EF Core Migrations

1. Install EF Core Tools
```bash
dotnet tool install --global dotnet-ef
```
2. Add new migration
```bash
dotnet ef migrations add <MigrationName>
```
3. Update migration 
```bash
dotnet ef database update
```
