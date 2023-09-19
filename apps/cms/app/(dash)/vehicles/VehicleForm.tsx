'use client';

import React, { useEffect, useState } from 'react';

import { VehicleType, VehicleStatus } from '@/features/vehicle/vehicle.types';
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
import useCountByVehicleType from '@/features/vehicle/useCountByVehicleType';
import StaffList from '../staffs/StaffList';

const vehicleTypes = [
  {
    value: VehicleType.Truck,
    label: 'Truck',
  },
  {
    value: VehicleType.Trailer,
    label: 'Trailer',
  },
  {
    value: VehicleType.Van,
    label: 'Van',
  },
  {
    value: VehicleType.Motorcycle,
    label: 'Motorcycle',
  },
];

const vehicleStatuses = [
  {
    value: VehicleStatus.Idle,
    label: 'Idle',
  },
  {
    value: VehicleStatus.Busy,
    label: 'Busy',
  },
  {
    value: VehicleStatus.Offline,
    label: 'Offline',
  },
];

interface VehicleFormProps {
  onSubmit: (data: FormData) => void;
  initialValue: {
    zipCodeCurrent?: string;
    licensePlate?: string;
    type?: VehicleType;
    status?: VehicleStatus;
    staffId?: string;
  };
  error: any;
  isError: boolean;
  isSuccess: boolean;
  onSuccess: () => void;
  labelButton: string;
}

const VehicleForm: React.FC<VehicleFormProps> = ({
  onSubmit,
  initialValue,
  error,
  isError,
  isSuccess,
  onSuccess,
  labelButton,
}) => {
  const [vehicle, setVehicle] = useState({
    zipCodeCurrent: initialValue.zipCodeCurrent || '',
    licensePlate: initialValue.licensePlate || '',
    type: initialValue.type || VehicleType.Truck,
    status: initialValue.status || VehicleStatus.Idle,
    staffId: initialValue.staffId || '',
  });

  const { enqueueSnackbar } = useSnackbar();
  const { refetch: refetchCount } = useCountByVehicleType();

  const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setVehicle({
      ...vehicle,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    onSubmit(data);
    setVehicle({
      zipCodeCurrent: initialValue.zipCodeCurrent || '',
      licensePlate: initialValue.licensePlate || '',
      type: initialValue.type || VehicleType.Truck,
      status: initialValue.status || VehicleStatus.Idle,
      staffId: initialValue.staffId || '',
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
        id="zipCodeCurrent"
        name="zipCodeCurrent"
        label="Zip Code Current"
        autoComplete="given-zipCodeCurrent"
        value={vehicle.zipCodeCurrent}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        variant="filled"
        id="licensePlate"
        name="licensePlate"
        label="License Plate"
        autoComplete="licensePlate"
        value={vehicle.licensePlate}
        onChange={handleChangeInput}
      />
      <TextField
        margin="dense"
        required
        fullWidth
        select
        defaultValue={VehicleType.Truck}
        variant="filled"
        id="type"
        name="type"
        label="Type"
        value={vehicle.type}
        onChange={handleChangeInput}
      >
        {vehicleTypes.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>
      <TextField
        margin="dense"
        required
        fullWidth
        select
        defaultValue={VehicleType.Truck}
        variant="filled"
        id="status"
        name="status"
        label="Status"
        value={vehicle.status}
        onChange={handleChangeInput}
      >
        {vehicleStatuses.map((option) => (
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
        id="staffId"
        name="staffId"
        label="Staff Id"
        autoComplete="staffId"
        value={vehicle.staffId}
        onChange={handleChangeInput}
      />
      <Typography variant="body2" fontStyle="italic" color="primary">
        More information about the staff is below
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
      <Divider />
      <StaffList />
    </Box>
  );
};

export default VehicleForm;
