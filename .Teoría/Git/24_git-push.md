# git push

> **Push**: Subir nuestros commits al remoto, es decir, a GitHub

`git push` es la forma de compartir nuestro trabajo.

La primera vez que subimos una rama nueva usamos este comando.

```powershell
git push -u origin main
```

La opción `-u` le dice a Git que la rama local queda conectada con la rama `main` de `origin`. A partir de ahí, basta con escribir `git push`.

Si la rama ya estaba conectada, basta con este comando.

```powershell
git push
```

Puede que Git rechace el push con un mensaje como este.

```text
! [rejected]        main -> main (fetch first)
```

Significa que en GitHub hay commits que nosotros no tenemos. Alguien los subió antes o los subimos nosotros desde otro ordenador. Primero los traemos con `git pull` y después volvemos a hacer push.
