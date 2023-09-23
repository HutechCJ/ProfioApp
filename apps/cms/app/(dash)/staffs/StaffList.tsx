'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import { Box, Typography, Stack, ButtonGroup, Divider } from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Staff, StaffPosition } from '@/features/staff/staff.types';
import useGetStaffs from '@/features/staff/useGetStaffs';
import FormDialog from '@/components/FormDialog';
import AddStaff from './AddStaff';
import EditStaff from './EditStaff';
import ActionForList from '@/components/ActionForList';
import useCountByPosition from '@/features/staff/useCountByPosition';
import useDeleteStaff from '@/features/staff/useDeleteStaff';
import CopyTextButton from '@/components/CopyTextButton';

function StaffList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
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

  const { mutate: deleteStaff, isSuccess } = useDeleteStaff();

  const { refetch: refetchCountByPosition } = useCountByPosition();

  React.useEffect(() => {
    if (isSuccess) {
      refetchCountByPosition();
      refetch();
    }
  }, [isSuccess, refetchCountByPosition, refetch]);

  const rowCount = pagingStaffs?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Staff>[] = [
    {
      field: 'id',
      headerName: 'ID',
      headerAlign: 'center',
      align: 'center',
      width: 250,
      renderCell(params) {
        const maxLength = 6;
        const truncatedValue =
          params.value.length > maxLength
            ? params.value.slice(0, 8) + '...'
            : params.value;

        return (
          <>
            <CopyTextButton text={params.value} />
            <Link href={`/staffs/${params.value}`}>
              <Typography variant="button">{truncatedValue}</Typography>
            </Link>
          </>
        );
      },
    },
    {
      field: 'name',
      width: 300,
      headerName: 'FULL NAME',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'phone',
      width: 250,
      headerName: 'PHONE',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'position',
      width: 250,
      headerName: 'POSITION',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { position } = params.row;
        return `${StaffPosition[position]}`;
      },
    },
    {
      field: 'actions',
      width: 320,
      headerName: 'ACTIONS',
      headerAlign: 'center',
      align: 'center',
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
            handleDelete={() => deleteStaff(staffId)}
          />
        );
      },
    },
  ];

  return (
    <Box>
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
          backgroundColor: 'white',
          width: '100%',
        }}
        slots={{
          toolbar: (props) => (
            <>
              <GridToolbar {...props} />
              <Divider />
            </>
          ),
        }}
      />
    </Box>
  );
}

export default StaffList;
