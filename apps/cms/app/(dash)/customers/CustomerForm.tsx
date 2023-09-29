'use client';

import React, { useEffect, useState } from 'react';

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
import { Gender } from '@/features/customer/customer.types';

const genders = [
  {
    value: Gender.Male,
    label: 'Male',
  },
  {
    value: Gender.Female,
    label: 'Female',
  },
  {
    value: Gender.Other,
    label: 'Other',
  },
];

interface CustomerFormProps {
  onSubmit: (data: FormData) => void;
  initialValue: {
    name?: string;
    phone?: string;
    email?: string;
    gender?: Gender;
    address?: {
      street?: string;
      ward?: string;
      city?: string;
      province?: string;
      zipCode?: string;
    };
  };
  error: any;
  isError: boolean;
  isSuccess: boolean;
  onSuccess: () => void;
  labelButton: string;
}

const CustomerForm: React.FC<CustomerFormProps> = ({
  onSubmit,
  initialValue,
  error,
  isError,
  isSuccess,
  onSuccess,
  labelButton,
}) => {
  const [customer, setCustomer] = useState({
    name: initialValue.name || '',
    phone: initialValue.phone || '',
    email: initialValue.email || '',
    gender: initialValue.gender || Gender.Other,
    address: {
      street: initialValue.address?.street || '',
      ward: initialValue.address?.ward || '',
      city: initialValue.address?.city || '',
      province: initialValue.address?.province || '',
      zipCode: initialValue.address?.zipCode || '',
    } || {
      street: '',
      ward: '',
      city: '',
      province: '',
      zipCode: '',
    },
  });

  const { enqueueSnackbar } = useSnackbar();

  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.name.includes('address.')) {
      const childName = e.target.name.split('.').pop();
      if (!childName) return;
      setCustomer((c) => ({
        ...c,
        address: {
          ...c.address,
          [childName]: e.target.value,
        },
      }));
    } else {
      setCustomer((c) => ({
        ...c,
        [e.target.name]: e.target.value,
      }));
    }
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    onSubmit(data);
    setCustomer({
      name: initialValue.name || '',
      phone: initialValue.phone || '',
      email: initialValue.email || '',
      gender: initialValue.gender || Gender.Other,
      address: {
        street: initialValue.address?.street || '',
        ward: initialValue.address?.ward || '',
        city: initialValue.address?.city || '',
        province: initialValue.address?.province || '',
        zipCode: initialValue.address?.zipCode || '',
      } || {
        street: '',
        ward: '',
        city: '',
        province: '',
        zipCode: '',
      },
    });
  };

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar('Successfully!', {
        variant: 'success',
      });
      onSuccess();
    }
  }, [
    isSuccess,
    // enqueueSnackbar,
    // onSuccess
  ]);

  return (
    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="name"
        name="name"
        label="Full Name"
        autoComplete="given-name"
        value={customer.name}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="phone"
        name="phone"
        label="Phone"
        autoComplete="phone"
        value={customer.phone}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="email"
        name="email"
        label="Email"
        autoComplete="email"
        value={customer.email}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        select
        defaultValue={Gender.Other}
        variant="filled"
        id="gender"
        name="gender"
        label="Gender"
        value={customer.gender}
        onChange={handleChangeInput}
      >
        {genders.map((option) => (
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
        id="address.street"
        name="address.street"
        label="Street • Address"
        autoComplete="street"
        value={customer.address.street}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="address.ward"
        name="address.ward"
        label="Ward • Address"
        autoComplete="ward"
        value={customer.address.ward}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="address.city"
        name="address.city"
        label="City • Address"
        autoComplete="city"
        value={customer.address.city}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="address.province"
        name="address.province"
        label="Province • Address"
        autoComplete="province"
        value={customer.address.province}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="address.zipCode"
        name="address.zipCode"
        label="Zip Code • Address"
        autoComplete="zipCode"
        value={customer.address.zipCode}
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

export default CustomerForm;
