'use client';

import React from 'react';

import {
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Grid,
  LinearProgress,
} from '@mui/material';

import InventoryIcon from '@mui/icons-material/Inventory';
import PauseCircleFilledIcon from '@mui/icons-material/PauseCircleFilled';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import ArchiveIcon from '@mui/icons-material/Archive';
import CancelIcon from '@mui/icons-material/Cancel';

import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';
import StatColorCard from '@/components/StatColorCard';

const OrderStatisticsCard = () => {
  const {
    data: countDataStatus,
    isLoading: isLoadingStatus,
    isError: isErrorStatus,
  } = useCountByOrderStatus();

  if (isLoadingStatus) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isErrorStatus) {
    return <p>Error loading order data.</p>;
  }

  const totalPending = countDataStatus?.data?.[0] || 0;
  const totalInProgress = countDataStatus?.data?.[1] || 0;
  const totalCompleted = countDataStatus?.data?.[2] || 0;
  const totalReceived = countDataStatus?.data?.[3] || 0;
  const totalCancelled = countDataStatus?.data?.[4] || 0;
  const totalOrders =
    totalPending +
    totalInProgress +
    totalCompleted +
    totalReceived +
    totalCancelled;

  return (
    <Card sx={{ marginBottom: 4 }}>
      <CardHeader title="ORDER" subheader="Statistics" />
      <Divider />
      <CardContent>
        <Grid
          minHeight={128}
          container
          direction="row"
          spacing={1}
          columns={{ xs: 1, sm: 3, md: 6 }}
          mb={2}
        >
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#dd77f2"
              icon={<InventoryIcon />}
              label="Total Orders"
              value={totalOrders}
            />
          </Grid>
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#e2e2e2"
              icon={<PauseCircleFilledIcon />}
              label="Pending"
              value={totalPending}
            />
          </Grid>
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#ed9a56"
              icon={<LocalShippingIcon />}
              label="InProgress"
              value={totalInProgress}
            />
          </Grid>
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#61b2de"
              icon={<CheckCircleIcon />}
              label="Completed"
              value={totalCompleted}
            />
          </Grid>
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#579f5a"
              icon={<ArchiveIcon />}
              label="Received"
              value={totalReceived}
            />
          </Grid>
          <Grid item xs={1}>
            <StatColorCard
              cardColor="#d36363"
              icon={<CancelIcon />}
              label="Cancelled"
              value={totalCancelled}
            />
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
};

export default OrderStatisticsCard;
