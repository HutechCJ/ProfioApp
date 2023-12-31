'use client';

import React from 'react';

import { Container } from '@mui/material';
import OrderList from './OrderList';
import OrderStatisticsCard from './OrderStatisticsCard';
import MainTabs from '@/components/MainTabs';

const Orders = () => {
  const tabs = [
    { label: 'LIST', content: <OrderList /> },
    { label: 'STATISTICS', content: <OrderStatisticsCard /> },
    // {
    //   label: 'LOGS',
    //   content:
    //     'The service to view logs is currently unavailable. Coming soon!',
    // },
  ];

  return (
    <Container maxWidth="xl">
      <MainTabs tabs={tabs} />
    </Container>
  );
};

export default Orders;
