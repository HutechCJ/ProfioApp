'use client';

import React from 'react';

import { Container } from '@mui/material';

import StaffList from '@/components/staff/StaffList';

const Staffs = () => {
  return (
    <Container maxWidth="xl">
      <StaffList />
    </Container>
  );
};

export default Staffs;
