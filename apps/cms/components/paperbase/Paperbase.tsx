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

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  // Get title of each route if it has one
  const headerTitle = categories
    .map((cate) => cate.children.map((v) => ({ ...v, category: cate.id })))
    .flat()
    .find((child) => pathname.includes(child.href));

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
      <Box sx={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
        <Header
          onDrawerToggle={handleDrawerToggle}
          title={
            headerTitle
              ? `${headerTitle.category} » ${headerTitle.id}`
              : undefined
          }
          subtitle={
            headerTitle
              ? `List of ${headerTitle.id} managed by Profio`
              : undefined
          }
        />
        <Divider />
        <Box
          component="main"
          sx={{ flex: 1, py: 4, px: 2, bgcolor: '#fafafa', overflow: 'auto' }}
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
