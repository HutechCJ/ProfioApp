'use client';

import React from 'react';
import { PieChart, pieArcClasses } from '@mui/x-charts/PieChart';
import { Box, CircularProgress, Paper, Stack, Typography } from '@mui/material';
import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';

function OrderStatusPieChart() {
  const {
    data: countDataStatus,
    isLoading: isLoadingStatus,
    isError: isErrorStatus,
  } = useCountByOrderStatus();

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
    <Paper
      sx={{
        height: '100%',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        p: 2,
        my: 2,
      }}
    >
      {isLoadingStatus ? (
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            height: '35vh',
          }}
        >
          <CircularProgress />
        </Box>
      ) : (
        <Stack alignItems="center">
          <Typography variant="h6" fontWeight="bold">
            Order
          </Typography>
          <Typography variant="body1" color="gray">
            Total: {totalOrders}
          </Typography>
          <PieChart
            series={[
              {
                data: [
                  {
                    id: 0,
                    value: totalPending,
                    label: 'Pending',
                    color: '#e2e2e2',
                  },
                  {
                    id: 1,
                    value: totalInProgress,
                    label: 'InProgress',
                    color: '#ed6c02',
                  },
                  {
                    id: 2,
                    value: totalCompleted,
                    label: 'Completed',
                    color: '#0288d1',
                  },
                  {
                    id: 3,
                    value: totalReceived,
                    label: 'Received',
                    color: '#2e7d32',
                  },
                  {
                    id: 4,
                    value: totalCancelled,
                    label: 'Cancelled',
                    color: '#d32f2f',
                  },
                ],
                innerRadius: 30,
                outerRadius: 120,
                paddingAngle: 2,
                cornerRadius: 5,
                startAngle: 0,
                endAngle: 360,
                cx: 150,
                cy: 150,
                highlightScope: { faded: 'global', highlighted: 'item' },
                faded: { innerRadius: 30, additionalRadius: -30 },
              },
            ]}
            width={400}
            height={300}
            sx={{
              [`& .${pieArcClasses.faded}`]: {
                fill: 'gray',
              },
            }}
          />
        </Stack>
      )}
    </Paper>
  );
}

export default OrderStatusPieChart;
