# git merge

> **Merge**: Unir los cambios de una rama con la rama en la que estamos

Normalmente usamos `merge` para llevar el trabajo de una rama de prueba a `main`. Primero nos cambiamos a la rama que va a recibir los cambios. Después hacemos el merge con el nombre de la otra rama.

```powershell
git switch main
git merge feature/login
```

Hay dos casos:

- **Fast-forward**: `main` no ha cambiado desde que creamos la rama, así que Git solo mueve su nombre hacia delante
- **Merge commit**: Las dos ramas tienen commits distintos, así que Git crea un commit nuevo que une ambos. Su mensaje suele ser `Merge branch 'feature/login'`.

Así se ve el caso fast-forward.

```text
Updating 9c1e0d4..3b8f2a1
Fast-forward
```

Si Git no puede unir los cambios de forma automática, aparece un conflicto. Antes de hacer un merge conviene tener la rama principal actualizada.
