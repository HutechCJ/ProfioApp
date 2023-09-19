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
  Typography,
  Container,
} from '@mui/material';

import EvStationIcon from '@mui/icons-material/EvStation';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import RvHookupIcon from '@mui/icons-material/RvHookup';
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle';
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler';
import AlarmIcon from '@mui/icons-material/Alarm';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';

import Stat from '../../../components/Stat';
import useCountByVehicleType from '@/features/vehicle/useCountByVehicleType';
import useCountByVehicleStatus from '@/features/vehicle/useCountByVehicleStatus';

const VehicleStatisticsCard = () => {
  const {
    data: countDataType,
    isLoading: isLoadingType,
    isError: isErrorType,
  } = useCountByVehicleType();
  const {
    data: countDataStatus,
    isLoading: isLoadingStatus,
    isError: isErrorStatus,
  } = useCountByVehicleStatus();

  if (isLoadingType && isLoadingStatus) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isErrorType && isErrorStatus) {
    return <p>Error loading vehicle data.</p>;
  }

  const totalTrucks = countDataType?.data?.[0] || 0;
  const totalTrailers = countDataType?.data?.[1] || 0;
  const totalVans = countDataType?.data?.[2] || 0;
  const totalMotorcycles = countDataType?.data?.[3] || 0;
  const totalVehicle =
    totalTrucks + totalTrailers + totalVans + totalMotorcycles;

  const totalIdle = countDataStatus?.data?.[0] || 0;
  const totalBusy = countDataStatus?.data?.[1] || 0;
  const totalOffline = countDataStatus?.data?.[2] || 0;

  return (
    <Card sx={{ marginBottom: 4 }}>
      <CardHeader title="VEHICLE" subheader="Statistics" />
      <Divider />
      <CardContent sx={{ marginY: 4 }}>
        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
          divider={<Divider orientation="vertical" flexItem />}
          mb={4}
        >
          <Container>
            <Card
              sx={{
                bgcolor: '#ed6c02',
                justifyContent: 'center',
                alignItems: 'center',
              }}
            >
              <CardContent>
                <Stack direction="row" justifyContent="space-around">
                  <AlarmIcon sx={{ width: 100, height: 100 }} />
                  <Stack
                    sx={{
                      textAlign: 'center',
                      justifyContent: 'center',
                      alignItems: 'center',
                    }}
                  >
                    <Typography variant="h6" gutterBottom>
                      Idle
                    </Typography>
                    <Typography variant="h4" fontWeight="bold" mt={1}>
                      {totalIdle}
                    </Typography>
                  </Stack>
                </Stack>
              </CardContent>
            </Card>
          </Container>

          <Container>
            <Card
              sx={{
                bgcolor: '#d32f2f',
                justifyContent: 'center',
                alignItems: 'center',
              }}
            >
              <CardContent>
                <Stack direction="row" justifyContent="space-around">
                  <RemoveCircleOutlineIcon sx={{ width: 100, height: 100 }} />
                  <Stack
                    sx={{
                      textAlign: 'center',
                      justifyContent: 'center',
                      alignItems: 'center',
                    }}
                  >
                    <Typography variant="h6" gutterBottom>
                      Busy
                    </Typography>
                    <Typography variant="h4" fontWeight="bold" mt={1}>
                      {totalBusy}
                    </Typography>
                  </Stack>
                </Stack>
              </CardContent>
            </Card>
          </Container>

          <Container>
            <Card
              sx={{
                bgcolor: '#009be5',
                justifyContent: 'center',
                alignItems: 'center',
              }}
            >
              <CardContent>
                <Stack direction="row" justifyContent="space-around">
                  <WarningAmberIcon sx={{ width: 100, height: 100 }} />
                  <Stack
                    sx={{
                      textAlign: 'center',
                      justifyContent: 'center',
                      alignItems: 'center',
                    }}
                  >
                    <Typography variant="h6" gutterBottom>
                      Offline
                    </Typography>
                    <Typography variant="h4" fontWeight="bold" mt={1}>
                      {totalOffline}
                    </Typography>
                  </Stack>
                </Stack>
              </CardContent>
            </Card>
          </Container>
        </Stack>
        <Divider />
        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
          mt={4}
        >
          <Stat
            icon={<EvStationIcon />}
            iconColor="orange"
            value={totalVehicle}
            description="Total Vehicle"
          />
          <Stat
            icon={<LocalShippingIcon />}
            value={totalTrucks}
            description="Truck"
            iconColor="#009be5"
          />
          <Stat
            icon={<RvHookupIcon />}
            value={totalTrailers}
            description="Trailer"
            iconColor="#d32f2f"
          />
          <Stat
            icon={<AirportShuttleIcon />}
            value={totalVans}
            description="Van"
            iconColor="#ed6c02"
          />
          <Stat
            icon={<TwoWheelerIcon />}
            value={totalMotorcycles}
            description="Motorcycle"
            iconColor="#9c27b0"
          />
        </Stack>
      </CardContent>
    </Card>
  );
};

export default VehicleStatisticsCard;
