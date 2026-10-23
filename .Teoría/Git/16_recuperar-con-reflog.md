# Recuperar con reflog

> **Reflog**: Registro local de los movimientos de `HEAD` y de las ramas, que solo existe en nuestro ordenador y no se sube a GitHub

Git guarda un registro de todo lo que hacemos con las ramas y con `HEAD`. Gracias a él podemos recuperar commits que parecían perdidos después de un `git reset`.

Para verlo escribimos este comando.

```powershell
git reflog
```

Vemos una lista parecida a esta.

```text
3f9a1c2 HEAD@{0}: reset: moving to HEAD~1
8b2d4e7 HEAD@{1}: commit: Añade la página de contacto
```

Cada línea es un movimiento. El código de la izquierda es el commit al que apuntaba `HEAD` en ese momento. Si queremos volver al commit `8b2d4e7`, escribimos este comando.

```powershell
git reset --hard 8b2d4e7
```

El reflog no recupera cambios que nunca llegamos a guardar en un commit. Si borramos un archivo con `git restore` sin haberlo guardado, ese trabajo no aparece en el reflog.
