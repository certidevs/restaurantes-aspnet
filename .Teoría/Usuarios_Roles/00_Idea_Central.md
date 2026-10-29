# Un solo tipo de usuario

> **Usuario**: Persona que entra en la aplicación con su email y su contraseña

En una aplicación con login, todas las personas que entran son el mismo tipo de cosa: un usuario. Un cliente, un administrador o un empleado se guardan igual, con su email, su nombre de login y su contraseña cifrada.

Lo que cambia entre unos y otros no es la clase, es el **rol**. El rol dice qué puede hacer cada persona.

Hay tres piezas que trabajan juntas.

- **La cuenta**: una fila por persona, en la tabla de usuarios. Es lo que usa el login
- **El rol**: una etiqueta como `Admin` o `User`. Decide qué acciones puede ejecutar
- **Lo que hace cada persona**: reseñas, pedidos, reservas. Son entidades normales con una clave que apunta a la cuenta

Así funciona en el proyecto de restaurantes. Cada persona tiene una sola cuenta, y esa cuenta tiene uno o varios roles:

- Un administrador tiene el rol `Admin` y puede gestionar restaurantes y usuarios
- Un cliente tiene el rol `User` y puede dejar reseñas y hacer pedidos
- Las reseñas (`Review`) y los pedidos (`Order`) guardan el `Id` de la cuenta que los hizo

**¿Y si hubiera una clase `Cliente` y otra `Administrador`?**

- Cada clase repetiría el email y la contraseña. Dos sitios donde guardar lo mismo se acaban desincronizando
- Identity solo sabe hacer login con la clase de usuario. Tendríamos que escribir nosotros las contraseñas, las cookies y los bloqueos
- Si una persona pasa de cliente a administrador, habría que mover sus datos de una tabla a otra. Con un rol basta con cambiar una etiqueta
- Una persona puede ser cliente y administradora a la vez. Con clases separadas eso no cabe
