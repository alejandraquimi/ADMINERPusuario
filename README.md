Este es un proyecto donde se usó lo siguiente:
Para el `backend` se usó c# .net 7 y base de datos SQL SERVER en su última versión.
Para el `frontend` se uso javascript, html, css3 React.js v18 con redux para el manejo del estado.
El uso de redux lo use básicamente en la autenticación del usuario, para guardar la información del usuario con el fin de que todos los componentes tengan acceso, así mismo para el manejo de estado con el uso del store, action, reducer, dispatch y selector.

## Comencemos

Ejecución del frontend:

En el frontend se encuentra un `.env` donde se debe poner la ruta de nuestro servidor backend y también para poder cambiar el puerto que en el que se desea ejecutar el frontend.

```bash
cd frontend
npm install
npm start
```

Ejecución del backend:
Antes de ejecutar el comando se debe modificar lo siguiente:
`appsettings.json` en este archivo se debe modificar la información de la base de datos como su Server, Database, el usuario y contraseña si lo desea o por una autenticación de windows que no necesito de estos dos últimos campo. Además que debe colocar DevelopmentIp que es la url del frontend.
Luego de seguir lo pasos previos ejecutamos lo siguiente:

```bash
cd backend
dotnet restore
dotnet ef migrations add GenerarMigraciones
dotnet ef database
dotnet run
```

## Entre las funciones implementadas son:

- Creación de un login, para ingresar el usuario por defecto sera el administrador donde las claves son correo: `admin@hotmail.com` contraseña: `admin`. La autenticacion es realizada con `jwt` y al ingresar un nuevo usuario su contraseña sera codificada.
- Al ingresar al home de mi aplicacion web se tendra una navbar con varias opciones.
- La primera opción es la creación de usuario y la segunda opción será la edición de usuario.
- La tercera opción es la consulta y búsqueda de usuarios tipo reporte, donde se tendrá en esta tabla la agregación de contactos y la eliminación de usuarios. Para la agregación de contactos se tuvo las siguientes validaciones: no se puede agregar un mismo usuario a tus contactos más de una vez y no te puedes añadir como contacto a ti mismo. Para eliminar a usuarios se inactivo al usuario, se cambió el campo EstadoRegistro a inactivo, cabe mencionar que la búsqueda de usuarios solo va a traer a usuarios activos.
- Como última opción se tiene la visualización de tus contactos.

## Preguntas

### ¿Cómo decidió las opciones técnicas y arquitectónicas utilizadas como parte de su solución?

En el backend está desarrollado con el patrón MVC, con el cual ya he trabajado antes y me gusta principalmente por la separación de responsabilidades ya que esta divido en modelos, vistas y controladores donde cada uno es responsable de una tarea. Además, la escalabilidad y reutilización de código que brinda.
En el frontend al implementar react se usó una arquitectura basada en componentes, es decir se crean varios componentes cada uno con una función con el fin de construir componentes más grandes.

### ¿Hay alguna mejora que pueda hacer en su envío?

- La implementación de la paginación del lado del servidor backend para luego implementarla en el frontend.
- Implementación de cookies en las solicitudes Http, por lo que no se requiere algún trabajo del lado del cliente. Las cuales son gestionadas por el servidor y el cliente. Tienen una mayor seguridad tiene atributos seguros y HttpOnly que evitan atacas XSS.
- Implementar material ui para la facilidad de implementar código html y brinda una mejor interfaz ux en poco tiempo de desarrollo.

### ¿Qué haría de manera diferente si se le asignara más tiempo?

- La autenticación a pesar de que use jwt del lado del servidor. En el frontend la almacene un localstorage por lo que no es seguro. Utilizaría cookies.
- Aunque no se solicitó manejo de roles. Hubiera creado una tabla de roles, donde según los roles tenga acceso a ciertas solicitudes.
- La interfaz UX la mejoraría.
- Como último punto, en estas últimas horas he investigado que se puede implementar react ClientApp dentro de un proyecto de c# .net, me pareció algo interesante y lo implementaría con el fin de obtener más conocimientos sobre el tema. Además, he leído que tiene algunos beneficios como comunicación eficiente, desarrollo rápido, la seguridad, despliegue sencillo.

## Imágenes de mi proyecto:
