'use client';

import React from 'react';

import useUpdateVehicle from '@/features/vehicle/useUpdateVehicle';
import VehicleForm from './VehicleForm';
import useGetVehicle from '@/features/vehicle/useGetVehicle';
import { VehicleType, VehicleStatus } from '@/features/vehicle/vehicle.types';

interface EditVehicleProps {
  onSuccess: () => void;
  params: { vehicleId: string };
}

const EditVehicle: React.FC<EditVehicleProps> = ({ onSuccess, params }) => {
  const {
    data: vehicleApiRes,
    isLoading,
    isError,
    refetch,
  } = useGetVehicle(params.vehicleId);
  const {
    mutate: updateVehicle,
    error,
    isError: isErrorUpdateVehicle,
    isSuccess,
  } = useUpdateVehicle();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError || !vehicleApiRes) {
    return <div>There was an error!</div>;
  }

  const handleEditVehicle = (vehicleId: string, data: FormData) => {
    updateVehicle({
      id: vehicleId,
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
      onSubmit={(data: FormData) => {
        handleEditVehicle(params.vehicleId, data);
        refetch();
      }}
      initialValue={{
        zipCodeCurrent: vehicleApiRes.data.zipCodeCurrent,
        licensePlate: vehicleApiRes.data.licensePlate,
        type: vehicleApiRes.data.type,
        status: vehicleApiRes.data.status,
        staffId: vehicleApiRes.data.staff?.id,
      }}
      error={error}
      isError={isErrorUpdateVehicle}
      isSuccess={isSuccess}
      onSuccess={onSuccess}
      labelButton="UPDATE"
    />
  );
};

export default EditVehicle;
