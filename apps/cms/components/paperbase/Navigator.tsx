'use client';

import HomeIcon from '@mui/icons-material/Home';
import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import Drawer, { DrawerProps } from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Link from 'next/link';
// import { usePathname } from 'next/navigation';
import useUser from '@/features/user/useUser';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import { Avatar, Skeleton, Typography } from '@mui/material';
import { Stack } from '@mui/system';
import Image from 'next/image';
import Logo from '../../public/images/CJ_logo.png';
import { categories } from '../navItems';

import SettingsIcon from '@mui/icons-material/Settings';
import LogoutIcon from '@mui/icons-material/Logout';

import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import StoreKeys from '@/common/constants/storekeys';
import useLocalStorage from '@/common/hooks/useLocalStorage';

const item = {
  py: '2px',
  px: 3,
  color: 'rgba(255, 255, 255, 0.7)',
  '&:hover, &:focus': {
    bgcolor: 'rgba(255, 255, 255, 0.08)',
  },
};

const itemCategory = {
  boxShadow: '0 -1px 0 rgb(255,255,255,0.1) inset',
  py: 1.5,
  px: 3,
};

export default function Navigator(props: DrawerProps) {
  // const pathname = usePathname();
  const { ...other } = props;
  const { data: user, isLoading } = useUser();
  const localStorage = useLocalStorage();
  const MySwal = withReactContent(Swal);

  const logout = () => {
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
    <Drawer variant="permanent" {...other}>
      <List disablePadding>
        <ListItem
          sx={{
            ...item,
            ...itemCategory,
            fontSize: 22,
            color: '#fff',
          }}
        >
          <Stack justifyContent="center" alignItems="center">
            <Stack
              direction="row"
              justifyContent="center"
              alignItems="center"
              spacing={2}
            >
              <Avatar sx={{ bgcolor: 'white' }}>
                <Image src={Logo} alt="CJ Logo" width={20} height={20} />
              </Avatar>
              <Typography variant="h6" fontSize={18} fontWeight="bold">
                Profio Application
              </Typography>
            </Stack>
            <Typography variant="body2" fontSize={14} mt={1}>
              Content Management System
            </Typography>
          </Stack>
        </ListItem>

        <Box>
          <ListItem sx={{ ...item, ...itemCategory }}>
            <ListItemText>
              <Stack alignItems="center">
                <Avatar sx={{ width: 80, height: 80 }} />
                {isLoading ? (
                  <Skeleton
                    // animation="wave"
                    sx={{ bgcolor: 'grey.800' }}
                    width={150}
                    height={40}
                  />
                ) : (
                  <Box>
                    <Typography variant="body1" color="white" my={1}>
                      {user?.fullName}
                    </Typography>
                  </Box>
                )}
              </Stack>
            </ListItemText>
          </ListItem>
        </Box>

        <Box>
          <ListItem sx={{ ...item, ...itemCategory }}>
            <ListItemButton LinkComponent={Link} href={'/'}>
              <ListItemIcon>
                <HomeIcon />
              </ListItemIcon>
              <ListItemText>Overview</ListItemText>
            </ListItemButton>
          </ListItem>
        </Box>

        {categories.map(({ id, children }) => (
          <Box key={id} sx={{ bgcolor: '#101F33' }}>
            <ListItem sx={{ py: 2, px: 3 }}>
              <ListItemText sx={{ color: '#fff' }}>{id}</ListItemText>
            </ListItem>
            {children.map(({ id: childId, icon, href }) => (
              <ListItem disablePadding key={childId}>
                <ListItemButton
                  LinkComponent={Link}
                  href={href}
                  selected={false}
                  sx={item}
                >
                  <ListItemIcon>{icon}</ListItemIcon>
                  <ListItemText>{childId}</ListItemText>
                </ListItemButton>
              </ListItem>
            ))}
            <Divider sx={{ mt: 2 }} />
          </Box>
        ))}

        <Box key="account" sx={{ bgcolor: '#101F33' }}>
          <ListItem sx={{ py: 2, px: 3 }}>
            <ListItemText sx={{ color: '#fff' }}>Account</ListItemText>
          </ListItem>
          <ListItem disablePadding key="Settings">
            <ListItemButton
              LinkComponent={Link}
              href={'/settings'}
              selected={false}
              sx={item}
            >
              <ListItemIcon>
                <SettingsIcon />
              </ListItemIcon>
              <ListItemText>Settings</ListItemText>
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding key="Logout">
            <ListItemButton
              LinkComponent={Link}
              onClick={logout}
              selected={false}
              sx={item}
            >
              <ListItemIcon>
                <LogoutIcon />
              </ListItemIcon>
              <ListItemText>Logout</ListItemText>
            </ListItemButton>
          </ListItem>
          <Divider sx={{ mt: 2 }} />
        </Box>

        <ListItem
          sx={{
            ...item,
            ...itemCategory,
          }}
        >
          <ListItemButton
            LinkComponent={Link}
            href={'https://profio-document.onrender.com/'}
            target="_blank"
          >
            <ListItemIcon>
              <AutoStoriesIcon />
            </ListItemIcon>
            <ListItemText>View Documentation</ListItemText>
          </ListItemButton>
        </ListItem>
      </List>
    </Drawer>
  );
}
