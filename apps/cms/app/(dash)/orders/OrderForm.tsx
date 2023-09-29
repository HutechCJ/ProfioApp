'use client';

import React, { useEffect, useState } from 'react';

import { Order, OrderStatus } from '@/features/order/order.types';
import {
  TextField,
  MenuItem,
  Box,
  Alert,
  AlertTitle,
  Stack,
  Button,
  Divider,
  Typography,
} from '@mui/material';
import { useSnackbar } from 'notistack';
import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';
import CustomerList from '../customers/CustomerList';
import useSenderEmailOrder from '@/features/order/useSenderEmailOrder';

const statuses = [
  {
    value: OrderStatus.Pending,
    label: 'Pending',
  },
  {
    value: OrderStatus.InProgress,
    label: 'InProgress',
  },
  {
    value: OrderStatus.Completed,
    label: 'Completed',
  },
  {
    value: OrderStatus.Received,
    label: 'Received',
  },
  {
    value: OrderStatus.Cancelled,
    label: 'Cancelled',
  },
];

interface OrderFormProps {
  onSubmit: (data: FormData) => void;
  initialValue: {
    startedDate?: string;
    expectedDeliveryTime?: string;
    status?: OrderStatus;
    destinationAddress?: {
      street?: string;
      ward?: string;
      city?: string;
      province?: string;
      zipCode?: string;
    };
    destinationZipCode?: string;
    note?: string;
    distance?: number;
    customerId?: string;
  };
  error: any;
  isError: boolean;
  isSuccess: boolean;
  onSuccess: () => void;
  labelButton: string;
  isSuccessAddOrder?: boolean;
  dataResponseFromAddOrder?: ApiResponse<Order>;
}

const OrderForm: React.FC<OrderFormProps> = ({
  onSubmit,
  initialValue,
  error,
  isError,
  isSuccess,
  onSuccess,
  labelButton,
  isSuccessAddOrder,
  dataResponseFromAddOrder,
}) => {
  const [order, setOrder] = useState({
    startedDate: initialValue.startedDate || '',
    expectedDeliveryTime: initialValue.expectedDeliveryTime || '',
    status: initialValue.status || OrderStatus.Pending,
    destinationAddress: {
      street: initialValue.destinationAddress?.street || '',
      ward: initialValue.destinationAddress?.ward || '',
      city: initialValue.destinationAddress?.city || '',
      province: initialValue.destinationAddress?.province || '',
      zipCode: initialValue.destinationAddress?.zipCode || '',
    } || {
      street: '',
      ward: '',
      city: '',
      province: '',
      zipCode: '',
    },
    destinationZipCode: initialValue.destinationZipCode || '',
    note: initialValue.note || '',
    distance: initialValue.distance || 0,
    customerId: initialValue.customerId || '',
  });

  const { enqueueSnackbar } = useSnackbar();
  const { refetch: refetchCount } = useCountByOrderStatus();

  const { mutate: sendEmailOrder } = useSenderEmailOrder();

  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.name.includes('destinationAddress.')) {
      const childName = e.target.name.split('.').pop();
      if (!childName) return;
      setOrder((o) => ({
        ...o,
        destinationAddress: {
          ...o.destinationAddress,
          [childName]: e.target.value,
        },
      }));
    } else {
      setOrder((o) => ({
        ...o,
        [e.target.name]: e.target.value,
      }));
    }
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    onSubmit(data);
    setOrder({
      startedDate: initialValue.startedDate || '',
      expectedDeliveryTime: initialValue.expectedDeliveryTime || '',
      status: initialValue.status || OrderStatus.Pending,
      destinationAddress: {
        street: initialValue.destinationAddress?.street || '',
        ward: initialValue.destinationAddress?.ward || '',
        city: initialValue.destinationAddress?.city || '',
        province: initialValue.destinationAddress?.province || '',
        zipCode: initialValue.destinationAddress?.zipCode || '',
      } || {
        street: '',
        ward: '',
        city: '',
        province: '',
        zipCode: '',
      },
      destinationZipCode: initialValue.destinationZipCode || '',
      note: initialValue.note || '',
      distance: initialValue.distance || 0,
      customerId: initialValue.customerId || '',
    });
  };

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar('Successfully!', {
        variant: 'success',
      });
      refetchCount();
      onSuccess();
    }
    if (isSuccessAddOrder && dataResponseFromAddOrder) {
      const orderId = dataResponseFromAddOrder.data.id;
      const customerName = dataResponseFromAddOrder.data.customer?.name || '';
      const email = 'nhthai.dev@gmail.com';
      const phone = '0856028897';
      const from = 'CJ Profio';
      const to = 'nhthai.dev@gmail.com';

      sendEmailOrder({
        id: orderId,
        customerName: customerName,
        email: email,
        phone: phone,
        from: from,
        to: to,
      });
    }
  }, [
    isSuccess,
    // enqueueSnackbar,
    // onSuccess,
    // refetchCount,
    isSuccessAddOrder,
    dataResponseFromAddOrder,
    // sendEmailOrder,
  ]);

  return (
    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="startedDate"
        name="startedDate"
        label="Started Date"
        autoComplete="startedDate"
        value={order.startedDate}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="expectedDeliveryTime"
        name="expectedDeliveryTime"
        label="Expected Delivery Time"
        autoComplete="expectedDeliveryTime"
        value={order.expectedDeliveryTime}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        select
        defaultValue={OrderStatus.Pending}
        variant="filled"
        id="status"
        name="status"
        label="Order Status"
        value={order.status}
        onChange={handleChangeInput}
      >
        {statuses.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationAddress.street"
        name="destinationAddress.street"
        label="Street • Destination Address"
        autoComplete="street"
        value={order.destinationAddress.street}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationAddress.ward"
        name="destinationAddress.ward"
        label="Ward • Destination Address"
        autoComplete="ward"
        value={order.destinationAddress.ward}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationAddress.city"
        name="destinationAddress.city"
        label="City • Destination Address"
        autoComplete="city"
        value={order.destinationAddress.city}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationAddress.province"
        name="destinationAddress.province"
        label="Province • Destination Address"
        autoComplete="province"
        value={order.destinationAddress.province}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationAddress.zipCode"
        name="destinationAddress.zipCode"
        label="ZipCode • Destination Address"
        autoComplete="zipCode"
        value={order.destinationAddress.zipCode}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="destinationZipCode"
        name="destinationZipCode"
        label="Destination ZipCode"
        autoComplete="destinationZipCode"
        value={order.destinationZipCode}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="note"
        name="note"
        label="Note"
        autoComplete="note"
        value={order.note}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="distance"
        name="distance"
        label="Distance"
        autoComplete="distance"
        value={order.distance}
        onChange={handleChangeInput}
        type="number"
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="customerId"
        name="customerId"
        label="Customer Id"
        autoComplete="customerId"
        value={order.customerId}
        onChange={handleChangeInput}
      />
      <Typography variant="body2" fontStyle="italic" color="primary">
        More information about the customer is below
      </Typography>
      {isError && (
        <Alert severity="error" sx={{ mt: 2 }}>
          <AlertTitle>Error</AlertTitle>
          <Stack>
            {error &&
              error.response &&
              error.response.data &&
              error.response.data.data &&
              Object.values(error.response.data.data)
                .flat()
                .map((value, i) => (
                  <React.Fragment key={`error_${i}`}>
                    {String(value)}
                  </React.Fragment>
                ))}
          </Stack>
        </Alert>
      )}
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        {labelButton}
      </Button>
      <Divider sx={{ mb: 2 }} />
      <CustomerList />
    </Box>
  );
};

export default OrderForm;
