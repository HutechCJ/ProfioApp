'use client';

import React from 'react';

import { Container } from '@mui/material';

import StaffList from '@/app/(dash)/staffs/StaffList';
import StaffStatisticsCard from '@/app/(dash)/staffs/StaffStatisticsCard';
import MainTabs from '@/components/MainTabs';

const Staffs = () => {
  const tabs = [
    { label: 'LIST', content: <StaffList /> },
    { label: 'STATISTICS', content: <StaffStatisticsCard /> },
  ];

  return (
    <Container maxWidth="xl">
      <MainTabs tabs={tabs} />
    </Container>
  );
};

export default Staffs;
