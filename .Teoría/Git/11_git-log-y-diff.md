# git log y git diff

`git log` muestra el historial de commits, del más reciente al más antiguo. Para verlo de forma resumida usamos este comando.

```powershell
git log --oneline
```

Vemos una línea por commit con su código corto y su mensaje.

```text
3b8f2a1 Añade la página de contacto
9c1e0d4 Crea el proyecto base
```

Para ver los cambios línea a línea de un commit usamos `git show` seguido del código.

```powershell
git show 3b8f2a1
```

`git diff` muestra los cambios que todavía no hemos guardado en un commit. Si hemos editado un archivo y queremos revisar lo que tocamos antes de hacer commit, escribimos este comando.

```powershell
git diff
```

Las líneas que empiezan por `-` se quitaron, y las que empiezan por `+` se añadieron. Si ya hicimos `git add` sobre los cambios, usamos `git diff --staged` para verlos.
