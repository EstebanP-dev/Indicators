import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/global.scss";
import { lazy, useMemo } from "react";
import { ProtectedRoute } from "./components";
import { Home, Roles, Users } from "./pages";
import { PrivateRoutes, PublicRoutes } from "./enviroments";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useSelector } from "react-redux";
import { themeSettings } from "./themes";
import { AppStore } from "./redux/store";
import { Layout } from "./scenes";

const Login = lazy(() => import('./pages/Login'));
const Displays = lazy(() => import('./pages/Displays'));
const ActorTypes = lazy(() => import('./pages/ActorTypes'));
const IndicatorTypes = lazy(() => import('./pages/IndicatorTypes'));

const App = () => {
  const mode: string = useSelector((store: AppStore) => store.appTheme.mode);
  const theme = useMemo(() => createTheme(themeSettings(mode)), [mode]);

  const router = createBrowserRouter([
    {
      path: "/",
      element: (
        <ProtectedRoute>
          <Layout />
        </ProtectedRoute>
      ),
      children: [
        {
          path: PrivateRoutes.HOME,
          element: <Home />,
        },
        {
          path: "/users",
          element: <Users />,
        },
        {
          path: "/roles",
          element: <Roles />,
        },
        {
          path: PrivateRoutes.DISPLAYS,
          element: <Displays />,
        },
        {
          path: PrivateRoutes.ACTORTYPES,
          element: <ActorTypes />,
        },
        {
          path: PrivateRoutes.INDICATORTYPES,
          element: <IndicatorTypes />,
        },
      ],
    },
    {
      path: PublicRoutes.LOGIN,
      element: <Login />,
    },
  ]);

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <RouterProvider router={router} />
    </ThemeProvider>
  );
};

export default App;
