import { useLocation, useNavigate } from "react-router-dom";
import { AccountInfo } from "../models";
import React, { useEffect, useState } from "react";
import {
  Box,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Typography,
  useTheme,
} from "@mui/material";

import {
  SettingsOutlined,
  ChevronLeft,
  ChevronRightOutlined,
  HomeOutlined,
} from "@mui/icons-material";
import { FlexBetween } from "../components";

const navItems = [
  {
    text: "Dashboard",
    icon: <HomeOutlined />,
    slug: "",
  },
  {
    text: "Estándares",
    icon: null,
    route: null,
    slug: "",
  },
  {
    text: "Visuales",
    icon: <HomeOutlined />,
    slug: "displays",
  },
  {
    text: "Tipo Actor",
    icon: <HomeOutlined />,
    slug: "actortypes",
  },
  {
    text: "Tipo Indicador",
    icon: <HomeOutlined />,
    slug: "indicatortypes",
  },
  {
    text: "Unidad de Medición",
    icon: <HomeOutlined />,
    slug: "measurementunits",
  },
  {
    text: "Texto",
    icon: null,
    slug: "",
  },
  {
    text: "Sentido",
    icon: <HomeOutlined />,
    slug: "meanings",
  },
  {
    text: "Sección",
    icon: <HomeOutlined />,
    slug: "sections",
  },
  {
    text: "Subsección",
    icon: <HomeOutlined />,
    slug: "subsections",
  },
  {
    text: "Interno",
    icon: null,
    slug: "",
  },
  {
    text: "Usuarios",
    icon: <HomeOutlined />,
    slug: "users",
  },
  {
    text: "Roles",
    icon: <HomeOutlined />,
    slug: "roles",
  },
];

type Props = {
  accountInfo: AccountInfo;
  drawerWidth: string;
  isSidebarOpen: boolean;
  setIsSidebarOpen: React.Dispatch<React.SetStateAction<boolean>>;
  isNonMobile: boolean;
};

const Sidebar = (props: Props) => {
  const { pathname } = useLocation();
  const [active, setActive] = useState("");
  const navigate = useNavigate();
  const theme = useTheme();

  useEffect(() => {
    setActive(pathname.substring(1));
  }, [pathname]);

  return (
    <Box component="nav">
      {props.isSidebarOpen && (
        <Drawer
          open={props.isSidebarOpen}
          onClose={() => props.setIsSidebarOpen(false)}
          variant="persistent"
          anchor="left"
          sx={{
            width: props.drawerWidth,
            "& .MuiDrawer-paper": {
              color: theme.palette.secondary.light,
              backgroundColor: theme.palette.background.paper,
              boxSizing: "border-box",
              borderWidth: props.isNonMobile ? 0 : "2px",
              width: props.drawerWidth,
            },
          }}
        >
          <Box width="100%">
            <Box m="1.5rem 2rem 2rem 3rem">
              <FlexBetween color={theme.palette.secondary.main}>
                <Box display="flex" alignItems="center" gap="0.5rem">
                  <Typography variant="h4" fontWeight="bold">
                    USBMED
                  </Typography>
                </Box>
                {!props.isNonMobile && (
                  <IconButton
                    onClick={() => props.setIsSidebarOpen(!props.isSidebarOpen)}
                  >
                    <ChevronLeft />
                  </IconButton>
                )}
              </FlexBetween>
            </Box>
            <List>
              {navItems.map(({ text, icon, slug }) => {
                if (!icon) {
                  return (
                    <Typography
                      key={text}
                      fontWeight="bold"
                      sx={{
                        m: "2.25rem 0 1rem 3rem",
                        color: theme.palette.secondary.main,
                      }}
                    >
                      {text}
                    </Typography>
                  );
                }
                return (
                  <ListItem key={text} disablePadding>
                    <ListItemButton
                      onClick={() => {
                        navigate(`/${slug}`);
                        setActive(slug);
                      }}
                      sx={{
                        backgroundColor:
                          active === slug
                            ? theme.palette.secondary.main
                            : "transparent",
                        color:
                          active === slug
                            ? theme.palette.secondary.contrastText
                            : theme.palette.primary.contrastText,
                        "& .Mui-selected": {
                          color: theme.palette.primary.contrastText,
                        },
                      }}
                    >
                      <ListItemIcon
                        sx={{
                          ml: "2rem",
                          color:
                            active === slug
                              ? theme.palette.secondary.contrastText
                              : theme.palette.primary.contrastText,
                        }}
                      >
                        {icon}
                      </ListItemIcon>
                      <ListItemText primary={text} />
                      {active === slug && (
                        <ChevronRightOutlined sx={{ ml: "auto" }} />
                      )}
                    </ListItemButton>
                  </ListItem>
                );
              })}
            </List>
          </Box>

          <Box position="absolute" bottom="2rem">
            <Divider />
            <FlexBetween textTransform="none" gap="1rem" m="1.5rem 2rem 0 3rem">
              <Box
                component="img"
                alt="profile"
                src="https://images.pexels.com/photos/697509/pexels-photo-697509.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                height="40px"
                width="40px"
                borderRadius="50%"
                sx={{
                  objectFit: "cover",
                }}
              />
              <Box textAlign="left">
                <Typography
                  fontWeight="bold"
                  fontSize="0.9rem"
                  color="secondary"
                >
                  {props.accountInfo.user.email.split('@')[0].toUpperCase()}
                </Typography>
                <Typography
                  fontWeight="bold"
                  fontSize="0.8rem"
                  color="secondary"
                >
                  {
                    props.accountInfo.user.roles[0].name
                  }
                </Typography>
              </Box>
              <SettingsOutlined
                sx={{
                  color: theme.palette.primary.contrastText,
                  fontSize: "25px"
                }}
              />
            </FlexBetween>
          </Box>
        </Drawer>
      )}
    </Box>
  );
};

export default Sidebar;
