'use client';

import React from 'react';

import { Container, Divider } from '@mui/material';

import VehicleList from './VehicleList';
import VehicleStatisticsCard from './VehicleStatisticsCard';

const Vehicles = () => {
  return (
    <Container maxWidth="xl">
      <VehicleStatisticsCard />
      <Divider />
      <VehicleList />
    </Container>
  );
};

export default Vehicles;
