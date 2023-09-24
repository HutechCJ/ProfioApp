'use client';

import React from 'react';
import { Container, Grid } from '@mui/material';

import ChangePasswordCard from './ChangePasswordCard';
import UserDetailsCard from './UserDetailsCard';

export default function Settings() {
  return (
    <Container maxWidth={'xl'}>
      <Grid
        container
        direction="row"
        spacing={2}
        columns={{ xs: 1, sm: 1, md: 2 }}
        mt={1}
      >
        <Grid item xs={1}>
          <UserDetailsCard />
        </Grid>
        <Grid item xs={1}>
          <ChangePasswordCard />
        </Grid>
      </Grid>
    </Container>
  );
}
