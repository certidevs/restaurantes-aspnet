# Deshacer cambios sin commit

Si cambiamos un archivo y queremos volver a como estaba en el último commit, usamos este comando.

```powershell
git restore Views/Home/Index.cshtml
```

Este comando descarta todos los cambios de ese archivo, y no se puede deshacer. Los cambios que no están en un commit no se pueden recuperar después. Por eso, antes de ejecutarlo, miramos los cambios con `git diff`.

Si ya hicimos `git add` sobre un archivo y queremos quitarlo del área de preparación sin perder los cambios, usamos este otro.

```powershell
git restore --staged Views/Home/Index.cshtml
```

El archivo sigue modificado en la carpeta pero ya no está preparado para el siguiente commit.
