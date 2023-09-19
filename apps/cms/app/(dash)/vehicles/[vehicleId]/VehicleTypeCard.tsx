import React from 'react';

import { Card, CardContent, Container, Stack, Typography } from '@mui/material';
import { VehicleType } from '@/features/vehicle/vehicle.types';

import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import RvHookupIcon from '@mui/icons-material/RvHookup';
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle';
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler';

const types = [
  {
    value: VehicleType.Truck,
    color: `rgba(0, 155, 229, 0.75)`,
    icon: <LocalShippingIcon sx={{ width: 100, height: 100 }} />,
  },
  {
    value: VehicleType.Trailer,
    color: `rgba(211, 47, 47, 0.75)`,
    icon: <RvHookupIcon sx={{ width: 100, height: 100 }} />,
  },
  {
    value: VehicleType.Van,
    color: `rgb(237, 108, 2, 0.75)`,
    icon: <AirportShuttleIcon sx={{ width: 100, height: 100 }} />,
  },
  {
    value: VehicleType.Motorcycle,
    color: `rgba(156, 39, 176, 0.75)`,
    icon: <TwoWheelerIcon sx={{ width: 100, height: 100 }} />,
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
    <Container>
      <Card
        sx={{
          minHeight: 275,
          height: 320,
          bgcolor: types.find((s) => s.value === type)?.color || '#e2e2e2',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
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
            <Typography variant="h6" gutterBottom>
              Vehicle
            </Typography>
            {types.find((s) => s.value === type)?.icon || (
              <LocalShippingIcon sx={{ width: 100, height: 100 }} />
            )}
            <Typography variant="h4" fontWeight="bold" gutterBottom>
              {VehicleType[type]}
            </Typography>
            <Typography variant="h6" color="#575757" gutterBottom>
              Zip Code Current: <strong>{zipCodeCurrent}</strong>
            </Typography>
            <Typography variant="h6" color="#575757" gutterBottom>
              License Plate: <strong>{licensePlate}</strong>
            </Typography>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
};

export default VehicleTypeCard;
