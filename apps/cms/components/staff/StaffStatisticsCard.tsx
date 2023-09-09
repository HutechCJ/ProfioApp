'use client';

import React from 'react';

import { Card, CardHeader, CardContent, Divider, Stack } from '@mui/material';

import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import DriveEtaIcon from '@mui/icons-material/DriveEta';

import Stat from '../Stat';
import useGetStaffs from '@/features/staff/useGetStaffs';
import { StaffPosition } from '@/features/staff/staff.types';

const StaffStatisticsCard = () => {
  const { data, isLoading, isError } = useGetStaffs();

  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (isError) {
    return <p>Error loading staff data.</p>;
  }

  const staffData = data?.data?.items || [];
  const totalStaff = staffData.length || 0;
  const totalDrivers =
    staffData.filter((staff) => staff.position === StaffPosition.Driver)
      .length || 0;
  const totalShippers =
    staffData.filter((staff) => staff.position === StaffPosition.Shipper)
      .length || 0;

  return (
    <Card sx={{ marginBottom: 4 }}>
      <CardHeader title="NHÂN VIÊN" subheader="Số liệu thống kê" />
      <Divider />
      <CardContent sx={{ marginY: 4 }}>
        <Stack
          direction="row"
          justifyContent="space-around"
          alignItems="center"
        >
          <Stat
            icon={<PeopleIcon />}
            iconColor="orange"
            value={totalStaff}
            description="Tổng Số Nhân Viên"
          />
          <Stat
            icon={<DriveEtaIcon />}
            value={totalDrivers}
            description="Driver"
            iconColor="red"
          />
          <Stat
            icon={<LocalShippingIcon />}
            value={totalShippers}
            description="Shipper"
            iconColor="blue"
          />
        </Stack>
      </CardContent>
    </Card>
  );
};

export default StaffStatisticsCard;
