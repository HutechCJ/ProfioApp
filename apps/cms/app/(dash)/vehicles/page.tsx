'use client';

import React from 'react';

import { Container } from '@mui/material';

import VehicleList from './VehicleList';
import VehicleStatisticsCard from './VehicleStatisticsCard';
import MainTabs from '@/components/MainTabs';

const Vehicles = () => {
  const tabs = [
    { label: 'LIST', content: <VehicleList /> },
    { label: 'STATISTICS', content: <VehicleStatisticsCard /> },
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

export default Vehicles;
