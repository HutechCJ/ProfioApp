'use client';

import React from 'react';
import { PieChart, pieArcClasses } from '@mui/x-charts/PieChart';
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
            Vehicle
          </Typography>
          <Typography variant="body1" color="gray">
            Total: {totalVehicle}
          </Typography>
          <PieChart
            series={[
              {
                data: [
                  {
                    id: 0,
                    value: totalTrucks,
                    label: 'Truck',
                    color: '#009be5',
                  },
                  {
                    id: 1,
                    value: totalTrailers,
                    label: 'Trailers',
                    color: '#d32f2f',
                  },
                  {
                    id: 2,
                    value: totalVans,
                    label: 'Vans',
                    color: '#ed6c02',
                  },
                  {
                    id: 3,
                    value: totalMotorcycles,
                    label: 'Motors',
                    color: '#9c27b0',
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

export default VehicleTypePieChart;
