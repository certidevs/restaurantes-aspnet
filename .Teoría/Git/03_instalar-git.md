# Instalar Git

Git se instala de forma distinta según el sistema operativo. La comprobación final es la misma en todos.

## Windows

Descargamos el instalador desde [git-scm.com/download/win](https://git-scm.com/download/win). Es el paquete oficial, que se llama **Git for Windows**. Lo ejecutamos y, en la mayoría de pantallas, dejamos las opciones que vienen por defecto. Una pantalla pregunta qué editor de texto usar para escribir los mensajes de los commits. Si usamos VS Code, lo elegimos en esa lista.

El instalador también añade **Git Bash**, una terminal con comandos de Git. Git Bash es opcional, porque los comandos también funcionan en PowerShell.

## macOS

Abrimos la terminal y escribimos `git --version`. Si Git no está instalado, macOS nos ofrece instalar las herramientas de línea de comandos, que incluyen Git. También podemos descargar el instalador desde [git-scm.com/download/mac](https://git-scm.com/download/mac).

## Linux

En Ubuntu o Debian abrimos la terminal y escribimos estos comandos.

```bash
sudo apt update
sudo apt install git
```

En Fedora o similares usamos `sudo dnf install git`. Si nuestra distribución es otra, las instrucciones están en [git-scm.com/download/linux](https://git-scm.com/download/linux).

## Comprobar la instalación

En cualquier sistema abrimos una terminal y escribimos este comando.

```powershell
git --version
```

Si aparece un número de versión, como `git version 2.x.x`, Git ya está instalado.
