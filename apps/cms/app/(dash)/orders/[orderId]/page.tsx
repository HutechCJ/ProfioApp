/* eslint-disable react/no-children-prop */
'use client';

import React from 'react';

import { Container } from '@mui/material';

import MainTabs from '@/components/MainTabs';
import OrderDetails from './OrderDetails';
import OrderOnMap from './OrderOnMap';

function Order({ params }: { params: { orderId: string } }) {
  const tabs = [
    { label: 'MAP', content: <OrderOnMap params={params} /> },
    { label: 'DETAILS', content: <OrderDetails params={params} /> },
  ];

  return (
    <Container maxWidth="xl" sx={{ '& > :not(style)': { m: 2 } }}>
      <MainTabs tabs={tabs} />
    </Container>
  );
}

export default Order;
