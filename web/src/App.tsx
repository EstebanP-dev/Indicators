import {
  createBrowserRouter,
  RouterProvider
} from "react-router-dom";
import "./styles/global.scss"
import { SnackbarProvider } from "notistack";
import { SnackbarUtilsConfigurator } from "./utilities";
import { Suspense, lazy } from "react";
import { Provider } from "react-redux";
import store from "./redux/store";
import { ProtectedRoute } from "./components";
import { Home, Login, Roles, Users } from "./pages";
import { PrivateRoutes, PublicRoutes } from "./enviroments";
import Layout from "./pages/Layout/Layout";

const DisplayList = lazy(() => import("./pages/Displays/List/DisplayList"));

const App = () => {
  const router = createBrowserRouter([
    {
      path: "/",
      element:<ProtectedRoute>
        <Layout />
      </ProtectedRoute>,
      children: [
        {
          path: PrivateRoutes.HOME,
          element: <Home />
        },
        {
          path: "/users",
          element: <Users />
        },
        {
          path: "/roles",
          element: <Roles/>
        },
        {
          path: "/displays",
          element: <DisplayList />
        }
      ]
    },
    {
      path: PublicRoutes.LOGIN,
      element: <Login />
    }
  ]);

  return (
    <SnackbarProvider autoHideDuration={5000}>
      <SnackbarUtilsConfigurator />
      <Suspense fallback={<div>Loading...</div>}>
        <Provider store={store}>
          <RouterProvider router={router} />
        </Provider>
      </Suspense>
    </SnackbarProvider>
  )
}

export default App;

