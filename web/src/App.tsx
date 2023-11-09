import { createBrowserRouter, Navigate, RouterProvider } from "react-router-dom";
import "./styles/global.scss";
import { lazy, useMemo } from "react";
import { ProtectedRoute } from "./components";
import { PrivateRoutes, PublicRoutes } from "./enviroments";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useSelector } from "react-redux";
import { themeSettings } from "./themes";
import { AppStore } from "./redux/store";
import { Layout } from "./scenes";

const Indicators = lazy(() => import('./pages/Indicators/Indicators'));
const Indicator = lazy(() => import('./pages/Indicators/Indicator'));
const Login = lazy(() => import('./pages/Login'));
const Users = lazy(() => import('./pages/Users/Users'));
const User = lazy(() => import('./pages/Users/User'));
const Displays = lazy(() => import('./pages/Displays'));
const ActorTypes = lazy(() => import('./pages/ActorTypes'));
const IndicatorTypes = lazy(() => import('./pages/IndicatorTypes'));
const Sections = lazy(() => import('./pages/Sections'));
const SubSections = lazy(() => import('./pages/SubSections'));
const Meanings = lazy(() => import('./pages/Meanings'));
const Roles = lazy(() => import('./pages/Roles'));
const MeasurementUnits = lazy(() => import('./pages/MeasurementUnits'));

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
          element: <Indicators />,
        },
        {
          path: PrivateRoutes.INDICATOR,
          element: <Indicator />,
        },
        {
          path: PrivateRoutes.USERS,
          element: <Users />,
        },
        {
          path:  PrivateRoutes.USER,
          element: <User />
        },
        {
          path: PrivateRoutes.ROLES,
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
        {
          path: PrivateRoutes.SECTIONS,
          element: <Sections />,
        },
        {
          path: PrivateRoutes.SUBSECTIONS,
          element: <SubSections />,
        },
        {
          path: PrivateRoutes.MEANINGS,
          element: <Meanings />,
        },
        {
          path: PrivateRoutes.MEASUREMENTUNITS,
          element: <MeasurementUnits />,
        },
      ],
    },
    {
      path: PublicRoutes.LOGIN,
      element: <Login />,
    },
    {
      path: "*",
      element: <Navigate to={{pathname: PrivateRoutes.HOME}} />
    }
  ]);

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <RouterProvider router={router} />
    </ThemeProvider>
  );
};

export default App;
