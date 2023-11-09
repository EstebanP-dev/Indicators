import React, { Suspense } from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { Provider } from "react-redux";
import { SnackbarProvider } from "notistack";
import { SnackbarUtilsConfigurator } from "./utilities/index.ts";
import store from "./redux/store.ts";
import { Loading } from "./components/index.ts";
import { AxiosInterceptor } from "./interceptors/index.ts";

AxiosInterceptor();

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <SnackbarProvider autoHideDuration={5000}>
      <SnackbarUtilsConfigurator />
      <Suspense
        fallback={
          <Loading
            canCancel={false}
            cancelTitle={undefined}
            message="Cargando"
          />
        }
      >
        <Provider store={store}>
          <App />
        </Provider>
      </Suspense>
    </SnackbarProvider>
  </React.StrictMode>
);
