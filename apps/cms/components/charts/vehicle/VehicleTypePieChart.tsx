'use client';

import React from 'react';
import { PieChart } from '@mui/x-charts/PieChart';
import { Box, CircularProgress, Paper, Stack, Typography } from '@mui/material';
import useCountByVehicleType from '@/features/vehicle/useCountByVehicleType';

function VehicleTypePieChart() {
  const {
    data: countDataType,
    isLoading: isLoadingType,
    isError: isErrorType,
  } = useCountByVehicleType();

  if (isErrorType) {
    return <p>Error loading vehicle data.</p>;
  }

  const totalTrucks = countDataType?.data?.[0] || 0;
  const totalTrailers = countDataType?.data?.[1] || 0;
  const totalVans = countDataType?.data?.[2] || 0;
  const totalMotorcycles = countDataType?.data?.[3] || 0;
  const totalVehicle =
    totalTrucks + totalTrailers + totalVans + totalMotorcycles;

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
      {isLoadingType ? (
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
            Type Of Vehicle
          </Typography>
          <Typography variant="body1" color="gray">
            Total: {totalVehicle}
          </Typography>
          <PieChart
            series={[
              {
                data: [
                  { id: 0, value: totalTrucks, label: 'Truck' },
                  { id: 1, value: totalTrailers, label: 'Trailers' },
                  { id: 2, value: totalVans, label: 'Vans' },
                  { id: 3, value: totalMotorcycles, label: 'Motors' },
                ],
                innerRadius: 30,
                outerRadius: 120,
                paddingAngle: 2,
                cornerRadius: 5,
                startAngle: 0,
                endAngle: 360,
                cx: 150,
                cy: 150,
              },
            ]}
            width={400}
            height={300}
          />
        </Stack>
      )}
    </Paper>
  );
}

export default VehicleTypePieChart;
