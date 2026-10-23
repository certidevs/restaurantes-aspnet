# git stash

> **Stash**: Lugar temporal donde guardamos los cambios sin commit, para dejar la carpeta limpia

`git stash` guarda nuestros cambios sin commit y deja la carpeta como estaba en el último commit. Lo usamos cuando estamos a medias con algo y necesitamos cambiar de rama o hacer un pull urgente.

Estos son los comandos que más usamos:

- `git stash`: Guarda los cambios
- `git stash list`: Muestra lo que tenemos guardado
- `git stash pop`: Aplica los cambios guardados y los saca de la lista
- `git stash apply`: Aplica los cambios pero los deja en la lista
- `git stash push -m "login a medias"`: Guarda los cambios con una descripción para reconocerlos después
- `git stash -u`: Guarda también los archivos nuevos que Git todavía no sigue

Git responde algo así al guardar.

```text
Saved working directory and index state WIP on main: 3b8f2a1 Añade la página de contacto
```
