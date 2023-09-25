'use client';

import React from 'react';

// import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  ButtonGroup,
  Divider,
  TextField,
  MenuItem,
  Button,
} from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';

import useGetUsers from '@/features/user/useGetUsers';
import CopyTextButton from '@/components/CopyTextButton';
import { User } from '@/features/user/user.types';

import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import FormDialog from '@/components/FormDialog';
import Link from '@/components/Link';
import { StaffPosition } from '@/features/staff/staff.types';

function UserList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingUsers,
    isLoading,
    refetch,
    remove,
  } = useGetUsers({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingUsers?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState,
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<User>[] = [
    {
      field: 'id',
      headerName: 'ID',
      headerAlign: 'center',
      align: 'center',
      width: 100,
      renderCell(params) {
        const maxLength = 6;
        const truncatedValue =
          params.value.length > maxLength
            ? params.value.slice(0, 5) + '...'
            : params.value;
        return (
          <>
            <CopyTextButton text={params.value} />
            {/* <Link href={`/users/${params.value}`}> */}
            <Typography variant="button">{truncatedValue}</Typography>
            {/* </Link> */}
          </>
        );
      },
    },
    {
      field: 'userName',
      width: 250,
      headerName: 'USERNAME',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'email',
      width: 250,
      headerName: 'EMAIL',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'fullName',
      width: 250,
      headerName: 'FULL NAME',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'staff',
      width: 250,
      headerName: 'STAFF POSITION',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { staff } = params.row;
        return staff?.position ? StaffPosition[staff.position] : null;
      },
      renderCell(params) {
        const { staff } = params.row;
        return (
          <Link href={`/staffs/${staff?.id}`} sx={{ color: 'black' }}>
            <Typography variant="button">
              {staff?.position ? StaffPosition[staff.position] : null}
            </Typography>
          </Link>
        );
      },
    },
    {
      field: 'accessLevel',
      width: 320,
      headerName: 'ACCESS LEVEL',
      headerAlign: 'center',
      align: 'center',
      renderCell: (params) => {
        const userFullName = params.row.fullName;
        return (
          <FormDialog
            buttonText="ADMIN"
            buttonVariant="contained"
            buttonColor="success"
            buttonIcon={<AdminPanelSettingsIcon />}
            dialogTitle="PERMISSION"
            dialogDescription={`Change permission for ${userFullName}`}
            componentProps={(handleClose) => (
              <Box
                component="form"
                // onSubmit={handleSubmit}
                noValidate
                sx={{ mt: 1, width: 500 }}
              >
                <TextField
                  margin="dense"
                  required
                  fullWidth
                  select
                  defaultValue="ADMIN"
                  variant="filled"
                  id="accessLevel"
                  name="accessLevel"
                  label="Access Level"
                  value="ADMIN"
                  //   onChange={handleChangeInput}
                >
                  <MenuItem key="ADMIN" value="ADMIN">
                    ADMIN
                  </MenuItem>
                  <MenuItem key="MANAGER" value="MANAGER">
                    MANAGER
                  </MenuItem>
                </TextField>

                <Button
                  //   type="submit"
                  fullWidth
                  variant="contained"
                  sx={{ my: 2 }}
                  onClick={() => {
                    handleClose();
                  }}
                >
                  CONFIRM
                </Button>
              </Box>
            )}
          />
        );
      },
    },
  ];

  return (
    <Box>
      <Stack
        direction={{ sm: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems={{ sm: 'flex-start', md: 'center' }}
        spacing={{ md: 1 }}
        marginY={2}
      >
        <Stack>
          <Typography variant="h5" fontWeight="bold">
            USER
          </Typography>
          <Typography variant="body1" color="gray" gutterBottom>
            Managing permissions for users
          </Typography>
        </Stack>
        <ButtonGroup variant="text" aria-label="button-group-reload-and-create">
          <LoadingButton
            onClick={() => {
              remove();
              refetch();
            }}
            variant="contained"
            startIcon={<ReplayIcon />}
            loading={isLoading}
          >
            RELOAD
          </LoadingButton>
        </ButtonGroup>
      </Stack>
      <DataGrid
        columns={columns}
        rows={pagingUsers?.data.items || []}
        rowCount={rowCountState}
        loading={isLoading}
        pageSizeOptions={[5, 10, 20, 50, 100]}
        paginationModel={paginationModel}
        paginationMode="server"
        onPaginationModelChange={setPaginationModel}
        sx={{
          backgroundColor: 'white',
          width: '100%',
          height: 668,
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

export default UserList;
