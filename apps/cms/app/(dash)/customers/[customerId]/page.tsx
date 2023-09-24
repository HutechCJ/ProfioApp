'use client';

import React from 'react';

import useGetCustomer from '@/features/customer/useGetCustomer';
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
  Avatar,
} from '@mui/material';
import Link from '@/components/Link';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import MaleIcon from '@mui/icons-material/Male';
import FemaleIcon from '@mui/icons-material/Female';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

import { Gender } from '@/features/customer/customer.types';
import FormDialog from '@/components/FormDialog';
import EditCustomer from '../EditCustomer';

import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';
import useGetCustomers from '@/features/customer/useGetCustomers';
import useDeleteCustomer from '@/features/customer/useDeleteCustomer';

function Customer({ params }: { params: { customerId: string } }) {
  const {
    data: customerApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetCustomer(params.customerId);

  const { refetch: refetchCustomers } = useGetCustomers();

  const { mutate: deleteCustomer, isSuccess } = useDeleteCustomer();

  const MySwal = withReactContent(Swal);

  React.useEffect(() => {
    if (isSuccess) {
      refetchCustomers();
      redirect('/customers');
    }
  }, [isSuccess, refetchCustomers]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !customerApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, name, phone, email, gender, address } = customerApiRes.data;

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
        deleteCustomer(id);
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
        <Link href="/customers">
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
            dialogTitle="CUSTOMER INFORMATION"
            dialogDescription={`ID: ${params.customerId}`}
            componentProps={(handleClose) => (
              <EditCustomer
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ customerId: params.customerId }}
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
              CUSTOMER INFORMATION
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
              {gender == 0 && (
                <Avatar sx={{ bgcolor: 'info', width: 100, height: 100 }}>
                  <MaleIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {gender == 1 && (
                <Avatar sx={{ bgcolor: 'success', width: 100, height: 100 }}>
                  <FemaleIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {gender == 2 && (
                <Avatar sx={{ bgcolor: 'warning', width: 100, height: 100 }}>
                  <MoreHorizIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              <Typography variant="h6">{Gender[gender]}</Typography>
            </Stack>
            <Stack>
              <Typography variant="h5" gutterBottom>
                Full Name: <strong>{name}</strong>
              </Typography>
              <Typography variant="h5" gutterBottom>
                Phone: <strong>{phone}</strong>
              </Typography>
              <Typography variant="h5" gutterBottom>
                Email: <strong>{email}</strong>
              </Typography>
              <Typography variant="body1" gutterBottom>
                Address:{' '}
                <strong>
                  {(() => {
                    const { street, ward, city, province } = address || {};
                    const addressParts = [];

                    if (street) addressParts.push(street);
                    if (ward) addressParts.push(ward);
                    if (city) addressParts.push(city);
                    if (province) addressParts.push(province);

                    return addressParts.join(', ');
                  })()}
                </strong>
              </Typography>
            </Stack>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
}

export default Customer;
