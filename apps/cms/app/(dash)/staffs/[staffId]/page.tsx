'use client';

import React from 'react';

import { StaffPosition } from '@/features/staff/staff.types';
import useGetStaff from '@/features/staff/useGetStaff';
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
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import FormDialog from '@/components/FormDialog';
import StaffForm from '../StaffForm';

function Staff({ params }: { params: { staffId: string } }) {
  const { data: staffApiRes, isLoading, isError } = useGetStaff(params.staffId);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, name, phone, position } = staffApiRes.data;

  return (
    <Container maxWidth="xl">
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Link href="/staffs">
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
            dialogTitle="STAFF INFORMATION"
            dialogDescription={`ID: ${params.staffId}`}
            componentProps={(handleClose) => (
              <StaffForm onSuccess={handleClose} />
            )}
          />

          <FormDialog
            buttonText="Delete"
            buttonColor="error"
            buttonIcon={<DeleteIcon />}
            dialogTitle="Are you sure you want to delete this STAFF?"
            dialogDescription={`ID: ${params.staffId}`}
            componentProps={(handleClose) => (
              <StaffForm onSuccess={handleClose} />
            )}
          />
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
