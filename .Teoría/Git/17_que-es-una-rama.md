# Qué es una rama

> **Rama** (*branch*): Línea de trabajo separada dentro del mismo repositorio

Con una rama podemos probar algo nuevo sin tocar la versión principal del proyecto. Cuando el trabajo está listo, lo unimos a la rama principal.

Por dentro, una rama es un nombre que apunta a un commit. Cuando hacemos un commit nuevo en esa rama, el nombre se mueve hasta el commit nuevo.

La rama principal suele llamarse `main`. Para ver las ramas que tenemos usamos este comando.

```powershell
git branch
```

Vemos algo así. El asterisco marca la rama en la que estamos.

```text
* main
  feature/login
```

Para saber en qué rama estamos, miramos también la primera línea de `git status`.
