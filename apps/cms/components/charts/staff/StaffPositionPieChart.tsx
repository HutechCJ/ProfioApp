'use client';

import React from 'react';
import { PieChart, pieArcClasses } from '@mui/x-charts/PieChart';
import { Box, CircularProgress, Paper, Stack, Typography } from '@mui/material';
import useCountByPosition from '@/features/staff/useCountByPosition';

function StaffPositionPieChart() {
  const { data: countData, isLoading, isError } = useCountByPosition();

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
      {isLoading ? (
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
            Staff
          </Typography>
          <Typography variant="body1" color="gray">
            Total: {totalStaff}
          </Typography>
          <PieChart
            series={[
              {
                data: [
                  {
                    id: 'Drivers',
                    value: totalDrivers,
                    label: 'Drivers',
                    color: 'red',
                  },
                  {
                    id: 1,
                    value: totalShippers,
                    label: 'Shippers',
                    color: 'blue',
                  },
                  {
                    id: 2,
                    value: totalOfficers,
                    label: 'Officers',
                    color: 'green',
                  },
                  {
                    id: 3,
                    value: totalStokers,
                    label: 'Stokers',
                    color: 'brown',
                  },
                ],
                id: 'x',
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

export default StaffPositionPieChart;
