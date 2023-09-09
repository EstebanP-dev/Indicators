# Enviroments

En esta carpeta estarán las variables estaticas o constantes que necesitemos en todo el proyecto.

## Enviroment

Esta constante contiene las variables de entorno del proyecto.

```ts
export const enviroment = {
    production: false,
    enviroment: "local",
    api: "http://localhost:5233/v1/api"
}
```

## Endpoints

Esta constante contiene los endpoints de la API a consumir para obtener datos y realizar peticiones al servidor.

```ts
export const endpoints =
{
    auth:
    {
        login: "/auth/login"
    },
    users:
    {
        getUsersPagination: (page: number, rows: number) => `/users?page=${page}&rows=${rows}`,
        getUserById: '/users/%s',
    },
    displays:
    {
        getUsersPagination: (page: number, rows: number, exclude: string | null) =>
            `/displays?page=${page}&rows=${rows}${exclude === null
                ? ''
                : '&exclude=' + exclude}`,
        getUserById: (id: number) => `/displays/${id}`,
    }
}
```
