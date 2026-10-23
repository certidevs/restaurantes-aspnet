# Conflictos de merge

> **Conflicto**: Situación en la que Git no puede unir dos cambios de forma automática, porque las dos ramas modificaron la misma parte de un mismo archivo

Git no sabe qué versión queremos, así que nos pide que elijamos. El merge queda a medias hasta que lo resolvamos.

Cuando pasa, Git muestra un mensaje como este.

```text
Auto-merging Views/Shared/_Layout.cshtml
CONFLICT (content): Merge conflict in Views/Shared/_Layout.cshtml
Automatic merge failed; fix conflicts and then commit the result.
```

Abrimos el archivo en VS Code. Git marca la zona en conflicto así.

```text
<<<<<<< HEAD
<a href="/">Inicio</a>
=======
<a href="/">Página principal</a>
>>>>>>> feature/login
```

La parte de arriba es nuestra versión, la de la rama en la que estamos. La parte de abajo es la otra rama.

Para resolverlo seguimos estos pasos.

1. Decidimos qué texto debe quedar
2. Borramos las líneas de marcas y dejamos solo ese texto
3. Marcamos el archivo como resuelto y terminamos el merge con estos comandos

```powershell
git add Views/Shared/_Layout.cshtml
git commit
```

Después de resolverlo, el archivo queda así.

```text
<a href="/">Página principal</a>
```

Si queremos cancelar el merge por completo, usamos `git merge --abort`. Así volvemos al estado de antes del merge.
