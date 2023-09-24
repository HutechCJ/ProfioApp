'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  ButtonGroup,
  Divider,
  Chip,
} from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Customer, Gender } from '@/features/customer/customer.types';
import useGetCustomers from '@/features/customer/useGetCustomers';
import FormDialog from '@/components/FormDialog';
import AddCustomer from './AddCustomer';
import EditCustomer from './EditCustomer';
import ActionForList from '@/components/ActionForList';
import useDeleteCustomer from '@/features/customer/useDeleteCustomer';
import CopyTextButton from '@/components/CopyTextButton';

import MaleIcon from '@mui/icons-material/Male';
import FemaleIcon from '@mui/icons-material/Female';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';

function CustomerList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingCustomers,
    isLoading,
    refetch,
    remove,
  } = useGetCustomers({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const { mutate: deleteCustomer, isSuccess } = useDeleteCustomer();

  React.useEffect(() => {
    if (isSuccess) {
      refetch();
    }
  }, [isSuccess, refetch]);

  const rowCount = pagingCustomers?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Customer>[] = [
    {
      field: 'id',
      headerName: 'ID',
      headerAlign: 'center',
      align: 'center',
      width: 120,
      renderCell(params) {
        const maxLength = 6;
        const truncatedValue =
          params.value.length > maxLength
            ? params.value.slice(0, 8) + '...'
            : params.value;

        return (
          <>
            <CopyTextButton text={params.value} />
            <Link href={`/customers/${params.value}`}>
              <Typography variant="button">{truncatedValue}</Typography>
            </Link>
          </>
        );
      },
    },
    {
      field: 'name',
      width: 200,
      headerName: 'FULL NAME',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'phone',
      width: 180,
      headerName: 'PHONE',
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
      field: 'gender',
      width: 200,
      headerName: 'GENDER',
      headerAlign: 'center',
      align: 'center',
      renderCell(params) {
        const getColor = () => {
          const value = params.value as Gender;
          if (value === Gender.Male) return 'info';
          if (value === Gender.Female) return 'success';
          return 'warning';
        };
        const getIcon = () => {
          const value = params.value as Gender;
          if (value === Gender.Male) return <MaleIcon />;
          if (value === Gender.Female) return <FemaleIcon />;
          return <MoreHorizIcon />;
        };
        return (
          <Chip
            icon={getIcon()}
            color={getColor()}
            label={`${Gender[params.value]}`}
            sx={{ width: 100 }}
          />
        );
      },
    },
    {
      field: 'address',
      width: 140,
      headerName: 'ADDRESS',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { address } = params.row;
        if (address) {
          const { street, ward, city, province } = address;
          const addressParts = [];

          if (street) addressParts.push(street);
          if (ward) addressParts.push(ward);
          if (city) addressParts.push(city);
          if (province) addressParts.push(province);

          return addressParts.join(', ');
        }
        return '';
      },
    },
    {
      field: 'actions',
      width: 320,
      headerName: 'ACTIONS',
      headerAlign: 'center',
      align: 'center',
      renderCell: (params) => {
        const customerId = params.row.id;
        return (
          <ActionForList
            entityId={customerId}
            entity="customer"
            detailsLink={`/customers/${customerId}`}
            editComponentProps={(handleClose) => (
              <EditCustomer
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ customerId: customerId }}
              />
            )}
            handleDelete={() => deleteCustomer(customerId)}
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
        spacing={{ sm: 1 }}
        marginBottom={2}
      >
        <Typography variant="h5" fontWeight="bold">
          CUSTOMER LIST
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
            buttonText="ADD CUSTOMER"
            buttonVariant="contained"
            buttonColor="success"
            buttonIcon={<PersonAddIcon />}
            dialogTitle="ADD CUSTOMER"
            dialogDescription="Please enter information for the customer"
            componentProps={(handleClose) => (
              <AddCustomer
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
        rows={pagingCustomers?.data.items || []}
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

export default CustomerList;
