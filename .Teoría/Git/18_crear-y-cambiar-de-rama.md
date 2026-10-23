# Crear y cambiar de rama

Para crear una rama nueva y cambiarnos a ella en un solo paso usamos este comando.

```powershell
git switch -c feature/login
```

La opción `-c` viene de "create". El nombre `feature/login` es una convención habitual, y la barra separa el tipo de trabajo del tema. El nombre puede ser cualquiera sin espacios pero conviene que sea corto y claro.

Si solo queremos cambiarnos a una rama que ya existe, quitamos la `-c`.

```powershell
git switch main
```

Muchos tutoriales antiguos usan `git checkout -b feature/login`, que hace lo mismo. Nosotros usamos `git switch`, porque `git checkout` sirve para varias cosas distintas.

Antes de cambiar de rama, nos aseguramos de que no tenemos cambios sin guardar que puedan chocar con la otra rama. Si chocan, Git no nos deja cambiar. En ese caso hacemos commit o usamos `git stash`.

Cuando terminamos el trabajo en una rama y ya está unida a `main`, la borramos con este comando.

```powershell
git branch -d feature/login
```
