import {
  createBrowserRouter,
  RouterProvider,
  Outlet,
} from "react-router-dom";
import "./styles/global.scss"
import { SnackbarProvider } from "notistack";
import { SnackbarUtilsConfigurator } from "./utilities";
import { Suspense, lazy } from "react";
import { Provider } from "react-redux";
import store from "./redux/store";
import { Footer, Menu, Navbar, ProtectedRoute } from "./components";
import { Home, Login, Roles, Users } from "./pages";
import { PrivateRoutes, PublicRoutes } from "./enviroments";

const DisplayList = lazy(() => import("./pages/Displays/List/DisplayList"))
const DisplayDetails = lazy(() => import("./pages/Displays/Details/DisplayDetails"))

function App() {
  const Layout = () => {
    return (
      <div className="main">
        <div className="navbar">
          <Navbar />
        </div>
        <div className="container">
          <div className="menuContainer">
            <Menu/>
          </div>
          <div className="contentContainer">
            <Outlet />
          </div>
        </div>
        <div className="footer">
          <Footer/>
        </div>
      </div>
    )
  }

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
        },
        {
          path: "/displays/:id",
          element: <DisplayDetails />
        }
      ]
    },
    {
      path: PublicRoutes.LOGIN,
      element: <Login />
    }
  ]);

  return (
    <SnackbarProvider>
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

