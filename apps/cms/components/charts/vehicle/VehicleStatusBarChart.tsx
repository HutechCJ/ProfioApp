'use client';

import React from 'react';
import { Paper, Typography, Stack, CircularProgress, Box } from '@mui/material';
import { BarChart } from '@mui/x-charts/BarChart';
import useCountByVehicleStatus from '@/features/vehicle/useCountByVehicleStatus';

function VehicleStatusBarChart() {
  const {
    data: countDataStatus,
    isLoading: isLoadingStatus,
    isError: isErrorStatus,
  } = useCountByVehicleStatus();

  if (isErrorStatus) {
    return <p>Error loading vehicle data.</p>;
  }

  const totalIdle = countDataStatus?.data?.[0] || 0;
  const totalBusy = countDataStatus?.data?.[1] || 0;
  const totalOffline = countDataStatus?.data?.[2] || 0;

  const uData = [totalIdle, totalBusy, totalOffline];
  const xLabels = ['Idle', 'Busy', 'Offline'];

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
            Status Of Vehicle
          </Typography>
          <BarChart
            width={500}
            height={300}
            series={[{ data: uData, label: 'status', id: 'uvId' }]}
            xAxis={[{ data: xLabels, scaleType: 'band' }]}
          />
        </Stack>
      )}
    </Paper>
  );
}

export default VehicleStatusBarChart;
