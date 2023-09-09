# Displays

Este archivo contiene todos los llamados al api para el modelo **Display** (represenvisual).

```ts
export const getDisplaysPagination = (page: number, rows: number) =>{
    const controller = loadAbort();

    return {
        call: axios.get<Pagination<Display>>(
            enviroment.api + endpoints.displays.getUsersPagination(page, rows, null),
            {
                signal: controller.signal
            }),
        controller: controller
    }
}
```
