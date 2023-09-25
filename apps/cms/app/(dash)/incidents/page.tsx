'use client';

import React from 'react';

import { Container } from '@mui/material';

import MainTabs from '@/components/MainTabs';
import IncidentList from './IncidentList';

const Incidents = () => {
  const tabs = [{ label: 'LIST', content: <IncidentList /> }];

  return (
    <Container maxWidth="xl">
      <MainTabs tabs={tabs} />
    </Container>
  );
};

export default Incidents;
