# Utilities

En esta carpeta almacenamos métodos globales que nos pueden servir para multiples casos de uso.

## Load Abort

Este método permite cancelar las peticiones cuando sea requerido.

```ts
export const loadAbort = () => {
    const controller = new AbortController();
    return controller;
};
```

## Snackbar Utilities

Este método permite lanzar alertas snackbar en las vistas.

```ts
let useSnackbarRef: WithSnackbarProps;
export const SnackbarUtilsConfigurator: React.FC = () => {
  useSnackbarRef = useSnackbar();
  return null;
};

export const SnackbarUtilities = {
  success(msg: string) {
    this.toast(msg, 'success');
  },
  warning(msg: string) {
    this.toast(msg, 'warning');
  },
  info(msg: string) {
    this.toast(msg, 'info');
  },
  error(msg: string) {
    this.toast(msg, 'error');
  },
  toast(msg: string, variant: VariantType = 'default') {
    useSnackbarRef.enqueueSnackbar(msg, { variant });
  }
};
```
