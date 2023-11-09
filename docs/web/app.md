# App

Es la función base de nuestra aplicación. En esta realizamos la instancia de rutas y demás servicios. Es importante resaltar que tambien realizamos el lazy loading para la carga adecuada de las promesas.

```ts
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
      // element: <ProtectRoutes>
      //   <Layout />,
      // </ProtectRoutes>,
      element: <Layout/>,
      children: [
        {
          path: "/",
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
      path: "/login",
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

export default App
```
