'use client';

import React, { useEffect } from 'react';

import { StaffPosition } from '@/features/staff/staff.types';
import {
  TextField,
  MenuItem,
  Box,
  Alert,
  AlertTitle,
  Stack,
  Button,
} from '@mui/material';
import useCreateStaff from '@/features/staff/useCreateStaff';
import { useSnackbar } from 'notistack';
import useCountByPosition from '@/features/staff/useCountByPosition';

const positions = [
  {
    value: StaffPosition.Driver,
    label: 'Driver',
  },
  {
    value: StaffPosition.Shipper,
    label: 'Shipper',
  },
];

interface StaffFormProps {
  onSuccess: () => void;
}

const StaffForm: React.FC<StaffFormProps> = ({ onSuccess }) => {
  const { enqueueSnackbar } = useSnackbar();
  const { mutate: createStaff, error, isError, isSuccess } = useCreateStaff();
  const { refetch: refetchCount } = useCountByPosition();

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    createStaff({
      name: data.get('name') as string,
      phone: data.get('phone') as string & { length: 10 },
      position:
        StaffPosition[
          parseInt(data.get('position') as string) as StaffPosition
        ],
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
        id="name"
        name="name"
        label="Full Name"
        autoComplete="given-name"
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
            {Object.values(
              (error as any).response.data.data as {
                [key: string]: any;
              }
            )
              .flat()
              .map((value, i) => (
                <span key={`error_${i}`}>{value}</span>
              ))}
          </Stack>
        </Alert>
      )}
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        SUBMIT
      </Button>
    </Box>
  );
};

export default StaffForm;
