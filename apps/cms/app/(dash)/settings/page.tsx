'use client';

import React from 'react';
import {
  Avatar,
  Card,
  CardHeader,
  Container,
  Grid,
  Typography,
} from '@mui/material';

import ChangePasswordCard from './ChangePasswordCard';
import UserDetailsCard from './UserDetailsCard';
import useUser from '@/features/user/useUser';

export default function Settings() {
  const { data: user, isLoading } = useUser();

  return (
    <Container maxWidth={'xl'}>
      <Card>
        <CardHeader
          avatar={
            <Avatar alt="Avatar" src="" sx={{ width: 100, height: 100 }} />
          }
          // action={}
          title={
            isLoading ? (
              ''
            ) : (
              <Typography variant="h4" fontWeight="bold">
                {user?.fullName}
              </Typography>
            )
          }
          subheader={
            isLoading ? (
              ''
            ) : (
              <Typography variant="h6" color="gray">
                Administrator
              </Typography>
            )
          }
          sx={{ px: 10 }}
        />
      </Card>
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
