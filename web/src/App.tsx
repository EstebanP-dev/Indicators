import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/global.scss";
import { lazy, useMemo } from "react";
import { ProtectedRoute } from "./components";
import { Home, Login, Roles, Users } from "./pages";
import { PrivateRoutes, PublicRoutes } from "./enviroments";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useSelector } from "react-redux";
import { themeSettings } from "./themes";
import { AppStore } from "./redux/store";
import { Layout } from "./scenes";

const DisplayList = lazy(() => import("./pages/Displays"));

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
          path: "/displays",
          element: <DisplayList />,
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
