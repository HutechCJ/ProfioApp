'use client';

import React, { useEffect, useState } from 'react';

import { OrderStatus } from '@/features/order/order.types';
import {
  TextField,
  MenuItem,
  Box,
  Alert,
  AlertTitle,
  Stack,
  Button,
} from '@mui/material';
import { useSnackbar } from 'notistack';
import useCountByOrderStatus from '@/features/order/useCountByOrderStatus';

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
    street?: string;
    ward?: string;
    city?: string;
    province?: string;
    zipCode?: string;
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
}

const OrderForm: React.FC<OrderFormProps> = ({
  onSubmit,
  initialValue,
  error,
  isError,
  isSuccess,
  onSuccess,
  labelButton,
}) => {
  const [order, setOrder] = useState({
    startedDate: initialValue.startedDate || '',
    expectedDeliveryTime: initialValue.expectedDeliveryTime || '',
    status: initialValue.status || OrderStatus.Pending,
    street: initialValue.street || '',
    ward: initialValue.ward || '',
    city: initialValue.city || '',
    province: initialValue.province || '',
    zipCode: initialValue.zipCode || '',
    destinationZipCode: initialValue.destinationZipCode || '',
    note: initialValue.note || '',
    distance: initialValue.distance || 0,
    customerId: initialValue.customerId || '',
  });

  const { enqueueSnackbar } = useSnackbar();
  const { refetch: refetchCount } = useCountByOrderStatus();

  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setOrder({
      ...order,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    onSubmit(data);
    setOrder({
      startedDate: initialValue.startedDate || '',
      expectedDeliveryTime: initialValue.expectedDeliveryTime || '',
      status: initialValue.status || OrderStatus.Pending,
      street: initialValue.street || '',
      ward: initialValue.ward || '',
      city: initialValue.city || '',
      province: initialValue.province || '',
      zipCode: initialValue.zipCode || '',
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
  }, [isSuccess, enqueueSnackbar, onSuccess, refetchCount]);

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
        id="street"
        name="street"
        label="Street • Destination Address"
        autoComplete="street"
        value={order.street}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="ward"
        name="ward"
        label="Ward • Destination Address"
        autoComplete="ward"
        value={order.ward}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="city"
        name="city"
        label="City • Destination Address"
        autoComplete="city"
        value={order.city}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="province"
        name="province"
        label="Province • Destination Address"
        autoComplete="province"
        value={order.province}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="zipCode"
        name="zipCode"
        label="ZipCode • Destination Address"
        autoComplete="zipCode"
        value={order.zipCode}
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
    </Box>
  );
};

export default OrderForm;
