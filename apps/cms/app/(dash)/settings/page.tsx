'use client';

import React from 'react';
import {
  Avatar,
  Box,
  Card,
  CardHeader,
  Container,
  Grid,
  LinearProgress,
  Typography,
} from '@mui/material';

import ChangePasswordCard from './ChangePasswordCard';
import UserDetailsCard from './UserDetailsCard';
import useUser from '@/features/user/useUser';
// import { StaffPosition } from '@/features/staff/staff.types';
// import Link from '@/components/Link';

export default function Settings() {
  const { data: user, isLoading } = useUser();

  return (
    <Container maxWidth={'xl'}>
      <Card>
        <CardHeader
          avatar={
            <Avatar alt="Avatar" src="" sx={{ width: 100, height: 100 }} />
          }
          // action={
          //   <Link href={`/staffs/${user?.staff?.id}`} sx={{ color: 'black' }}>
          //     <Typography>
          //       View Staff
          //     </Typography>
          //   </Link>
          // }
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
      {isLoading ? (
        <Box sx={{ width: '100%', mt: 1 }}>
          <LinearProgress />
        </Box>
      ) : (
        <Grid
          container
          direction="row"
          spacing={2}
          columns={{ xs: 1, sm: 1, md: 2 }}
          mt={0.5}
        >
          <Grid item xs={1}>
            <UserDetailsCard />
          </Grid>
          <Grid item xs={1}>
            <ChangePasswordCard />
          </Grid>
        </Grid>
      )}
    </Container>
  );
}
