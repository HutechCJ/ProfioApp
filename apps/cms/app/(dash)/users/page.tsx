'use client';

import React from 'react';

import { Container } from '@mui/material';

import UserList from './UserList';

const Users = () => {
  return (
    <Container maxWidth="xl">
      <UserList />
    </Container>
  );
};

export default Users;
