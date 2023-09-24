'use client';

import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import useMediaQuery from '@mui/material/useMediaQuery';
import { usePathname } from 'next/navigation';
import * as React from 'react';
import Copyright from '../Copyright';
import theme from '../ThemeRegistry/theme';
import { categories } from '../navItems';
import Header from './Header';
import Navigator from './Navigator';
import { Divider } from '@mui/material';

const drawerWidth = 256;

export default function Paperbase({ children }: React.PropsWithChildren) {
  const [mobileOpen, setMobileOpen] = React.useState(false);
  const isSmUp = useMediaQuery(theme.breakpoints.up('sm'));
  const pathname = usePathname() ?? '';
  let idFromPathname = pathname.substring(pathname.lastIndexOf('/') + 1);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  // Get title of each route if it has one
  const headerTitle = categories
    .map((cate) => cate.children.map((v) => ({ ...v, category: cate.id })))
    .flat()
    .find((child) => pathname.includes(child.href));

  if (
    idFromPathname === headerTitle?.id.toLowerCase() ||
    idFromPathname === 'users'
  ) {
    idFromPathname = '';
  }

  return (
    <Box sx={{ display: 'flex', minHeight: '100vh', width: '100%' }}>
      <CssBaseline />
      <Box
        component="nav"
        sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
      >
        {isSmUp ? null : (
          <Navigator
            PaperProps={{ style: { width: drawerWidth } }}
            variant="temporary"
            open={mobileOpen}
            onClose={handleDrawerToggle}
          />
        )}
        <Navigator
          PaperProps={{ style: { width: drawerWidth } }}
          sx={{ display: { sm: 'block', xs: 'none' } }}
        />
      </Box>
      <Box
        sx={{
          flexGrow: 1,
          display: 'flex',
          flexDirection: 'column',
          width: { xs: `calc(100% - ${drawerWidth}px)` },
        }}
      >
        <Header
          onDrawerToggle={handleDrawerToggle}
          mainTitle={
            headerTitle
              ? idFromPathname
                ? `${headerTitle.id.slice(0, -1)} Details`
                : `${headerTitle.id}`
              : undefined
          }
          title={
            headerTitle
              ? `» ${headerTitle.category} » ${headerTitle.id}${
                  idFromPathname ? ` » ${idFromPathname}` : ''
                }`
              : undefined
          }
          id={idFromPathname}
          subtitle={
            headerTitle
              ? idFromPathname
                ? `Detailed information about ${headerTitle.id.slice(
                    0,
                    -1,
                  )} is managed by Profio`
                : `List of ${headerTitle.id} managed by Profio`
              : undefined
          }
        />
        <Divider />
        <Box
          component="main"
          sx={{ flex: 1, py: 1, px: 2, bgcolor: '#fafafa' }}
        >
          {children}
        </Box>
        <Box component="footer" sx={{ p: 2, bgcolor: '#fafafa' }}>
          <Copyright />
        </Box>
      </Box>
    </Box>
  );
}
