'use client';

import StoreKeys from '@/common/constants/storekeys';
import useLocalStorage from '@/common/hooks/useLocalStorage';
import useUser from '@/features/user/useUser';
import Logout from '@mui/icons-material/Logout';
import MenuIcon from '@mui/icons-material/Menu';
import NotificationsIcon from '@mui/icons-material/Notifications';
import Settings from '@mui/icons-material/Settings';
import { Divider, ListItemIcon, Menu, MenuItem, Skeleton } from '@mui/material';
import AppBar from '@mui/material/AppBar';
import Avatar from '@mui/material/Avatar';
import Grid from '@mui/material/Grid';
import IconButton from '@mui/material/IconButton';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import Toolbar from '@mui/material/Toolbar';
import Tooltip from '@mui/material/Tooltip';
import Typography from '@mui/material/Typography';
import * as React from 'react';
// import BedtimeIcon from '@mui/icons-material/Bedtime';
// import SearchIcon from '@mui/icons-material/Search';
import { Box } from '@mui/material';
// import InputBase from '@mui/material/InputBase';
import useSwal from '@/common/hooks/useSwal';
import HomeIcon from '@mui/icons-material/Home';
import { Stack } from '@mui/system';
import Image from 'next/image';
import Link from 'next/link';
import Logo from '../../public/images/CJ_logo.png';
import CopyTextButton from '../CopyTextButton';

export const lightColor = 'rgba(255, 255, 255, 0.7)';

export interface HeaderProps {
  onDrawerToggle: () => void;
  mainTitle?: string;
  title?: string;
  id?: string;
  subtitle?: string;
}

export default function Header(props: HeaderProps) {
  const { onDrawerToggle, mainTitle, title, id, subtitle } = props;

  return (
    <React.Fragment>
      <HeaderDefault onDrawerToggle={onDrawerToggle} />
      <HeaderTitle
        mainTitle={mainTitle || ''}
        title={title || ''}
        id={id}
        subtitle={subtitle || ''}
      />
    </React.Fragment>
  );
}

export function HeaderDefault({ onDrawerToggle }: HeaderProps) {
  const { data: user, isLoading } = useUser();
  const localStorage = useLocalStorage();
  const MySwal = useSwal();
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
      title: 'Are you sure you want to logout?',
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
            {/* <Grid item>
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
            </Grid> */}
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
          <Avatar />
          {isLoading ? (
            <Skeleton sx={{ bgcolor: 'grey.400' }} width={150} height={40} />
          ) : (
            user?.fullName
          )}
        </MenuItem>
        <Divider />
        <Link
          href={'/settings'}
          style={{ textDecoration: 'none', color: '#202020' }}
        >
          <MenuItem onClick={handleClose}>
            <ListItemIcon>
              <Settings fontSize="small" />
            </ListItemIcon>
            Settings
          </MenuItem>
        </Link>
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
  mainTitle,
  title,
  id,
  subtitle,
  items,
}: {
  mainTitle: string;
  title: string;
  id?: string;
  subtitle?: string;
  items?: React.ReactNode[];
}) {
  return (
    <AppBar
      component="div"
      position="static"
      elevation={0}
      sx={{ zIndex: 0, pl: 3.5, py: 1.5, bgcolor: '#fafafa' }}
    >
      <Toolbar>
        <Grid container alignItems="center" spacing={1}>
          <Grid item xs>
            <Stack direction="row" alignItems="center">
              <Box sx={{ mx: 1.5 }}>
                <Image src={Logo} alt="CJ Logo" width={48} />
              </Box>
              <Stack>
                <Stack
                  direction={{ md: 'column', lg: 'row' }}
                  alignContent="center"
                  alignItems={{ md: 'flex-start', lg: 'center' }}
                  justifyContent="center"
                >
                  {mainTitle && (
                    <Stack direction="row">
                      <Typography color="black" variant="h5" mt="3px">
                        {mainTitle} |
                      </Typography>
                      <Link href={'/'}>
                        <IconButton sx={{ '&:hover': { color: '#007dc3' } }}>
                          <HomeIcon />
                        </IconButton>
                      </Link>
                    </Stack>
                  )}

                  {title ? (
                    <Typography color="gray" variant="h6">
                      {title}
                      {id && <CopyTextButton text={id} />}
                    </Typography>
                  ) : (
                    <Typography color="black" variant="h5">
                      Overview
                    </Typography>
                  )}
                </Stack>
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
