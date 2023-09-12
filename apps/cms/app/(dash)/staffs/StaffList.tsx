'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  Button,
  ButtonGroup,
  Divider,
} from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import InfoIcon from '@mui/icons-material/Info';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

import { Staff, StaffPosition } from '@/features/staff/staff.types';
import useGetStaffs from '@/features/staff/useGetStaffs';
import FormDialog from '@/components/FormDialog';
import StaffForm from './StaffForm';

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
    headerName: 'FULL NAME',
  },
  {
    field: 'phone',
    width: 250,
    headerName: 'PHONE',
  },
  {
    field: 'position',
    width: 200,
    headerName: 'POSITION',
    valueGetter: (params) => {
      const { position } = params.row;
      return `${StaffPosition[position]}`;
    },
  },
  {
    field: '',
    width: 400,
    headerName: 'ACTIONS',
    renderCell: (params) => {
      const staffId = params.row.id;
      return (
        <Stack
          direction="row"
          divider={<Divider orientation="vertical" flexItem />}
          spacing={2}
        >
          <Link href={`/staffs/${staffId}`}>
            <Button color="primary" startIcon={<InfoIcon />}>
              Details
            </Button>
          </Link>

          <FormDialog
            buttonText="Edit"
            buttonVariant="text"
            buttonColor="secondary"
            buttonIcon={<EditIcon />}
            dialogTitle="STAFF INFORMATION"
            dialogDescription={`ID: ${staffId}`}
            componentProps={(handleClose) => (
              <StaffForm onSuccess={handleClose} />
            )}
          />

          <FormDialog
            buttonText="Delete"
            buttonVariant="text"
            buttonColor="error"
            buttonIcon={<DeleteIcon />}
            dialogTitle="Are you sure you want to delete this STAFF?"
            dialogDescription={`ID: ${staffId}`}
            componentProps={(handleClose) => (
              <StaffForm onSuccess={handleClose} />
            )}
          />
        </Stack>
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
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingStaffs?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState,
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
          STAFF LIST
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
          <FormDialog
            buttonText="ADD STAFF"
            buttonVariant="contained"
            buttonColor="success"
            buttonIcon={<PersonAddIcon />}
            dialogTitle="ADD STAFF"
            dialogDescription="Please enter information for the staff"
            componentProps={(handleClose) => (
              <StaffForm
                onSuccess={() => {
                  refetch();
                  handleClose();
                }}
              />
            )}
          />
        </ButtonGroup>
      </Stack>
      <DataGrid
        columns={columns}
        rows={pagingStaffs?.data.items || []}
        rowCount={rowCountState}
        loading={isLoading}
        pageSizeOptions={[5, 10, 20, 50, 100]}
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
