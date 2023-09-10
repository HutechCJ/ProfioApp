'use client';

import { OrderStatus } from '@/features/order/order.types';
import useGetOrder from '@/features/order/useGetOrder';
import { Box, Stack, Button } from '@mui/material';
import React from 'react';

function Order({ params }: { params: { orderId: string } }) {
  const { data: orderApiRes, isLoading, isError } = useGetOrder(params.orderId);

  if (isLoading) {
    return 'Loading...';
  }

  if (!orderApiRes || isError) {
    return 'There is an error occurred!';
  }

  return (
    <Box>
      <Stack direction="row" spacing={1} sx={{ mb: 1 }}>
        <Button>Update</Button>
        <Button>Delete</Button>
      </Stack>
      <div>{orderApiRes.data.id}</div>
      <div>Status: {OrderStatus[orderApiRes.data.status]}</div>
    </Box>
  );
}

export default Order;
