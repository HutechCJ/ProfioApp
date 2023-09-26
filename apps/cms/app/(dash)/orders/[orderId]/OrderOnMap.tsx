/* eslint-disable react/no-children-prop */
'use client';

import React from 'react';

import useGetOrder from '@/features/order/useGetOrder';
import { Typography, Box, LinearProgress, Button, Stack } from '@mui/material';

import GoogleMapComponent from './GoogleMap';
import OrderStepper from './OrderStepper';
import Link from '@/components/Link';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

function OrderOnMap({ params }: { params: { orderId: string } }) {
  const {
    data: orderApiRes,
    isLoading: orderLoading,
    isError: orderError,
  } = useGetOrder(params.orderId);

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

  return (
    <>
      <Stack
        direction={{ sm: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems={{ sm: 'flex-start', md: 'center' }}
        spacing={{ xs: 1 }}
        mt={1}
      >
        <Typography variant="h4" fontWeight="bold">
          Order Tracking via Hubs
        </Typography>
        <Link href="/orders">
          <Button
            variant="contained"
            color="primary"
            startIcon={<ArrowBackIcon />}
          >
            BACK TO LIST
          </Button>
        </Link>
      </Stack>
      <OrderStepper order={orderApiRes.data} />
      <GoogleMapComponent orderId={params.orderId} />
    </>
  );
}

export default OrderOnMap;
