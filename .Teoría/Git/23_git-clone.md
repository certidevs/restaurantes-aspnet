# git clone

> **Clone**: Copia completa de un repositorio remoto en nuestro ordenador, con todos los archivos, todo el historial y todas las ramas

`git clone` es la forma de empezar a trabajar en un proyecto que ya existe. Seguimos estos pasos.

1. En GitHub pulsamos el botón verde **Code** y copiamos la dirección HTTPS
2. En la terminal nos situamos en la carpeta donde queremos el proyecto
3. Clonamos el repositorio con esa dirección

```powershell
cd C:\cdev
git clone https://github.com/tu-usuario/proyecto-aspnet.git
```

Git crea una carpeta con el nombre del repositorio y dentro deja todos los archivos. Entramos en ella para trabajar.

```powershell
cd proyecto-aspnet
```

El clone deja configurado el remoto `origin`, que apunta a GitHub. Lo comprobamos con `git remote -v`.

Si queremos que la carpeta tenga otro nombre, lo escribimos después de la dirección.

```powershell
git clone https://github.com/tu-usuario/proyecto-aspnet.git mi-proyecto
```
