# .gitignore

> **.gitignore**: Archivo que indica a Git qué archivos y carpetas debe ignorar, para que no aparezcan en `git status` ni se suban al repositorio

Hay archivos que nunca queremos subir. Un ejemplo son las carpetas `bin` y `obj`, que se crean al compilar. Se vuelven a generar en cualquier momento, así que no tiene sentido guardarlas. Otro ejemplo son las bases de datos locales y los archivos con contraseñas.

En este proyecto, el `.gitignore` está en la raíz y tiene líneas como estas.

```gitignore
# Build output
**/bin/
**/obj/

# ASP.NET local data and secrets
**/App_Data/*.db
secrets.json
appsettings.Local.json
```

Cada línea es un patrón. Estas son las reglas básicas:

- Una barra al final, como `bin/`, indica una carpeta
- El asterisco `*` significa "cualquier cosa", así que `*.log` ignora todos los archivos que terminan en `.log`
- Una línea que empieza por `#` es un comentario y Git la ignora

Eso sí, `.gitignore` solo afecta a los archivos que Git todavía no sigue. Si un archivo ya está en el repositorio, seguirá apareciendo aunque lo pongamos en `.gitignore`. Para dejar de seguirlo sin borrarlo del disco usamos este comando.

```powershell
git rm --cached secrets.json
```
