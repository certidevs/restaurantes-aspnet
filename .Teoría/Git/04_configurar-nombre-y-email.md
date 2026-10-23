# Configurar nombre y email

Cada commit guarda quién lo hizo. Para eso, Git necesita nuestro nombre y nuestro email. Lo configuramos una sola vez en el ordenador.

Escribimos estos dos comandos con nuestro nombre y el email de nuestra cuenta de GitHub.

```powershell
git config --global user.name "Tu Nombre"
git config --global user.email "tu-email@correo.com"
```

La opción `--global` hace que la configuración valga para todos los proyectos. Si la quitamos, la configuración solo valdrá para el repositorio en el que estemos.

Si queremos que Git use VS Code para escribir los mensajes de los commits, añadimos esta línea.

```powershell
git config --global core.editor "code --wait"
```

Para revisar la configuración usamos este comando.

```powershell
git config --list
```

Veremos una lista con todas las opciones. Buscamos `user.name` y `user.email` para comprobar que están bien.
