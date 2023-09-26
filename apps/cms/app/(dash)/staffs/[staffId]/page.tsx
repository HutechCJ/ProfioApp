'use client';

import React from 'react';

import Link from '@/components/Link';
import useGetStaff from '@/features/staff/useGetStaff';
import {
  Avatar,
  Box,
  Button,
  ButtonGroup,
  Card,
  CardContent,
  CardHeader,
  Container,
  Divider,
  LinearProgress,
  Stack,
  Typography,
} from '@mui/material';
import { redirect } from 'next/navigation';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import InventoryIcon from '@mui/icons-material/Inventory';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';

import FormDialog from '@/components/FormDialog';
import { StaffPosition } from '@/features/staff/staff.types';
import EditStaff from '../EditStaff';

import useSwal from '@/common/hooks/useSwal';
import useCountByPosition from '@/features/staff/useCountByPosition';
import useDeleteStaff from '@/features/staff/useDeleteStaff';
import useGetStaffs from '@/features/staff/useGetStaffs';

function Staff({ params }: { params: { staffId: string } }) {
  const {
    data: staffApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetStaff(params.staffId);

  const { refetch: refetchStaffs } = useGetStaffs();

  const { mutate: deleteStaff, isSuccess } = useDeleteStaff();

  const { refetch: refetchCountByPosition } = useCountByPosition();

  const MySwal = useSwal();

  React.useEffect(() => {
    if (isSuccess) {
      refetchCountByPosition();
      refetchStaffs();
      redirect('/staffs');
    }
  }, [isSuccess, refetchCountByPosition, refetchStaffs]);

  if (isLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isError || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, name, phone, position } = staffApiRes.data;

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
        deleteStaff(id);
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
        <Link href="/staffs">
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
            dialogTitle="STAFF INFORMATION"
            dialogDescription={`ID: ${params.staffId}`}
            componentProps={(handleClose) => (
              <EditStaff
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ staffId: params.staffId }}
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
              STAFF INFORMATION
            </Typography>
          }
          subheader={`ID: ${id}`}
        />

        <Divider />

        <CardContent>
          <Stack
            direction="row"
            justifyContent="flex-start"
            alignItems="center"
            spacing={4}
          >
            <Stack justifyContent="center" alignItems="center" spacing={1}>
              {position == 0 && (
                <Avatar sx={{ bgcolor: 'red', width: 100, height: 100 }}>
                  <LocalShippingIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {position == 1 && (
                <Avatar sx={{ bgcolor: 'blue', width: 100, height: 100 }}>
                  <MopedIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {position == 2 && (
                <Avatar sx={{ bgcolor: 'green', width: 100, height: 100 }}>
                  <BusinessCenterIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {position == 3 && (
                <Avatar sx={{ bgcolor: 'brown', width: 100, height: 100 }}>
                  <InventoryIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              <Typography variant="h6">{StaffPosition[position]}</Typography>
            </Stack>
            <Stack>
              <Typography variant="h5" gutterBottom>
                Full Name: <strong>{name}</strong>
              </Typography>
              <Typography variant="h5" gutterBottom>
                Phone: <strong>{phone}</strong>
              </Typography>
            </Stack>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
}

export default Staff;
