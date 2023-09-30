'use client';

import React, { useEffect, useState } from 'react';

import useSwal from '@/common/hooks/useSwal';
import { StaffPosition } from '@/features/staff/staff.types';
import useCountByPosition from '@/features/staff/useCountByPosition';
import {
  Alert,
  AlertTitle,
  Box,
  Button,
  MenuItem,
  Stack,
  TextField,
} from '@mui/material';
import {
  confirmSwalOption,
  successSwalOption,
} from '@/common/constants/sweetAlertOptions';

const positions = [
  {
    value: StaffPosition.Driver,
    label: 'Driver',
  },
  {
    value: StaffPosition.Shipper,
    label: 'Shipper',
  },
  {
    value: StaffPosition.Officer,
    label: 'Officer',
  },
  {
    value: StaffPosition.Stoker,
    label: 'Stoker',
  },
];

interface StaffFormProps {
  onSubmit: (data: FormData) => void;
  initialValue: {
    name?: string;
    phone?: string & { length: 10 };
    position?: StaffPosition;
  };
  error: any;
  isError: boolean;
  isSuccess: boolean;
  onSuccess: () => void;
  labelButton: string;
}

const StaffForm: React.FC<StaffFormProps> = ({
  onSubmit,
  initialValue,
  error,
  isError,
  isSuccess,
  onSuccess,
  labelButton,
}) => {
  const Swal = useSwal();
  const [staff, setStaff] = useState({
    name: initialValue.name || '',
    phone: initialValue.phone || '',
    position: initialValue.position || StaffPosition.Driver,
  });
  const { refetch: refetchCount } = useCountByPosition();

  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setStaff({
      ...staff,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    Swal.fire(confirmSwalOption).then((result) => {
      if (result.isConfirmed) {
        onSubmit(data);
        setStaff({
          name: initialValue.name || '',
          phone: initialValue.phone || '',
          position: initialValue.position || StaffPosition.Driver,
        });
      }
    });
  };

  useEffect(() => {
    if (isSuccess) {
      Swal.fire(successSwalOption);
      refetchCount();
      onSuccess();
    }
  }, [
    isSuccess,
    // enqueueSnackbar,
    // onSuccess,
    // refetchCount
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
        value={staff.name}
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
        value={staff.phone}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        select
        defaultValue={StaffPosition.Driver}
        variant="filled"
        id="position"
        name="position"
        label="Position"
        value={staff.position}
        onChange={handleChangeInput}
      >
        {positions.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>
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

export default StaffForm;
