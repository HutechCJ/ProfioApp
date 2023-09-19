'use client';

import React from 'react';

import { VehicleType, VehicleStatus } from '@/features/vehicle/vehicle.types';
import useCreateVehicle from '@/features/vehicle/useCreateVehicle';
import VehicleForm from './VehicleForm';

interface AddVehicleProps {
  onSuccess: () => void;
}

const AddVehicle: React.FC<AddVehicleProps> = ({ onSuccess }) => {
  const {
    mutate: createVehicle,
    error,
    isError,
    isSuccess,
  } = useCreateVehicle();

  const handleAddVehicle = (data: FormData) => {
    createVehicle({
      zipCodeCurrent: data.get('zipCodeCurrent') as string,
      licensePlate: data.get('licensePlate') as string,
      type: VehicleType[parseInt(data.get('type') as string) as VehicleType],
      status:
        VehicleStatus[parseInt(data.get('status') as string) as VehicleStatus],
      staffId: data.get('staffId') as string,
    });
  };

  return (
    <VehicleForm
      onSubmit={handleAddVehicle}
      initialValue={{}}
      error={error}
      isError={isError}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="SUBMIT"
    />
  );
};

export default AddVehicle;
