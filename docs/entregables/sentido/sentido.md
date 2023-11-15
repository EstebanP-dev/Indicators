# Sentido

## Diagramas

### Diagrama de Caso de Uso

![Diagrama de Caso de Uso](./hu.png)

### Diagrama de Secuencia

![Diagrama de Secuencia](./secuencia.png)

## Historias de Usuario

### Listar

| **Caso de Uso** | Listar Variables |
|---|---|
| **Variables** | Usuario, Base de datos SisIndicadores |
| **Tipo** | Inclusión |
| **Propósito** | Listar todos los Variables disponibles en el sistema de indicadores para su administración y consulta. |
| **Resumen** | Este caso de uso se activa cuando el Usuario requiere ver la lista completa de Variables. Permite al usuario visualizar una lista ordenada y posiblemente filtrada de Variables registrados en el sistema. |
| **Precondiciones** | El Usuario debe estar autenticado y tener permisos para acceder a la lista de Variables. |
| **Flujo Principal** | El Usuario accede a la sección "Variables" en la interfaz de usuario (UI-1). Se le presenta una lista de Variables disponibles en el sistema. El usuario puede seleccionar un Sentido para ver más detalles o realizar acciones adicionales. |
| **Subflujos** | Desde la lista de Variables, el usuario puede optar por ver detalles de un Sentido específico (Detalle), actualizar información de un Sentido (Actualizar), o crear un nuevo Sentido (Crear). Puede volver a esta lista en cualquier momento para realizar más operaciones. |
| **Excepciones** | Si la lista no puede ser generada o está vacía, se muestra el mensaje "The meaning list could not be retrieved. Try again later." Si el Usuario no tiene permisos, se muestra "The operation was cancelled." |
---

### Detalle

| **Caso de Uso** | Detalle de Sentido |
|---|---|
| **Variables** | Usuario, Base de datos SisIndicadores |
| **Tipo** | Inclusión |
| **Propósito** | Proporcionar al Usuario una visualización detallada de la información de un Sentido específico dentro del sistema de indicadores. |
| **Resumen** | Este caso de uso comienza cuando el Usuario selecciona un Sentido específico de la lista para ver en detalle. El sistema muestra una página con toda la información detallada del Sentido seleccionado. |
| **Precondiciones** | El Usuario debe estar autenticado y tener permisos para acceder a los detalles de un Sentido. |
| **Flujo Principal** | En la interfaz de gestión de Variables (UI-1), el Usuario selecciona un Sentido de la lista.  |
| **Subflujos** | Ninguno|
| **Excepciones** | Si el Sentido no existe o no se encuentra, se muestra el mensaje "The meaning was not found." Si ocurre un error al intentar mostrar los detalles, se muestra "An error occurred while processing the request. Try to contact the support team." |
---

### Crear

| **Caso de Uso** | Crear Sentido |
|---|---|
| **Variables** | Usuario, Base de datos SisIndicadores |
| **Tipo** | Inclusión |
| **Propósito** | Permitir al Usuario agregar un nuevo Sentido al sistema de indicadores. |
| **Resumen** | Este caso de uso se inicia cuando el Usuario necesita ingresar un nuevo Sentido al sistema. El Usuario proporciona los detalles necesarios para registrar un nuevo Sentido y lo añade al sistema a través de una interfaz de usuario. |
| **Precondiciones** | El Usuario debe estar autenticado y tener permisos para añadir nuevos Variables al sistema. |
| **Flujo Principal** | El Usuario navega a la opción "Agregar Nuevo" en la interfaz de usuario (UI-1). Completa el formulario con la información del nuevo Sentido y selecciona la opción "Guardar" para crear el registro. |
| **Subflujos** | Después de la creación, el Usuario puede ser redirigido a la lista de Variables para confirmar que el nuevo Sentido se ha agregado correctamente o para continuar con la creación de otros Variables. |
| **Excepciones** | Si los detalles proporcionados son insuficientes o incorrectos, se muestra el mensaje "cannot be empty." Si el Sentido ya existe, se muestra "The meaning already exists." En caso de un error inesperado durante la creación, se muestra "Something was wrong. Try again later." |
---

### Actualizar

| **Caso de Uso** | Actualizar Sentido |
|---|---|
| **Variables** | Usuario, Base de datos SisIndicadores |
| **Tipo** | Inclusión |
| **Propósito** | Habilitar al Usuario para modificar la información de un Sentido existente dentro del sistema de indicadores. |
| **Resumen** | Este caso de uso ocurre cuando un Usuario necesita cambiar los datos de un Sentido. El Usuario selecciona un Sentido específico de la lista y actualiza la información necesaria a través de un formulario. |
| **Precondiciones** | El Usuario debe estar autenticado y tener los permisos necesarios para editar la información de un Sentido. |
| **Flujo Principal** | Desde la página de gestión de Sentido (P-1), el Usuario selecciona un Sentido de la lista. Doble click en el campo que desea modificar y reemplaza el valor. Presiona el icono de "guardar". El sistema valida y actualiza el valor. |
| **Subflujos** | Una vez actualizado el Sentido, el Usuario puede volver a la lista de Variables para ver los cambios realizados o para actualizar otros Variables. |
| **Excepciones** | Si el Sentido no se encuentra para la actualización, se muestra "The meaning was not found." Si hay discrepancias en los datos proporcionados, se muestra "The value does not coincide with." Si se produce un fallo al guardar los cambios, se muestra "Something was wrong. Try again later." |
---
