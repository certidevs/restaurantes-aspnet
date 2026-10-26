# Migraciones

> **Migración**: Archivo de C# que describe un cambio en la base de datos, como crear una tabla

Cuando cambiamos los modelos, la base de datos también tiene que cambiar. No tocamos las tablas a mano. En su lugar creamos una migración desde la terminal.

```powershell
dotnet ef migrations add CrearRestaurantes --output-dir Data/Migrations
dotnet ef database update
```

El primer comando crea el archivo de la migración. El segundo aplica los cambios a la base de datos.

La opción `--output-dir` indica la carpeta donde se guardan las migraciones. La ruta es relativa a la carpeta del proyecto. Si no la usamos, EF Core crea una carpeta `Migrations` en la raíz del proyecto, junto al archivo `.csproj`. Si ya hay migraciones en otra carpeta, EF Core sigue usando esa carpeta.

El comando responde algo así.

```text
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

El nombre describe el cambio, así que usamos algo como `CrearRestaurantes`. En la carpeta `Data/Migrations` aparecen archivos nuevos con la fecha y el nombre. El archivo `ApplicationDbContextModelSnapshot.cs` guarda cómo está el modelo ahora.

Hay dos reglas.

- No cambiamos los archivos de migración a mano
- Si nos equivocamos antes de aplicarla, borramos la migración con `dotnet ef migrations remove`

Cada cambio en los modelos necesita su propia migración.
