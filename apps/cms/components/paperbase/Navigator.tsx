'use client';

import * as React from 'react';
import Divider from '@mui/material/Divider';
import Drawer, { DrawerProps } from '@mui/material/Drawer';
import List from '@mui/material/List';
import Box from '@mui/material/Box';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import HomeIcon from '@mui/icons-material/Home';
import Link from 'next/link';
// import { usePathname } from 'next/navigation';
import { categories } from '../navItems';
import { Stack } from '@mui/system';
import { Avatar, Typography } from '@mui/material';
import Image from 'next/image';
import Logo from '../../public/images/CJ_logo.png';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import useUser from '@/features/user/useUser';
import Copyright from '../Copyright';

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
  const user = useUser();

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

        <ListItem sx={{ ...item, ...itemCategory }}>
          <ListItemText>
            <Stack alignItems="center">
              <Avatar sx={{ width: 80, height: 80 }} />
              <Typography variant="body1" color="white" my={1}>
                {user?.fullName}
              </Typography>
            </Stack>
          </ListItemText>
        </ListItem>

        <ListItem sx={{ ...item, ...itemCategory }}>
          <ListItemButton LinkComponent={Link} href={'/'}>
            <ListItemIcon>
              <HomeIcon />
            </ListItemIcon>
            <ListItemText>Overview</ListItemText>
          </ListItemButton>
        </ListItem>

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
        <Copyright color="#bdbdbd" sx={{ my: 2 }} />
      </List>
    </Drawer>
  );
}
