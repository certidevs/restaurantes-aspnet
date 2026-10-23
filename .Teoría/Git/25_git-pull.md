# git pull

`git pull` trae los cambios del remoto y los une con nuestra rama actual. Es lo primero que hacemos al empezar a trabajar, para tener la última versión del proyecto.

```powershell
git pull
```

Por dentro, `git pull` hace dos cosas seguidas. Primero descarga los cambios con `git fetch`, y después los une con un merge. Si el remoto no tiene nada nuevo, no pasa nada.

Algunas versiones de Git avisan la primera vez que hacemos pull y piden elegir cómo unir los cambios. Si nos sale ese aviso, escribimos esta línea para usar merge, que es la opción más fácil de entender.

```powershell
git config --global pull.rebase false
```

Si los cambios del remoto tocan archivos que nosotros hemos modificado sin guardar, Git no hace el pull y avisa de que nuestros cambios se sobrescribirían. En ese caso hacemos commit o guardamos los cambios con `git stash` y después volvemos a hacer pull.
