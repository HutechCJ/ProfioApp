'use client';

import React from 'react';

import { StaffPosition } from '@/features/staff/staff.types';
import useGetStaff from '@/features/staff/useGetStaff';
import {
  Container,
  Card,
  CardHeader,
  CardContent,
  Typography,
  Stack,
  ButtonGroup,
  Button,
  Divider,
  Avatar,
} from '@mui/material';

import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import MopedIcon from '@mui/icons-material/Moped';
import Link from '@/components/Link';

function Staff({ params }: { params: { staffId: string } }) {
  const { data: staffApiRes, isLoading, isError } = useGetStaff(params.staffId);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !staffApiRes) {
    return <div>There was an error!</div>;
  }

  const { id, name, phone, position } = staffApiRes.data;

  return (
    <Container maxWidth="xl">
      <Stack direction="row" justifyContent="space-between" alignItems="center">
        <Link href="/staffs">
          <Button
            variant="contained"
            color="primary"
            startIcon={<ArrowBackIcon />}
          >
            VỀ DANH SÁCH
          </Button>
        </Link>
        <ButtonGroup variant="contained">
          <Button color="success">Chỉnh Sửa</Button>
          <Button color="error">Xoá</Button>
        </ButtonGroup>
      </Stack>

      <Card sx={{ marginY: 4 }}>
        <CardHeader
          title={
            <Typography variant="h4" fontWeight="bold">
              THÔNG TIN NHÂN VIÊN
            </Typography>
          }
          subheader={`ID: ${id}`}
        />

        <Divider />

        <CardContent>
          <Stack
            direction="row"
            justifyContent="flex-start"
            alignItems="center"
            spacing={4}
          >
            <Stack justifyContent="center" alignItems="center" spacing={1}>
              {position == 0 && (
                <Avatar sx={{ bgcolor: 'red', width: 100, height: 100 }}>
                  <LocalShippingIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              {position == 1 && (
                <Avatar sx={{ bgcolor: 'blue', width: 100, height: 100 }}>
                  <MopedIcon sx={{ width: 60, height: 60 }} />
                </Avatar>
              )}
              <Typography variant="h6">{StaffPosition[position]}</Typography>
            </Stack>
            <Stack>
              <Typography variant="h5" gutterBottom>
                Họ Tên: <strong>{name}</strong>
              </Typography>
              <Typography variant="h5" gutterBottom>
                Số Điện Thoại: <strong>{phone}</strong>
              </Typography>
            </Stack>
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
}

export default Staff;
