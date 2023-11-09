# Axios Call

Este modelo mapea el llamado al servicios de la libreria [Axios](https://axios-http.com/docs/intro).

```ts
export interface AxiosCall<T> {
    call: Promise<AxiosResponse<T>>;
    controller?: AbortController;
}
```
