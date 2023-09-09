'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import { Box, Typography, Stack, Button, ButtonGroup } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Staff, StaffPosition } from '@/features/staff/staff.types';
import useGetStaffs from '@/features/staff/useGetStaffs';

const columns: GridColDef<Staff>[] = [
  {
    field: 'id',
    headerName: 'ID',
    width: 300,
    renderCell(params) {
      return (
        <Link href={`/staffs/${params.value}`}>
          <Typography variant="button">{params.value}</Typography>
        </Link>
      );
    },
  },
  {
    field: 'name',
    width: 300,
    headerName: 'HỌ TÊN',
  },
  {
    field: 'phone',
    width: 250,
    headerName: 'SỐ ĐIỆN THOẠI',
  },
  {
    field: 'position',
    width: 200,
    headerName: 'CHỨC VỤ',
    valueGetter: (params) => {
      const { position } = params.row;
      return `${StaffPosition[position]}`;
    },
  },
  {
    field: '',
    width: 400,
    headerName: 'HÀNH ĐỘNG',
    renderCell: (params) => {
      //   const staffId = params.row.id;
      return (
        <ButtonGroup variant="outlined">
          <Button color="primary">Xem Chi Tiết</Button>
          <Button color="success">Chỉnh Sửa</Button>
          <Button color="error">Xoá</Button>
        </ButtonGroup>
      );
    },
  },
];

function StaffList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 5,
  });

  const {
    data: pagingStaffs,
    isLoading,
    refetch,
    remove,
  } = useGetStaffs({
    PageNumber: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingStaffs?.data.totalPages || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState
    );
  }, [rowCount, setRowCountState]);

  return (
    <Box sx={{ paddingY: 4 }}>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        marginBottom={2}
      >
        <Typography variant="h5" fontWeight="bold">
          DANH SÁCH NHÂN VIÊN
        </Typography>
        <ButtonGroup variant="text" aria-label="button-group-reload-and-create">
          <LoadingButton
            onClick={() => {
              remove();
              refetch();
            }}
            variant="contained"
            startIcon={<ReplayIcon />}
          >
            RELOAD
          </LoadingButton>
          <Button
            variant="contained"
            color="success"
            startIcon={<PersonAddIcon />}
          >
            THÊM NHÂN VIÊN
          </Button>
        </ButtonGroup>
      </Stack>
      <DataGrid
        columns={columns}
        rows={pagingStaffs?.data.items || []}
        rowCount={rowCountState}
        loading={isLoading}
        pageSizeOptions={[1, 5, 10]}
        paginationModel={paginationModel}
        paginationMode="server"
        onPaginationModelChange={setPaginationModel}
        sx={{
          boxShadow: '0 2px 4px rgba(22, 22, 22, 0.5)',
          backgroundColor: 'white',
        }}
      />
    </Box>
  );
}

export default StaffList;
