# Remoto y autenticación

> **Remoto**: Copia del repositorio que está en otro sitio, normalmente en GitHub, con la que sincronizamos nuestra copia usando `git push` y `git pull`

Por convención, el remoto principal se llama `origin`. Para verlo escribimos este comando.

```powershell
git remote -v
```

Si el repositorio viene de un `git clone`, `origin` ya está configurado. Si lo creamos con `git init`, lo añadimos nosotros. Primero creamos un repositorio vacío en GitHub y después usamos este comando.

```powershell
git remote add origin https://github.com/tu-usuario/proyecto-aspnet.git
```

## Autenticación

Cuando Git sube algo a GitHub, necesita saber quiénes somos. GitHub no acepta la contraseña de la cuenta para estas operaciones. En su lugar usamos **Git Credential Manager**, que viene incluido con Git for Windows.

La primera vez que hacemos push o clone desde una dirección `https://`, Git abre el navegador para que iniciemos sesión en GitHub. Después guarda las credenciales en el Administrador de credenciales de Windows, así que no hace falta entrar cada vez.

Si las credenciales dejan de funcionar, abrimos el Administrador de credenciales de Windows, buscamos la entrada de GitHub y la borramos. Git nos pedirá iniciar sesión otra vez.
