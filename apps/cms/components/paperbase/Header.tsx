'use client';

import useUser from '@/features/user/useUser';
import MenuIcon from '@mui/icons-material/Menu';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { Divider, ListItemIcon, Menu, MenuItem } from '@mui/material';
import AppBar from '@mui/material/AppBar';
import Avatar from '@mui/material/Avatar';
import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import Toolbar from '@mui/material/Toolbar';
import Tooltip from '@mui/material/Tooltip';
import Typography from '@mui/material/Typography';
import Settings from '@mui/icons-material/Settings';
import Logout from '@mui/icons-material/Logout';
import * as React from 'react';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import StoreKeys from '@/common/constants/storekeys';
import BedtimeIcon from '@mui/icons-material/Bedtime';
import SearchIcon from '@mui/icons-material/Search';
import { Box } from '@mui/material';
import InputBase from '@mui/material/InputBase';
import { Stack } from '@mui/system';
import Image from 'next/image';
import Logo from '../../public/images/CJ_logo.png';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

export const lightColor = 'rgba(255, 255, 255, 0.7)';

export interface HeaderProps {
  onDrawerToggle: () => void;
  title?: string;
  subtitle?: string;
}

export default function Header(props: HeaderProps) {
  const { onDrawerToggle, title, subtitle } = props;

  return (
    <React.Fragment>
      <HeaderDefault onDrawerToggle={onDrawerToggle} />
      <HeaderTitle title={title || 'Overview'} subtitle={subtitle || ''} />
    </React.Fragment>
  );
}

export function HeaderDefault({ onDrawerToggle }: HeaderProps) {
  const user = useUser();
  const localStorage = useLocalStorage();
  const MySwal = withReactContent(Swal);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const logout = () => {
    handleClose();
    MySwal.fire({
      title: 'Are you want to logout?',
      text: 'You will be logged out of the system!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Logout',
    }).then((result) => {
      if (result.isConfirmed) {
        fetch('/api/auth/logout', {
          method: 'POST',
        })
          .then(() => {
            localStorage.remove(StoreKeys.ACCESS_TOKEN);
            window.location.reload();
          })
          .catch(console.error);
      }
    });
  };

  return (
    <React.Fragment>
      <AppBar color="primary" position="sticky" elevation={0}>
        <Toolbar>
          <Grid container spacing={1} alignItems="center" py={2}>
            <Grid sx={{ display: { sm: 'none', xs: 'block' } }} item>
              <IconButton
                color="inherit"
                aria-label="open drawer"
                onClick={onDrawerToggle}
                edge="start"
              >
                <MenuIcon />
              </IconButton>
            </Grid>
            <Grid item>
              <Box
                sx={{
                  display: 'flex',
                  bgcolor: '#8bd4ff',
                  borderRadius: '3px',
                }}
              >
                <InputBase sx={{ ml: 2, flex: 1 }} placeholder="Search" />
                <IconButton type="button" sx={{ p: 1 }}>
                  <SearchIcon />
                </IconButton>
              </Box>
            </Grid>
            <Grid item xs />
            {/* <Grid item>
                        <Link
                            href="/"
                            variant="body2"
                            sx={{
                                textDecoration: 'none',
                                color: lightColor,
                                '&:hover': {
                                    color: 'common.white',
                                },
                            }}
                            rel="noopener noreferrer"
                            target="_blank"
                        >
                            Go to docs
                        </Link>
                    </Grid> */}
            {/* <Grid item>
              <Tooltip title="Theme">
                <IconButton color="inherit">
                  <BedtimeIcon />
                </IconButton>
              </Tooltip>
            </Grid> */}
            <Grid item>
              <Tooltip title="Alerts • No alerts">
                <IconButton color="inherit">
                  <NotificationsIcon />
                </IconButton>
              </Tooltip>
            </Grid>
            <Grid item>
              <Tooltip title="Account settings">
                <IconButton
                  onClick={handleClick}
                  color="inherit"
                  sx={{ p: 0.5 }}
                >
                  <Avatar src={''} alt={user?.fullName} />
                </IconButton>
              </Tooltip>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        slotProps={{
          paper: {
            elevation: 0,
            sx: {
              overflow: 'visible',
              filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
              mt: 1.5,
              '& .MuiAvatar-root': {
                width: 32,
                height: 32,
                ml: -0.5,
                mr: 1,
              },
              '&:before': {
                content: '""',
                display: 'block',
                position: 'absolute',
                top: 0,
                right: 14,
                width: 10,
                height: 10,
                bgcolor: 'background.paper',
                transform: 'translateY(-50%) rotate(45deg)',
                zIndex: 0,
              },
            },
          },
        }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        <MenuItem onClick={handleClose}>
          <Avatar /> {user?.fullName}
        </MenuItem>
        <Divider />
        {/* <MenuItem onClick={handleClose}>
          <ListItemIcon>
            <Settings fontSize="small" />
          </ListItemIcon>
          Settings
        </MenuItem> */}
        <MenuItem onClick={logout}>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          Logout
        </MenuItem>
      </Menu>
    </React.Fragment>
  );
}

export function HeaderTitle({
  title,
  subtitle,
  items,
}: {
  title: string;
  subtitle?: string;
  items?: React.ReactNode[];
}) {
  return (
    <AppBar
      component="div"
      position="static"
      elevation={0}
      sx={{ zIndex: 0, pl: 3.5, py: 2, bgcolor: '#fafafa' }}
    >
      <Toolbar>
        <Grid container alignItems="center" spacing={1}>
          <Grid item xs>
            <Stack direction="row" alignItems="center">
              <Box sx={{ mx: 1.5 }}>
                <Image src={Logo} alt="CJ Logo" width={48} />
              </Box>
              <Stack>
                <Typography color="black" variant="h5" component="h1">
                  {title}
                </Typography>
                <Typography color="gray" variant="body1" gutterBottom>
                  {subtitle}
                </Typography>
              </Stack>
            </Stack>
          </Grid>
          {items?.length &&
            items.map((item, i) => {
              return (
                <Grid key={`header_grid_item_${i}`} item>
                  {item}
                </Grid>
              );
            })}
        </Grid>
      </Toolbar>
    </AppBar>
  );
}

export function HeaderTabs({ items }: { items?: React.ReactNode[] }) {
  return (
    <AppBar component="div" position="static" elevation={0} sx={{ zIndex: 0 }}>
      <Tabs value={0} textColor="inherit">
        <Tab label="Users" />
        <Tab label="Sign-in method" />
        <Tab label="Templates" />
        <Tab label="Usage" />
      </Tabs>
    </AppBar>
  );
}
