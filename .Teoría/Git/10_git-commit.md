# git commit

> **Commit**: Versión guardada del proyecto, con un mensaje que explica el cambio y un identificador único

Un commit guarda los cambios que hemos preparado con `git add`. Lo hacemos con estos dos comandos.

```powershell
git add .
git commit -m "Añade la página de contacto"
```

La opción `-m` escribe el mensaje en la misma línea. Si no la ponemos, Git abre el editor para que escribamos el mensaje.

Git responde algo así.

```text
[main 3b8f2a1] Añade la página de contacto
 2 files changed, 18 insertions(+)
```

El código `3b8f2a1` es el identificador del commit. Lo usamos mucho al leer el historial.

Escribimos mensajes cortos y claros. "Corrige el título del login" dice más que "cambios" o "arreglos varios". Además, cada commit debería reunir cambios relacionados entre sí. Si mezclamos muchas cosas en uno, luego cuesta entender qué pasó.
