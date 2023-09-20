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
      <CardContent sx={{ marginY: 4 }}>
        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
          divider={<Divider orientation="vertical" flexItem />}
          mb={4}
        >
          <StatColorCard
            cardColor="#dd77f2"
            icon={<InventoryIcon />}
            label="Total Orders"
            value={totalOrders}
          />
          <StatColorCard
            cardColor="#e2e2e2"
            icon={<PauseCircleFilledIcon />}
            label="Pending"
            value={totalPending}
          />
          <StatColorCard
            cardColor="#ed9a56"
            icon={<LocalShippingIcon />}
            label="InProgress"
            value={totalInProgress}
          />
        </Stack>

        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
          divider={<Divider orientation="vertical" flexItem />}
          mb={4}
        >
          <StatColorCard
            cardColor="#61b2de"
            icon={<CheckCircleIcon />}
            label="Completed"
            value={totalCompleted}
          />
          <StatColorCard
            cardColor="#579f5a"
            icon={<ArchiveIcon />}
            label="Received"
            value={totalReceived}
          />
          <StatColorCard
            cardColor="#d36363"
            icon={<CancelIcon />}
            label="Cancelled"
            value={totalCancelled}
          />
        </Stack>
      </CardContent>
    </Card>
  );
};

export default OrderStatisticsCard;
