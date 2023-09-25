'use client';

import React from 'react';

import { Container } from '@mui/material';

import MainTabs from '@/components/MainTabs';
import HubList from './HubList';

const Hubs = () => {
  const tabs = [{ label: 'LIST', content: <HubList /> }];

  return (
    <Container maxWidth="xl">
      <MainTabs tabs={tabs} />
    </Container>
  );
};

export default Hubs;
