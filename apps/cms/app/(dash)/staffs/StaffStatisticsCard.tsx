'use client';

import React from 'react';

import {
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Stack,
  LinearProgress,
} from '@mui/material';

import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';

import Stat from '../../../components/Stat';
import useCountByPosition from '@/features/staff/useCountByPosition';

const StaffStatisticsCard = () => {
  const { data: countData, isLoading, isError } = useCountByPosition();

  if (isLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isError) {
    return <p>Error loading staff data.</p>;
  }

  const totalDrivers = countData?.data?.[0] || 0;
  const totalShippers = countData?.data?.[1] || 0;
  const totalStaff = totalDrivers + totalShippers;

  return (
    <Card sx={{ marginBottom: 4 }}>
      <CardHeader title="STAFF" subheader="Statistics" />
      <Divider />
      <CardContent sx={{ marginY: 4 }}>
        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
        >
          <Stat
            icon={<PeopleIcon />}
            iconColor="orange"
            value={totalStaff}
            description="Total Staff"
          />
          <Stat
            icon={<LocalShippingIcon />}
            value={totalDrivers}
            description="Driver"
            iconColor="red"
          />
          <Stat
            icon={<MopedIcon />}
            value={totalShippers}
            description="Shipper"
            iconColor="blue"
          />
        </Stack>
      </CardContent>
    </Card>
  );
};

export default StaffStatisticsCard;
