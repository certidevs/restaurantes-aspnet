# Las tres zonas de Git

Para entender los comandos de Git hay que conocer tres zonas. Los cambios pasan por ellas en este orden:

1. **Directorio de trabajo**: Los archivos de la carpeta tal como los editamos, antes de guardarlos en Git
2. **Área de preparación** (*staging area*): Donde marcamos los cambios que queremos incluir en el siguiente commit
3. **Historial**: Los commits ya guardados, que ya no cambian

El flujo normal sigue este orden.

**Directorio de trabajo** -> `git add` -> **Área de preparación** -> `git commit` -> **Historial**

Un archivo puede estar en dos zonas a la vez. Si lo modificamos después de añadirlo con `git add`, la versión preparada y la del directorio de trabajo ya no coinciden. Por eso Git lo muestra en dos listas.
