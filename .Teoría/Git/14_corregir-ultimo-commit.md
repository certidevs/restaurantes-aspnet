# Corregir el último commit

A veces hacemos un commit y nos damos cuenta de que se nos olvidó un archivo, o de que el mensaje tiene una errata. Para corregirlo sin crear un commit nuevo usamos `--amend`.

Si solo queremos cambiar el mensaje, escribimos este comando.

```powershell
git commit --amend -m "Añade la página de contacto y su enlace"
```

Si nos olvidamos de un archivo, lo añadimos y corregimos el commit manteniendo el mensaje anterior. Lo hacemos con estos dos comandos.

```powershell
git add Views/Shared/_Layout.cshtml
git commit --amend --no-edit
```

La opción `--no-edit` mantiene el mensaje que ya tenía el commit.

`--amend` no añade un commit nuevo sino que reemplaza el último. Eso cambia el historial. Lo usamos solo en commits que todavía no hemos subido a GitHub. Si ya están subidos, otras personas pueden tener esa versión, y sincronizar se complica.
