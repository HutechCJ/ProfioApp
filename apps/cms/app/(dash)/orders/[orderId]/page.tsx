/* eslint-disable react/no-children-prop */
'use client';

import React from 'react';

import useGetOrder from '@/features/order/useGetOrder';
import {
  Stack,
  Button,
  Container,
  Typography,
  Card,
  CardHeader,
  Divider,
  CardContent,
  ButtonGroup,
  Grid,
} from '@mui/material';

import _ from 'lodash';
import GoogleMapComponent from './GoogleMap';
import OrderStepper from './OrderStepper';
import Link from 'next/link';
import FormDialog from '@/components/FormDialog';
import EditOrder from '../EditOrder';
import DeleteOrder from '../DeleteOrder';
import { redirect } from 'next/navigation';
import OrderStatusCard from './OrderStatusCard';
import OrderCustomerCard from './OrderCustomerCard';
import OrderDetailsCard from './OrderDetailsCard';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

function Order({ params }: { params: { orderId: string } }) {
  const {
    data: orderApiRes,
    isLoading: orderLoading,
    isError: orderError,
    refetch: orderRefetch,
  } = useGetOrder(params.orderId);

  if (orderLoading) {
    return 'Loading...';
  }

  if (!orderApiRes || orderError) {
    return 'There is an error occurred!';
  }

  return (
    <Container maxWidth="xl" sx={{ '& > :not(style)': { m: 2 } }}>
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Link href="/orders">
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
            dialogTitle="ORDER INFORMATION"
            dialogDescription={`ID: ${params.orderId}`}
            componentProps={(handleClose) => (
              <EditOrder
                onSuccess={() => {
                  handleClose();
                  orderRefetch();
                }}
                params={{ orderId: params.orderId }}
              />
            )}
          />

          <FormDialog
            buttonText="Delete"
            buttonColor="error"
            buttonIcon={<DeleteIcon />}
            dialogTitle="Are you sure you want to delete this ORDER?"
            dialogDescription={`ID: ${params.orderId}`}
            componentProps={(handleClose) => (
              <DeleteOrder
                onSuccess={() => {
                  handleClose();
                  orderRefetch();
                  redirect('/orders');
                }}
                params={{ orderId: params.orderId }}
              />
            )}
          />
        </ButtonGroup>
      </Stack>

      <Card sx={{ marginY: 4 }}>
        <CardHeader
          title={
            <Typography variant="h4" fontWeight="bold">
              ORDER INFORMATION
            </Typography>
          }
          subheader={`ID: ${orderApiRes.data.id}`}
        />

        <Divider />

        <CardContent>
          <Grid
            minHeight={128}
            container
            direction="row"
            spacing={1}
            columns={{ xs: 1, sm: 6, md: 12 }}
            mb={2}
          >
            <Grid item xs={3}>
              <OrderStatusCard status={orderApiRes.data.status} />
            </Grid>
            <Grid item xs={3}>
              <OrderCustomerCard customer={orderApiRes.data.customer} />
            </Grid>
            <Grid item xs={6}>
              <OrderDetailsCard
                startedDate={orderApiRes.data.startedDate}
                expectedDeliveryTime={orderApiRes.data.expectedDeliveryTime}
                destinationAddress={orderApiRes.data.destinationAddress}
                destinationZipCode={orderApiRes.data.destinationZipCode}
                note={orderApiRes.data.note}
                distance={orderApiRes.data.distance}
              />
            </Grid>
          </Grid>
        </CardContent>
      </Card>

      <Divider />

      <Typography variant="h4" component="h4">
        {`Order #${orderApiRes.data.id}`}
      </Typography>
      <OrderStepper order={orderApiRes.data} />
      <GoogleMapComponent orderId={params.orderId} />
    </Container>
  );
}

export default Order;
