import React from 'react';

import { Card, CardContent, Box, Stack, Typography } from '@mui/material';
import { VehicleType } from '@/features/vehicle/vehicle.types';

import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import RvHookupIcon from '@mui/icons-material/RvHookup';
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle';
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler';

const types = [
  {
    value: VehicleType.Truck,
    color: '#a6dfff',
    icon: <LocalShippingIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: VehicleType.Trailer,
    color: '#ffacac',
    icon: <RvHookupIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: VehicleType.Van,
    color: '#ffd1ab',
    icon: <AirportShuttleIcon sx={{ width: 50, height: 50 }} />,
  },
  {
    value: VehicleType.Motorcycle,
    color: '#f6c5ff',
    icon: <TwoWheelerIcon sx={{ width: 50, height: 50 }} />,
  },
];

interface VehicleTypeCardProps {
  type: VehicleType;
  zipCodeCurrent: string;
  licensePlate: string;
}

const VehicleTypeCard: React.FC<VehicleTypeCardProps> = ({
  type,
  zipCodeCurrent,
  licensePlate,
}) => {
  return (
    <Box>
      <Card
        sx={{
          minHeight: 240,
          bgcolor: types.find((s) => s.value === type)?.color || '#e2e2e2',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          pt: 2,
        }}
      >
        <CardContent>
          <Stack
            sx={{
              textAlign: 'center',
              justifyContent: 'center',
              alignItems: 'center',
            }}
          >
            <Typography variant="body1" gutterBottom>
              Vehicle
            </Typography>
            {types.find((s) => s.value === type)?.icon || (
              <LocalShippingIcon sx={{ width: 100, height: 100 }} />
            )}
            <Typography variant="h6" fontWeight="bold" gutterBottom>
              {VehicleType[type]}
            </Typography>
            <Typography variant="body1" gutterBottom>
              Zip Code Current: <strong>{zipCodeCurrent}</strong>
            </Typography>
            <Typography variant="body1" gutterBottom>
              License Plate: <strong>{licensePlate}</strong>
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};

export default VehicleTypeCard;
