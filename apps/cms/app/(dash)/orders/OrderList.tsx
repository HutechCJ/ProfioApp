'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import { Box, Typography, Stack, ButtonGroup, Chip } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Order, OrderStatus } from '@/features/order/order.types';
import useGetOrders from '@/features/order/useGetOrders';
import FormDialog from '@/components/FormDialog';
import AddOrder from './AddOrder';
import EditOrder from './EditOrder';
import DeleteOrder from './DeleteOrder';
import ActionForList from '@/components/ActionForList';

function OrderList() {
  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  const {
    data: pagingOrders,
    isLoading,
    refetch,
    remove,
  } = useGetOrders({
    PageIndex: paginationModel.page + 1,
    PageSize: paginationModel.pageSize,
  });

  const rowCount = pagingOrders?.data.totalCount || 0;

  const [rowCountState, setRowCountState] = React.useState(rowCount);

  React.useEffect(() => {
    setRowCountState((prevRowCountState) =>
      rowCount !== undefined ? rowCount : prevRowCountState
    );
  }, [rowCount, setRowCountState]);

  const columns: GridColDef<Order>[] = [
    {
      field: 'id',
      headerName: 'ID',
      width: 240,
      renderCell(params) {
        return (
          <Link href={`/orders/${params.value}`}>
            <Typography variant="button">{params.value}</Typography>
          </Link>
        );
      },
    },
    {
      field: 'status',
      headerName: 'STATUS',
      width: 110,
      renderCell(params) {
        const getColor = () => {
          const value = params.value as OrderStatus;
          if (value === OrderStatus.Cancelled) return 'error';
          if (value === OrderStatus.Completed) return 'info';
          if (value === OrderStatus.Received) return 'success';
          if (value === OrderStatus.InProgress) return 'warning';
          return 'default';
        };
        return (
          <Chip color={getColor()} label={`${OrderStatus[params.value]}`} />
        );
      },
    },
    {
      field: 'startedDate',
      width: 170,
      headerName: 'STARTED DATE',
      valueGetter: (params) => {
        const { startedDate } = params.row;
        return `${new Date(startedDate).toLocaleString()}`;
      },
    },
    {
      field: 'expectedDeliveryTime',
      width: 180,
      headerName: 'EXPECTED DELIVERY TIME',
      valueGetter: (params) => {
        const { expectedDeliveryTime } = params.row;
        return `${new Date(expectedDeliveryTime).toLocaleString()}`;
      },
    },
    {
      field: 'destinationAddress',
      width: 80,
      headerName: 'ADDRESS',
      valueGetter: (params) => {
        const { destinationAddress } = params.row;
        if (destinationAddress) {
          const { street, ward, city, province } = destinationAddress;
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
      field: 'destinationZipCode',
      width: 80,
      headerName: 'ZIP CODE',
    },
    {
      field: 'note',
      width: 80,
      headerName: 'NOTE',
    },
    {
      field: 'distance',
      width: 80,
      headerName: 'DISTANCE',
    },
    {
      field: 'customer',
      width: 100,
      headerName: 'CUSTOMER',
      valueGetter: (params) => {
        const { customer } = params.row;
        return `${customer?.name || 'Empty'}`;
      },
      renderCell(params) {
        const { customer } = params.row;
        return (
          <Link href={`/customers/${customer?.id}`} sx={{ color: 'black' }}>
            <Typography variant="button">{customer?.name || 'Empty'}</Typography>
          </Link>
        );
      },
    },
    {
      field: '',
      width: 360,
      headerName: 'ACTIONS',
      renderCell: (params) => {
        const orderId = params.row.id;
        return (
          <ActionForList
            entityId={orderId}
            entity="order"
            detailsLink={`/orders/${orderId}`}
            editComponentProps={(handleClose) => (
              <EditOrder
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ orderId: orderId }}
              />
            )}
            deleteComponentProps={(handleClose) => (
              <DeleteOrder
                onSuccess={() => {
                  handleClose();
                  refetch();
                }}
                params={{ orderId: orderId }}
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
          ORDER LIST
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
            buttonText="ADD ORDER"
            buttonVariant="contained"
            buttonColor="success"
            buttonIcon={<PersonAddIcon />}
            dialogTitle="ADD ORDER"
            dialogDescription="Please enter information for the order"
            componentProps={(handleClose) => (
              <AddOrder
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
        rows={pagingOrders?.data.items || []}
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

export default OrderList;
