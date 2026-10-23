# Deshacer commits con revert y reset

Hay dos formas de deshacer un commit que ya existe. El que tengamos que usar depende de si el commit ya está en GitHub.

## git revert

> **Revert**: Comando que crea un commit nuevo para deshacer los cambios de otro commit, sin tocar el historial

Usamos `git revert` cuando el commit ya está subido a GitHub. Es la opción segura, porque no cambia lo que otras personas ya tienen. Un ejemplo es este.

```powershell
git revert 3b8f2a1
```

Git abre el editor para que escribamos el mensaje del nuevo commit. Si no queremos escribirlo, añadimos `--no-edit`.

## git reset

> **Reset**: Comando que mueve la rama a un commit anterior, de modo que los commits que quedan por delante dejan de aparecer en el historial

Usamos `git reset` para cambios que solo existen en nuestro ordenador. Tiene tres modos:

- `--soft`: Mueve la rama pero deja los cambios preparados
- `--mixed`: Mueve la rama y deja los cambios sin preparar, que es el modo por defecto
- `--hard`: Mueve la rama y borra los cambios de los archivos

> **HEAD**: Referencia a la rama o al commit en el que estamos

Para deshacer el último commit y conservar sus cambios sin preparar, escribimos este comando.

```powershell
git reset HEAD~1
```

`HEAD~1` significa "el commit anterior al actual".

`--hard` borra los cambios de los archivos sin preguntar. Lo usamos solo si estamos seguros de que no necesitamos esos cambios.

```powershell
git reset --hard HEAD~1
```
