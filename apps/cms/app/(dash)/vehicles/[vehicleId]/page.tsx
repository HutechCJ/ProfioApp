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
  LinearProgress,
} from '@mui/material';
import Link from '@/components/Link';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

import FormDialog from '@/components/FormDialog';
import EditVehicle from '../EditVehicle';
import VehicleStatusCard from './VehicleStatusCard';
import VehicleTypeCard from './VehicleTypeCard';
import VehicleStaffCard from './VehicleStaffCard';

import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import useGetVehicles from '@/features/vehicle/useGetVehicles';
import useDeleteVehicle from '@/features/vehicle/useDeleteVehicle';
import useCountByVehicleType from '@/features/vehicle/useCountByVehicleType';
import useCountByVehicleStatus from '@/features/vehicle/useCountByVehicleStatus';
import { Box } from '@mui/system';

function Vehicle({ params }: { params: { vehicleId: string } }) {
  const {
    data: vehicleApiRes,
    isLoading,
    isError,
    refetch: refetchVehicle,
  } = useGetVehicle(params.vehicleId);

  const { refetch: refetchVehicles } = useGetVehicles();

  const { mutate: deleteVehicle, isSuccess } = useDeleteVehicle();

  const { refetch: refetchCountType } = useCountByVehicleType();
  const { refetch: refetchCountStatus } = useCountByVehicleStatus();

  const MySwal = withReactContent(Swal);

  React.useEffect(() => {
    if (isSuccess) {
      refetchCountStatus();
      refetchCountType();
      refetchVehicles();
      redirect('/vehicles');
    }
  }, [isSuccess, refetchCountStatus, refetchCountType, refetchVehicles]);

  if (isLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isError || !vehicleApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, zipCodeCurrent, licensePlate, type, status, staff } =
    vehicleApiRes.data;

  const handleDelete = () => {
    MySwal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#007dc3',
      cancelButtonColor: '#d32f2f',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        deleteVehicle(id);
        MySwal.fire({
          title: 'Deleted!',
          text: 'Your data has been deleted.',
          icon: 'success',
          confirmButtonColor: '#007dc3',
          confirmButtonText: 'OK',
        });
      }
    });
  };

  return (
    <Container maxWidth="xl">
      <Stack
        direction={{ sm: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems={{ sm: 'flex-start', md: 'center' }}
        spacing={{ xs: 1 }}
        mt={1}
      >
        <Link href="/vehicles">
          <Button
            variant="contained"
            color="primary"
            startIcon={<ArrowBackIcon />}
          >
            BACK TO LIST
          </Button>
        </Link>
        <ButtonGroup variant="text">
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
                  refetchVehicle();
                }}
                params={{ vehicleId: params.vehicleId }}
              />
            )}
          />

          <Button
            onClick={handleDelete}
            variant="contained"
            startIcon={<DeleteIcon />}
            color="error"
          >
            Delete
          </Button>
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
