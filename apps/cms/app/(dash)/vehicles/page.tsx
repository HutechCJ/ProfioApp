'use client';

import React from 'react';

import { Container } from '@mui/material';

import VehicleList from './VehicleList';

const Vehicles = () => {
  return (
    <Container maxWidth="xl">
      <VehicleList />
    </Container>
  );
};

export default Vehicles;
