'use client';

import React from 'react';

import { Container, Divider } from '@mui/material';
import OrderList from './OrderList';

const Orders = () => {
  return (
    <Container maxWidth="xl">
      {/* <OrderStatisticsCard /> */}
      <Divider />
      <OrderList />
    </Container>
  );
};

export default Orders;