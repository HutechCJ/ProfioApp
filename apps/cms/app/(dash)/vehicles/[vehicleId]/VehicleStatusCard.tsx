import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';

import AlarmIcon from '@mui/icons-material/Alarm';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import { VehicleStatus } from '@/features/vehicle/vehicle.types';

const statuses = [
  {
    value: VehicleStatus.Idle,
    color: '#ed6c02',
    icon: <AlarmIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: VehicleStatus.Busy,
    color: '#d32f2f',
    icon: <RemoveCircleOutlineIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: VehicleStatus.Offline,
    color: '#009be5',
    icon: <WarningAmberIcon sx={{ width: 50, height: 50 }} />,
  },
];

interface VehicleStatusCardProps {
  status: VehicleStatus;
}

const VehicleStatusCard: React.FC<VehicleStatusCardProps> = ({ status }) => {
  return (
    <Box>
      <Card
        sx={{
          minHeight: 240,
          bgcolor: statuses.find((s) => s.value === status)?.color || '#e2e2e2',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <CardContent sx={{ textAlign: 'center' }}>
          <Stack
            sx={{
              textAlign: 'center',
              justifyContent: 'center',
              alignItems: 'center',
            }}
          >
            <Typography variant="body1" gutterBottom>
              Status
            </Typography>
            {statuses.find((s) => s.value === status)?.icon || (
              <WarningAmberIcon sx={{ width: 50, height: 50 }} />
            )}
            <Typography variant="h6" fontWeight="bold" mt={1} gutterBottom>
              {VehicleStatus[status]}
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};

export default VehicleStatusCard;
