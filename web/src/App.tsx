import { Footer, Home, Menu, Navbar } from "./shared";
import { Login } from "./auth"
import {
  createBrowserRouter,
  RouterProvider,
  Outlet,
} from "react-router-dom";
import { Users } from "./users";
import { Roles } from "./roles";
import "./styles/global.scss"

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
      // element: <ProtectRoutes>
      //   <Layout />,
      // </ProtectRoutes>,
      element: <Layout/>,
      children: [
        {
          path: "/",
          element: <Home/>
        },
        {
          path: "/users",
          element: <Users />
        },
        {
          path: "/roles",
          element: <Roles/>
        },
      ]
    },
    {
      path: "/login",
      element: <Login />
    }
  ]);

  return (
    <RouterProvider router={router} />
  )
}

export default App
