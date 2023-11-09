import { useSelector } from "react-redux";
import { AppStore } from "../redux/store";
import { Box, useMediaQuery } from "@mui/material";
import { Outlet } from "react-router-dom";
import { Navbar, Sidebar } from ".";
import { useState } from "react";
import { AccountInfo } from "../models";

const Layout = () => {
  const isNonMobile = useMediaQuery("(min-width: 600px)");

  const accountInfo: AccountInfo = useSelector(
    (store: AppStore) => store.accountInfo
  );
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);

  return (
    <Box
      display={isNonMobile ? "flex" : "block"}
    >
      <Sidebar
        accountInfo={accountInfo}
        isNonMobile={isNonMobile}
        drawerWidth="250px"
        isSidebarOpen={isSidebarOpen}
        setIsSidebarOpen={setIsSidebarOpen}
      />
      <Box flexGrow={1} height="100%" width="100%">
        <Navbar
          accountInfo={accountInfo}
          isSidebarOpen={isSidebarOpen}
          setIsSidebarOpen={setIsSidebarOpen}
        />
        <Outlet />
      </Box>
    </Box>
  );
};

export default Layout;
