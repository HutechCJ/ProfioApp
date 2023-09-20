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
  Grid,
  Paper,
} from '@mui/material';

import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import InventoryIcon from '@mui/icons-material/Inventory';

import Stat, { StatProps } from '../../../components/Stat';
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
  const totalOfficers = countData?.data?.[2] || 0;
  const totalStokers = countData?.data?.[3] || 0;
  const totalStaff =
    totalDrivers + totalShippers + totalOfficers + totalStokers;

  return (
    <Card sx={{ marginBottom: 4 }}>
      <CardHeader title="STAFF" subheader="Statistics" />
      <Divider />
      <CardContent>
        <Grid
          minHeight={128}
          container
          direction="row"
          spacing={2}
          columns={{ xs: 1, sm: 2, md: 5 }}
        >
          <Grid item xs={1}>
            <StatCard
              icon={<PeopleIcon />}
              iconColor="orange"
              value={totalStaff}
              description="Total Staff"
            />
          </Grid>
          <Grid item xs={1}>
            <StatCard
              icon={<LocalShippingIcon />}
              value={totalDrivers}
              description="Driver"
              iconColor="red"
            />
          </Grid>
          <Grid item xs={1}>
            <StatCard
              icon={<MopedIcon />}
              value={totalShippers}
              description="Shipper"
              iconColor="blue"
            />
          </Grid>
          <Grid item xs={1}>
            <StatCard
              icon={<BusinessCenterIcon />}
              value={totalOfficers}
              description="Officer"
              iconColor="green"
            />
          </Grid>
          <Grid item xs={1}>
            <StatCard
              icon={<InventoryIcon />}
              value={totalStokers}
              description="Stoker"
              iconColor="brown"
            />
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
};

export default StaffStatisticsCard;

function StatCard({ ...props }: StatProps) {
  return (
    <Paper
      sx={{
        padding: 4,
        height: '100%',
      }}
    >
      <Stat {...props} />
    </Paper>
  );
}
