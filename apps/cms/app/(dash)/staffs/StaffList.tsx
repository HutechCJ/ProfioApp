'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import { Box, Typography, Stack, ButtonGroup } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Staff, StaffPosition } from '@/features/staff/staff.types';
import useGetStaffs from '@/features/staff/useGetStaffs';
import FormDialog from '@/components/FormDialog';
import AddStaff from './AddStaff';
import EditStaff from './EditStaff';
import DeleteStaff from './DeleteStaff';
import ActionForList from '@/components/ActionForList';

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
          <ActionForList
            entityId={staffId}
            entity="staff"
            detailsLink={`/staffs/${staffId}`}
            editComponentProps={(handleClose) => (
              <EditStaff
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ staffId: staffId }}
              />
            )}
            deleteComponentProps={(handleClose) => (
              <DeleteStaff
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ staffId: staffId }}
              />
            )}
          />
        );
      },
    },
  ];

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
              <AddStaff
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
