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
import PersonAdd from '@mui/icons-material/PersonAdd';
import Settings from '@mui/icons-material/Settings';
import Logout from '@mui/icons-material/Logout';
import * as React from 'react';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import StoreKeys from '@/common/constants/storekeys';

export const lightColor = 'rgba(255, 255, 255, 0.7)';

export interface HeaderProps {
  onDrawerToggle: () => void;
  title?: string;
}

export default function Header(props: HeaderProps) {
  const { onDrawerToggle, title } = props;

  return (
    <React.Fragment>
      <HeaderDefault onDrawerToggle={onDrawerToggle} />
      <HeaderTitle title={title || 'Overview'} />
    </React.Fragment>
  );
}

export function HeaderDefault({ onDrawerToggle }: HeaderProps) {
  const user = useUser();
  const localStorage = useLocalStorage();
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
    fetch('/api/auth/logout', {
      method: 'POST',
    })
      .then(() => {
        localStorage.remove(StoreKeys.ACCESS_TOKEN);
        window.location.reload();
      })
      .catch(console.error);
  };

  return (
    <React.Fragment>
      <AppBar color="primary" position="sticky" elevation={0}>
        <Toolbar>
          <Grid container spacing={1} alignItems="center">
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
        <MenuItem onClick={handleClose}>
          <ListItemIcon>
            <Settings fontSize="small" />
          </ListItemIcon>
          Settings
        </MenuItem>
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
  items,
}: {
  title: string;
  items?: React.ReactNode[];
}) {
  return (
    <AppBar
      component="div"
      color="primary"
      position="static"
      elevation={0}
      sx={{ zIndex: 0 }}
    >
      <Toolbar>
        <Grid container alignItems="center" spacing={1}>
          <Grid item xs>
            <Typography color="inherit" variant="h5" component="h1">
              {title}
            </Typography>
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
