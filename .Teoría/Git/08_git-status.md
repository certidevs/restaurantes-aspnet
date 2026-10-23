# git status

`git status` muestra en qué estado están nuestros archivos. Este comando se suele usar mucho. No cambia nada, solo informa.

```powershell
git status
```

Si no hemos cambiado nada desde el último commit, vemos algo así.

```text
On branch main
nothing to commit, working tree clean
```

Si modificamos un archivo, aparece así.

```text
Changes not staged for commit:
  modified:   Views/Home/Index.cshtml
```

Si creamos un archivo nuevo que Git todavía no sigue, aparece en otra lista.

```text
Untracked files:
  Teoria/
```

Git también indica qué comando conviene usar en cada caso. Leemos su respuesta antes de actuar.
