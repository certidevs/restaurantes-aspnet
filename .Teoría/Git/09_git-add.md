# git add

`git add` pone los cambios en el área de preparación. Así los marcamos para que entren en el siguiente commit. Un cambio que no hemos añadido no se guarda al hacer commit.

Para añadir un archivo concreto usamos este comando.

```powershell
git add Views/Home/Index.cshtml
```

Para añadir todos los cambios de la carpeta actual usamos este otro.

```powershell
git add .
```

El punto significa "esta carpeta y todo lo que hay dentro". Git no añade los archivos que están en `.gitignore` pero conviene mirar `git status` antes de hacer commit para ver qué hemos preparado.

Si editamos un archivo después de añadirlo, volvemos a ejecutar `git add` sobre él. Git toma la versión que tenía el archivo en el momento del `git add`.
