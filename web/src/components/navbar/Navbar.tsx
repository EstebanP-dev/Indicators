import "./navbar.scss"

export const Navbar = () => {
  return (
    <div className="navbar">
      <div className="logo">
        <img src="logo.svg" alt="logo" />
        <span>Indicators Admin</span>
      </div>
      <div className="icons">
        <img src="/search.svg" alt="search icon" className="icon" />
        <img src="/app.svg" alt="app icon" className="icon" />
        <img src="/expand.svg" alt="expand icon" className="icon" />
        <div className="notification">
          <img src="/notifications.svg" alt="notifications icon" />
          <span>1</span>
        </div>
        <div className="user">
          <img src="https://images.pexels.com/photos/2586823/pexels-photo-2586823.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" alt="profile picture" />
          <span>Esteban</span>
        </div>
        <img src="/settings.svg" alt="settings icon" className="icon" />
      </div>
    </div>
  )
}

export default Navbar