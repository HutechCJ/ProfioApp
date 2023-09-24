'use client';

import React from 'react';

import { Container } from '@mui/material';

import CustomerList from '@/app/(dash)/customers/CustomerList';
import MainTabs from '@/components/MainTabs';

const Customers = () => {
  const tabs = [
    { label: 'LIST', content: <CustomerList /> },
    {
      label: 'STATISTICS',
      content:
        'The service to view statistics is currently unavailable. Coming soon!',
    },
    {
      label: 'LOGS',
      content:
        'The service to view logs is currently unavailable. Coming soon!',
    },
  ];

  return (
    <Container maxWidth="xl">
      <MainTabs tabs={tabs} />
    </Container>
  );
};

export default Customers;
