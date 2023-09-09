'use client';

import React from 'react';

import { Container, Divider } from '@mui/material';

import StaffList from '@/components/staff/StaffList';
import StaffStatisticsCard from '@/components/staff/StaffStatisticsCard';

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
