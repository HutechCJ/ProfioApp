'use client';

import { StaffPosition } from '@/features/staff/staff.types';
import useGetStaff from '@/features/staff/useGetStaff';
import { Box } from '@mui/material';
import React from 'react';

function Staff({ params }: { params: { staffId: string } }) {
  const { data: staffApiRes, isLoading, error } = useGetStaff(params.staffId);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, name, phone, position } = staffApiRes.data;

  return (
    <Box>
      <div>{id}</div>
      <div>{name}</div>
      <div>{phone}</div>
      <div>{StaffPosition[position]}</div>
    </Box>
  );
}

export default Staff;
