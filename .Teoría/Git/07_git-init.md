# git init

> **Repositorio**: Carpeta que Git vigila y que guarda su historial dentro de una carpeta oculta llamada `.git`

`git init` convierte una carpeta en un repositorio. Entramos en la carpeta del proyecto y ejecutamos estos comandos.

```powershell
cd C:\cdev\proyecto-aspnet
git init
```

Git responde algo así.

```text
Initialized empty Git repository in C:/cdev/proyecto-aspnet/.git/
```

Lo hacemos una sola vez por proyecto. Si lo ejecutamos en una carpeta que ya es un repositorio, Git no borra nada.

Por defecto, Git crea una rama llamada `master`. GitHub usa `main`. Si queremos que el repositorio nuevo empiece con `main`, usamos este comando.

```powershell
git init -b main
```

Si ya tenemos un repositorio con `master`, le cambiamos el nombre con `git branch -M main`.
