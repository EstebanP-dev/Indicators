import { useDispatch } from 'react-redux';
import './navbar.scss';
import {
  AppBar,
  Box,
  Button,
  IconButton,
  InputBase,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
  useTheme,
} from '@mui/material';
import { FlexBetween } from '../../components';
import {
  LightModeOutlined,
  DarkModeOutlined,
  Menu as MenuIcon,
  Search,
  SettingsOutlined,
  ArrowDropDownOutlined,
} from '@mui/icons-material';
import { setMode } from '../../redux/states/appTheme';
import { AccountInfo } from '../../models';
import { useState } from 'react';
import { resetAccountInfo } from '../../redux/states/accountInfo';
import { useNavigate } from 'react-router-dom';
import { PublicRoutes } from '../../enviroments';

type Props = {
  accountInfo: AccountInfo;
  isSidebarOpen: boolean;
  setIsSidebarOpen: React.Dispatch<React.SetStateAction<boolean>>;
};

export const Navbar = (props: Props) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const theme = useTheme();
  const [anchorEl, setAnchorEl] = useState(null);
  const isOpen = Boolean(anchorEl);

  const handleClick = (event: any) => setAnchorEl(event.currentTarget);
  const handleClose = () => {
    dispatch(resetAccountInfo());
    navigate(PublicRoutes.LOGIN, {
      replace: true,
    });
    setAnchorEl(null);
  };

  return (
    <AppBar
      sx={{
        position: 'static',
        background: 'none',
        boxShadow: 'none',
      }}
    >
      <Toolbar sx={{ justifyContent: 'space-between' }}>
        <FlexBetween>
          <IconButton
            onClick={() => props.setIsSidebarOpen(!props.isSidebarOpen)}
            sx={{
              color: theme.palette.primary.contrastText,
            }}
          >
            <MenuIcon />
          </IconButton>
          <FlexBetween
            sx={{
              backgroundColor: theme.palette.background.paper,
              borderRadius: '9px',
              gap: '3rem',
              p: '0.1rem 1.5rem',
            }}
          >
            <InputBase placeholder='Buscar' />
            <IconButton
              sx={{
                color: theme.palette.primary.contrastText,
              }}
            >
              <Search />
            </IconButton>
          </FlexBetween>
        </FlexBetween>
        <FlexBetween gap='1.5rem'>
          <IconButton
            onClick={() => dispatch(setMode())}
            sx={{
              color: theme.palette.primary.contrastText,
            }}
          >
            {theme.palette.mode === 'dark' ? (
              <DarkModeOutlined sx={{ fontSize: '25px' }} />
            ) : (
              <LightModeOutlined sx={{ fontSize: '25px' }} />
            )}
          </IconButton>
          <IconButton
            sx={{
              color: theme.palette.primary.contrastText,
            }}
          >
            <SettingsOutlined sx={{ fontSize: '25px' }} />
          </IconButton>
          <FlexBetween>
            <Button
              onClick={handleClick}
              color='secondary'
              sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                textTransform: 'none',
                gap: '1rem',
              }}
            >
              <Box
                component='img'
                alt='profile'
                src='https://images.pexels.com/photos/697509/pexels-photo-697509.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'
                height='32px'
                width='32px'
                borderRadius='50%'
                sx={{
                  objectFit: 'cover',
                }}
              />
              <Box textAlign='left'>
                <Typography
                  fontWeight='bold'
                  fontSize='0.85rem'
                  color='secondary'
                >
                  {props.accountInfo.user.email?.split('@')[0].toUpperCase()}
                </Typography>
                <Typography
                  fontWeight='bold'
                  fontSize='0.75rem'
                  color='secondary'
                >
                  {props.accountInfo.user.roles[0].name}
                </Typography>
              </Box>
              <ArrowDropDownOutlined
                sx={{
                  fontSize: '25px',
                }}
              />
            </Button>
            <Menu
              anchorEl={anchorEl}
              open={isOpen}
              onClose={handleClose}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
              }}
            >
              <MenuItem onClick={handleClose}>Cerrar Sesión</MenuItem>
            </Menu>
          </FlexBetween>
        </FlexBetween>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
