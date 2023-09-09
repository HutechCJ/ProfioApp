'use client';

import React from 'react';

import {
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Stack,
  LinearProgress,
} from '@mui/material';

import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';

import Stat from '../../../components/Stat';
import useGetStaffs from '@/features/staff/useGetStaffs';
import { StaffPosition } from '@/features/staff/staff.types';

const StaffStatisticsCard = () => {
  const { data, isLoading, isError } = useGetStaffs();

  if (isLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
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
            icon={<LocalShippingIcon />}
            value={totalDrivers}
            description="Driver"
            iconColor="red"
          />
          <Stat
            icon={<MopedIcon />}
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
