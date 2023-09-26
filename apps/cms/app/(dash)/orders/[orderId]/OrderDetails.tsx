/* eslint-disable react/no-children-prop */
'use client';

import React from 'react';

import useGetOrder from '@/features/order/useGetOrder';
import {
  Box,
  Button,
  ButtonGroup,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Grid,
  LinearProgress,
  Stack,
  Typography,
} from '@mui/material';

import FormDialog from '@/components/FormDialog';
import Link from 'next/link';
import { redirect } from 'next/navigation';
import EditOrder from '../EditOrder';
import OrderCustomerCard from './OrderCustomerCard';
import OrderDetailsCard from './OrderDetailsCard';
import OrderStatusCard from './OrderStatusCard';

import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';
import useDeleteOrder from '@/features/order/useDeleteOrder';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import useSwal from '@/common/hooks/useSwal';
import useGetOrders from '@/features/order/useGetOrders';

function OrderDetails({ params }: { params: { orderId: string } }) {
  const {
    data: orderApiRes,
    isLoading: orderLoading,
    isError: orderError,
    refetch: orderRefetch,
  } = useGetOrder(params.orderId);

  const { refetch: refetchOrders } = useGetOrders();

  const { mutate: deleteOrder, isSuccess } = useDeleteOrder();

  const { refetch: refetchCount } = useCountByOrderStatus();

  const MySwal = useSwal();

  React.useEffect(() => {
    if (isSuccess) {
      refetchCount();
      refetchOrders();
      redirect('/orders');
    }
  }, [isSuccess, refetchCount, refetchOrders]);

  if (orderLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (!orderApiRes || orderError) {
    return 'There is an error occurred!';
  }

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
        deleteOrder(orderApiRes.data.id);
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
    <>
      <Stack
        direction={{ sm: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems={{ sm: 'flex-start', md: 'center' }}
        spacing={{ xs: 1 }}
        mt={1}
      >
        <Link href="/orders">
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
            columns={{ xs: 1, sm: 6, md: 6, xl: 12 }}
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
    </>
  );
}

export default OrderDetails;
