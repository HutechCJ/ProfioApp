'use client';

import React from 'react';

import useGetVehicle from '@/features/vehicle/useGetVehicle';
import { redirect } from 'next/navigation';
import {
  Container,
  Card,
  CardHeader,
  CardContent,
  Typography,
  Stack,
  ButtonGroup,
  Button,
  Divider,
} from '@mui/material';
import Link from '@/components/Link';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import AlarmIcon from '@mui/icons-material/Alarm';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import RvHookupIcon from '@mui/icons-material/RvHookup';
import AirportShuttleIcon from '@mui/icons-material/AirportShuttle';
import TwoWheelerIcon from '@mui/icons-material/TwoWheeler';
import PersonPinIcon from '@mui/icons-material/PersonPin';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

import { VehicleType, VehicleStatus } from '@/features/vehicle/vehicle.types';
import FormDialog from '@/components/FormDialog';
import EditVehicle from '../EditVehicle';
import DeleteVehicle from '../DeleteVehicle';

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

function Vehicle({ params }: { params: { vehicleId: string } }) {
  const {
    data: vehicleApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetVehicle(params.vehicleId);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !vehicleApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, zipCodeCurrent, licensePlate, type, status, staff } =
    vehicleApiRes.data;

  return (
    <Container maxWidth="xl">
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Link href="/vehicles">
          <Button
            variant="contained"
            color="primary"
            startIcon={<ArrowBackIcon />}
          >
            BACK TO LIST
          </Button>
        </Link>
        <ButtonGroup variant="contained">
          <FormDialog
            buttonText="Edit"
            buttonColor="secondary"
            buttonIcon={<EditIcon />}
            dialogTitle="VEHICLE INFORMATION"
            dialogDescription={`ID: ${params.vehicleId}`}
            componentProps={(handleClose) => (
              <EditVehicle
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ vehicleId: params.vehicleId }}
              />
            )}
          />

          <FormDialog
            buttonText="Delete"
            buttonColor="error"
            buttonIcon={<DeleteIcon />}
            dialogTitle="Are you sure you want to delete this VEHICLE?"
            dialogDescription={`ID: ${params.vehicleId}`}
            componentProps={(handleClose) => (
              <DeleteVehicle
                onSuccess={() => {
                  handleClose();
                  refetch();
                  redirect('/vehicles');
                }}
                params={{ vehicleId: params.vehicleId }}
              />
            )}
          />
        </ButtonGroup>
      </Stack>

      <Card sx={{ marginY: 4 }}>
        <CardHeader
          title={
            <Typography variant="h4" fontWeight="bold">
              VEHICLE INFORMATION
            </Typography>
          }
          subheader={`ID: ${id}`}
        />

        <Divider />

        <CardContent>
          <Stack
            direction="row"
            justifyContent="space-around"
            alignItems="center"
          >
            <Container>
              <Card
                sx={{
                  minHeight: 275,
                  height: 320,
                  bgcolor:
                    statuses.find((s) => s.value === status)?.color ||
                    '#e2e2e2',
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
                    <Typography
                      variant="h4"
                      fontWeight="bold"
                      mt={1}
                      gutterBottom
                    >
                      {VehicleStatus[status]}
                    </Typography>
                  </Stack>
                </CardContent>
              </Card>
            </Container>
            <Container>
              <Card
                sx={{
                  minHeight: 275,
                  height: 320,
                  bgcolor:
                    types.find((s) => s.value === type)?.color || '#e2e2e2',
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
            <Container>
              <Link
                href={`/staffs/${staff?.id}`}
                sx={{ textDecoration: 'none' }}
              >
                <Card
                  sx={{
                    minHeight: 275,
                    height: 320,
                    bgcolor: `rgba(0, 128, 0, 0.75)`,
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
                        Staff
                      </Typography>
                      <PersonPinIcon sx={{ width: 150, height: 150 }} />
                      <Typography variant="h4" fontWeight="bold" gutterBottom>
                        {staff?.name}
                      </Typography>
                      <Typography variant="body1" color="#575757" gutterBottom>
                        {staff?.id}
                      </Typography>
                    </Stack>
                  </CardContent>
                </Card>
              </Link>
            </Container>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
}

export default Vehicle;
