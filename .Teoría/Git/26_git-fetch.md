# git fetch

`git fetch` descarga los cambios del remoto pero no los une a nuestro trabajo. Nuestros archivos no cambian. Solo se actualiza la información sobre lo que hay en GitHub. Por ejemplo, `origin/main` es como nuestro ordenador ve la rama `main` de GitHub desde el último fetch.

```powershell
git fetch
```

Después podemos mirar qué ha llegado antes de decidir nada. Para ver los commits de `main` en GitHub que todavía no tenemos escribimos este comando.

```powershell
git log main..origin/main --oneline
```

Para ver los cambios en los archivos usamos este otro.

```powershell
git diff main origin/main
```

Cuando lo hayamos revisado, unimos los cambios con `git merge origin/main`. También podemos usar `git pull`, que hace fetch y merge en un solo paso.

`fetch` sirve para saber qué ha cambiado sin tocar nuestro trabajo actual. Si estamos en una rama con cambios a medias, conviene hacer fetch primero.
