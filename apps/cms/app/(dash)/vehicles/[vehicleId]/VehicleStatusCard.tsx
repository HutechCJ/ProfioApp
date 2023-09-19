import React from 'react';

import { Card, CardContent, Container, Stack, Typography } from '@mui/material';

import AlarmIcon from '@mui/icons-material/Alarm';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import { VehicleStatus } from '@/features/vehicle/vehicle.types';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';

const statuses = [
  {
    value: VehicleStatus.Idle,
    color: '#ed6c02',
    icon: <AlarmIcon sx={{ width: 150, height: 150 }} />,
  },
  {
    value: VehicleStatus.Busy,
    color: '#d32f2f',
    icon: <RemoveCircleOutlineIcon sx={{ width: 150, height: 150 }} />,
  },
  {
    value: VehicleStatus.Offline,
    color: '#009be5',
    icon: <WarningAmberIcon sx={{ width: 150, height: 150 }} />,
  },
];

interface VehicleStatusCardProps {
  status: VehicleStatus;
}

const VehicleStatusCard: React.FC<VehicleStatusCardProps> = ({ status }) => {
  return (
    <Container>
      <Card
        sx={{
          minHeight: 275,
          height: 320,
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
            <Typography variant="h6" gutterBottom>
              Status
            </Typography>
            {statuses.find((s) => s.value === status)?.icon || (
              <WarningAmberIcon sx={{ width: 100, height: 100 }} />
            )}
            <Typography variant="h4" fontWeight="bold" mt={1} gutterBottom>
              {VehicleStatus[status]}
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
};

export default VehicleStatusCard;
