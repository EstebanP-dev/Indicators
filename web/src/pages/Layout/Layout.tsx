import { useSelector } from "react-redux";
import { AppStore } from "../../redux/store";
import { Footer, Loading, Menu, Navbar } from "../../components";
import { Outlet } from "react-router-dom";

const Layout = () => {
    const loadingData: boolean = useSelector((store: AppStore) => store.loadingData);
    return (
        <>
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
        {
            loadingData
            ? <Loading canCancel={false} cancelTitle={undefined} message="Cargando" />
            : <></>
        }
        </>
    )
}

export default Layout;