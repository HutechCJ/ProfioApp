'use client';

import React from 'react';

import { Container, Divider } from '@mui/material';

import StaffList from '@/app/(dash)/staffs/StaffList';
import StaffStatisticsCard from '@/app/(dash)/staffs/StaffStatisticsCard';

const Staffs = () => {
  return (
    <Container maxWidth="xl">
      <StaffStatisticsCard />
      <Divider />
      <StaffList />
    </Container>
  );
};

export default Staffs;
