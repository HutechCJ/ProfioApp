'use client';

import React from 'react';

import Link from '@/components/Link';
import LoadingButton from '@/components/LoadingButton';
import {
  Box,
  Typography,
  Stack,
  ButtonGroup,
  Chip,
  Divider,
} from '@mui/material';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';

import ReplayIcon from '@mui/icons-material/Replay';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

import { Order, OrderStatus } from '@/features/order/order.types';
import useGetOrders from '@/features/order/useGetOrders';
import FormDialog from '@/components/FormDialog';
import AddOrder from './AddOrder';
import EditOrder from './EditOrder';
import ActionForList from '@/components/ActionForList';
import useDeleteOrder from '@/features/order/useDeleteOrder';
import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';
import CopyTextButton from '@/components/CopyTextButton';

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

  const { mutate: deleteOrder, isSuccess } = useDeleteOrder();

  const { refetch: refetchCount } = useCountByOrderStatus();

  React.useEffect(() => {
    if (isSuccess) {
      refetchCount();
      refetch();
    }
  }, [isSuccess, refetchCount, refetch]);

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
            <Link href={`/orders/${params.value}`}>
              <Typography variant="button">{truncatedValue}</Typography>
            </Link>
          </>
        );
      },
    },
    {
      field: 'status',
      headerName: 'STATUS',
      headerAlign: 'center',
      align: 'center',
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
      width: 175,
      headerName: 'STARTED DATE',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { startedDate } = params.row;
        return `${new Date(startedDate).toLocaleString()}`;
      },
    },
    {
      field: 'expectedDeliveryTime',
      width: 200,
      headerName: 'EXPECTED DELIVERY TIME',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { expectedDeliveryTime } = params.row;
        return `${new Date(expectedDeliveryTime).toLocaleString()}`;
      },
    },
    {
      field: 'destinationAddress',
      width: 100,
      headerName: 'ADDRESS',
      headerAlign: 'center',
      align: 'center',
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
      width: 100,
      headerName: 'ZIP CODE',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'note',
      width: 100,
      headerName: 'NOTE',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'distance',
      width: 100,
      headerName: 'DISTANCE',
      headerAlign: 'center',
      align: 'center',
    },
    {
      field: 'customer',
      width: 100,
      headerName: 'CUSTOMER',
      headerAlign: 'center',
      align: 'center',
      valueGetter: (params) => {
        const { customer } = params.row;
        return `${customer?.name || 'Empty'}`;
      },
      renderCell(params) {
        const { customer } = params.row;
        return (
          <Link href={`/customers/${customer?.id}`} sx={{ color: 'black' }}>
            <Typography variant="button">
              {customer?.name || 'Empty'}
            </Typography>
          </Link>
        );
      },
    },
    {
      field: 'actions',
      width: 320,
      headerName: 'ACTIONS',
      headerAlign: 'center',
      align: 'center',
      sortable: false,
      filterable: false,
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
            handleDelete={() => deleteOrder(orderId)}
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

export default OrderList;
