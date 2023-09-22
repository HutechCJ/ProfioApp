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
  Grid,
} from '@mui/material';
import Link from '@/components/Link';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

import FormDialog from '@/components/FormDialog';
import EditVehicle from '../EditVehicle';
import DeleteVehicle from '../DeleteVehicle';
import VehicleStatusCard from './VehicleStatusCard';
import VehicleTypeCard from './VehicleTypeCard';
import VehicleStaffCard from './VehicleStaffCard';

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
          <Grid
            minHeight={128}
            container
            direction="row"
            spacing={2}
            columns={{ xs: 1, sm: 1, md: 3 }}
          >
            <Grid item xs={1}>
              <VehicleStatusCard status={status} />
            </Grid>
            <Grid item xs={1}>
              <VehicleTypeCard
                type={type}
                zipCodeCurrent={zipCodeCurrent}
                licensePlate={licensePlate}
              />
            </Grid>
            <Grid item xs={1}>
              <VehicleStaffCard staff={staff} />
            </Grid>
          </Grid>
        </CardContent>
      </Card>
    </Container>
  );
}

export default Vehicle;
